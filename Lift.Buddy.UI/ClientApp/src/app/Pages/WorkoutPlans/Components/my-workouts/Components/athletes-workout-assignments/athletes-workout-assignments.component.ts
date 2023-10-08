import { WorkoutplanService } from 'src/app/Services/workoutplan.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { User } from 'src/app/Model/User';
import { TrainerService } from 'src/app/Services/trainer.service';
import { UserService } from 'src/app/Services/user.service';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-athletes-workout-assignments',
  templateUrl: './athletes-workout-assignments.component.html',
  styleUrls: ['./athletes-workout-assignments.component.css']
})
export class AthletesWorkoutAssignmentsComponent implements OnInit {

  public workoutPlan: WorkoutPlan | undefined;
  public athletesAssigned: User[] = [];
  public athletesSuggested: User[] = [];
  public athletesSubscribed: User[] = [];
  public readonlySubscribers: number = 0;
  private username: string = "";

  constructor(
    private activatedRoute: ActivatedRoute,
    private workoutPlanService: WorkoutplanService,
    private userService: UserService,
    private trainerService: TrainerService,
    private snackbarService: SnackBarService
  ) { }

  async ngOnInit() {
    await this.initWorkoutData();
    await this.initAthletes();
  }

  private userSuggestionSubscription: Subscription | undefined;
  ngOnDestroy() {
    this.userService.setUserSuggestion('-1'); // TODO: set something to reset observable
    this.userSuggestionSubscription?.unsubscribe();
  }

  private async initWorkoutData() {
    const workoutId = this.activatedRoute.snapshot.paramMap.get('workoutId')!;

    const response = await this.workoutPlanService.getWorkoutPlanById(workoutId);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load workout plan. Ex: ${response.notes}`);
      return;
    }

    this.workoutPlan = response.body[0];
  }

  private async initAthletes() {
    if (this.workoutPlan == null) {
      return;
    }
    const athletesAssignedResp = await this.workoutPlanService.getWorkoutPlanSubscribers(this.workoutPlan);
    if (!athletesAssignedResp.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load athletes already assigned. Ex: ${athletesAssignedResp.notes}`);
      return;
    }

    this.athletesAssigned = athletesAssignedResp.body;
    this.readonlySubscribers = athletesAssignedResp.body.length;

    const athletesSubscribedResp = await this.trainerService.getMyAthletes();
    if (!athletesSubscribedResp.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load athletes subscribed. Ex: ${athletesAssignedResp.notes}`);
      return;
    }

    this.athletesSubscribed = athletesSubscribedResp.body.filter(x =>
      this.athletesAssigned.find(y => y.id == x.id) == null
    );
    this.userSuggestionSubscription = this.userService.userSuggestion$.subscribe(userSuggestion => {
      if (this.athletesSubscribed.length == 0) {
        this.athletesSuggested = userSuggestion;
        return;
      }
      this.athletesSuggested = this.athletesSubscribed
        .filter(x => this.athletesSuggested.findIndex(y => x.id == y.id) == -1)
        .filter(x => x.credentials.username.includes(this.username))
        .concat(userSuggestion)
        .slice(0, 10);
    })
  }

  public async getUsersByUsername(event: any) {
    const username = event.srcElement.value;
    this.userService.setUserSuggestion(username);
  }

  public assignWorkout() {
    this.athletesAssigned.push(new User());
  }

  public removeAssignation(assignmentIdx: number) {
    this.athletesAssigned.splice(assignmentIdx, 1);
    if (assignmentIdx <= this.readonlySubscribers) {
      this.readonlySubscribers--;
    }
  }

  public async save() {
    if (!this.workoutPlan) {
      this.snackbarService.operErrorSnackbar('Assignments not saved');
      return;
    }

    const response = await this.workoutPlanService.setWorkoutPlanSubscribers(this.workoutPlan, this.athletesAssigned);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar('Assignments not saved');
      return;
    }

    this.snackbarService.openSuccessSnackbar('Assignments saved');
  }

  public optionSelected(event: MatAutocompleteSelectedEvent, assignmentIdx: number) {
    const user = event.option.value as User;
    this.athletesAssigned[assignmentIdx] = user;
    const userIdx = this.athletesSuggested.indexOf(user);
    this.athletesSuggested.splice(userIdx, 1);
  }

  public displayFn(user: User) {
    return user.credentials.username;
  }

}
