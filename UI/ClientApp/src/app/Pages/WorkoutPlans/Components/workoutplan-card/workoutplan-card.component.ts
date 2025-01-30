import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { DialogService } from 'src/app/Services/Utils/dialog.service';
import { DeleteWorkoutPlanConfirmationPopupComponent } from '../../Pages/my-workouts/Components/delete-workout-plan-confirmation-popup/delete-workout-plan-confirmation-popup.component';
import { Review } from 'src/app/Model/Review';
import { WorkoutplanService } from 'src/app/Services/workoutplan.service';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';

@Component({
  selector: 'app-workoutplan-card',
  templateUrl: './workoutplan-card.component.html',
  styleUrls: ['./workoutplan-card.component.css']
})
export class WorkoutplanCardComponent implements OnInit {

  @Input() workoutplan: WorkoutPlan | undefined;

  @Input() subscribedToWorkoutPlan: number = 0;

  @Input() POV: 'trainer' | 'athlete' = 'athlete';
  
  @Output() OnWorkoutplanDelete: EventEmitter<WorkoutPlan> = new EventEmitter();

  public notes: string = '';

	public get isTrainer(): boolean {
    return this.POV == 'trainer';
  }
	public get isAthlete(): boolean {
    return this.POV == 'athlete';
  }

  constructor(
    private dialogService: DialogService,
    private workoutplanService: WorkoutplanService,
    private snackbarService: SnackBarService
  ) { }

  ngOnInit() {
  }
  
	public async deleteWorkout(workout: WorkoutPlan) {
		const dialogRef = this.dialogService.openCenterDialog(
			DeleteWorkoutPlanConfirmationPopupComponent,
			{
				data: { workout: workout }
			}
		);

		dialogRef.afterClosed().subscribe(result => {
			if (result) {
        this.OnWorkoutplanDelete.emit(workout);
			}
		})
	}

  public async reviewWorkoutplan(rating: number) {
    if (this.isTrainer) {
      return;
    }
    let review = new Review();
    review.value = rating,
    review.workoutPlanId = this.workoutplan?.id;
    review.notes = this.notes;

    const response = await this.workoutplanService.reviewWorkoutPlan(review);

    if (!response.result) {
      this.snackbarService.operErrorSnackbar('Failed to review.')
    }

    this.snackbarService.openSuccessSnackbar('Your review was submitted.')
  }
 
}
