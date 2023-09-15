import { Component, OnInit } from '@angular/core';
import {COMMA, ENTER, SPACE} from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { WorkoutplanService } from 'src/app/Services/workoutplan.service';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';

@Component({
  selector: 'app-my-workouts',
  templateUrl: './my-workouts.component.html',
  styleUrls: ['./my-workouts.component.css']
})
export class MyWorkoutsComponent implements OnInit {

  public separatorKeyCodes = [COMMA, ENTER, SPACE];

  public workouts: WorkoutPlan[] = [];

  constructor(
    private workoutplanService: WorkoutplanService,
    private snackbarService: SnackBarService
  ) { }

  async ngOnInit() {
    await this.initWorkouts();
  }

  private async initWorkouts() {
    const workoutPlanResp = await this.workoutplanService.getWorkoutPlansCreatedByUsername();
    if (!workoutPlanResp.result) {
      this.snackbarService.operErrorSnackbar(`Failed ot load workouts due to: ${workoutPlanResp.notes}`)
    }

    this.workouts = workoutPlanResp.body;
  }

  public items: string[] = [];

  public add(event: MatChipInputEvent) {
    const value = (event.value || '').trim();

    if (value) {
      this.items.push(value);
    }

    event.chipInput!.clear();
  }

  public remove(item: any) {
    const idx = this.items.indexOf(item);

    if (idx >= 0) {
      this.items.splice(idx, 1);
    }
  }

  public async deleteWorkout(workout: WorkoutPlan) {
    const response = await this.workoutplanService.deleteWorkoutPlan(workout);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to delete workoutplan ${workout.name}`);
      return;
    }
    const deletedWorkoutIdx = this.workouts.indexOf(workout);
    if (deletedWorkoutIdx != -1) {
      this.workouts.splice(deletedWorkoutIdx, 1)
    }
  }

}
