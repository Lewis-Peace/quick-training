import { Subscription } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';
import { WorkoutplanService } from 'src/app/Services/workoutplan.service';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-assigned-to-me',
  templateUrl: './assigned-to-me.component.html',
  styleUrls: ['./assigned-to-me.component.css']
})
export class AssignedToMeComponent implements OnInit {

  public isLoading: boolean = false;
  public workoutplans: WorkoutPlan[] | undefined;

  constructor(
    private loadingVisualizationService: LoadingVisualizationService,
    private userService: UserService,
    private snackbarService: SnackBarService,
    private workoutplanService: WorkoutplanService
  ) { }

  async ngOnInit() {
    this.loadingVisualizationService.setIsLoading(true);
    await this.initWorkoutPlans();
    this.loadingVisualizationService.setIsLoading(false);
  }

  private async initWorkoutPlans() {
    const response = await this.userService.getAssignedWorkouts();
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load workouts. Ex: ${response.notes}`);
      return;
    }

    this.workoutplans = response.body;
    this.workoutplanService.setWorkoutPlan(response.body[0]);
  }

}
