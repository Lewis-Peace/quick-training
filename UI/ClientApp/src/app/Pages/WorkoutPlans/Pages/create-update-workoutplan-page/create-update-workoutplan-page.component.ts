import { WorkoutplanService } from './../../../../Services/workoutplan.service';
import { WorkoutDay } from './../../../../Model/WorkoutDay';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Exercise } from 'src/app/Model/Exercise';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
    selector: 'app-create-update-workoutplan-page',
    templateUrl: './create-update-workoutplan-page.component.html',
    styleUrls: ['./create-update-workoutplan-page.component.css']
})

export class CreateUpdateWorkoutplanPageComponent implements OnInit {

    @Input() isReadOnly: boolean = false

    public exercises = new FormControl<Exercise[]>([]);
    public workoutPlan: WorkoutPlan | undefined;
    public days: string[] = [];
    public isLoading: boolean = false;

    constructor(
        private activatedRoute: ActivatedRoute,
        private workoutplanService: WorkoutplanService,
        private snackbarService: SnackBarService,
        private loadingService: LoadingVisualizationService
    ) { }

    async ngOnInit() {
      this.loadingService.setIsLoading(true);
      await this.initWorkschedule();
      this.loadingService.setIsLoading(false);
    }

    ngOnDestroy() {
      this.workoutplanService.setWorkoutPlan(new WorkoutPlan());
    }

    private async initWorkschedule() {
        const workoutId = this.activatedRoute.snapshot.paramMap.get('workoutId')!;
        if (workoutId == "new") {
            this.workoutPlan = new WorkoutPlan();
            return;
        }

        const response = await this.workoutplanService.getWorkoutPlanById(workoutId);
        if (!response.result || response.body.length == 0) {
            this.snackbarService.operErrorSnackbar("Failed to load workout plan");
            return;
        }

        this.workoutPlan = response.body[0];
        this.workoutplanService.setWorkoutPlan(response.body[0]);
        this.workoutPlan.workoutDays.sort((x: any, y: any) => x.day! - y.day!);
    }

    public async save(workoutPlan: WorkoutPlan) {
        workoutPlan.workoutDays.forEach((x, idx) => {
          if (x.exercises.length == 0) {
            workoutPlan.workoutDays.splice(idx, 1);
          }
      })
        if (workoutPlan.id == "")
            this.workoutplanService.addWorkoutPlan(workoutPlan);
        else
            this.workoutplanService.updateWorkoutPlan(workoutPlan);
    }

}
