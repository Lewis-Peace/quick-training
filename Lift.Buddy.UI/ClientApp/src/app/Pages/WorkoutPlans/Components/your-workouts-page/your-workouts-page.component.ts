import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { Component, OnInit } from '@angular/core';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { WorkoutplanService } from 'src/app/Services/workoutplan.service';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
  selector: 'app-your-workouts-page',
  templateUrl: './your-workouts-page.component.html',
  styleUrls: ['./your-workouts-page.component.css']
})

export class YourWorkoutsPageComponent implements OnInit {

  public workouts: WorkoutPlan[] | undefined;
  public dailyWorkouts: { workout: WorkoutDay, name: string }[] = []
  public isLoading: boolean = false;

  constructor(
    private workoutplanService: WorkoutplanService,
    private snackbarService: SnackBarService,
    private loadingVisualizationService: LoadingVisualizationService
  ) { }

  async ngOnInit() {
    this.loadingVisualizationService.setIsLoading(true);
    await this.initUserWorkouts()
    this.loadingVisualizationService.setIsLoading(false);
  }

  private async initUserWorkouts() {
    const response = await this.workoutplanService.getWorkoutPlanByUser();

    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load workouts due to: ${response.notes}`)
    }

    this.workouts = response.body;
    const today = new Date().getDay();

    response.body.forEach(workout => {
      const workoutDay = workout.workoutDays.find(x => x.day == today);
      if (workoutDay != undefined) {
        this.dailyWorkouts.push({ workout: workoutDay, name: workout.name });
      }
    });
  }

}
