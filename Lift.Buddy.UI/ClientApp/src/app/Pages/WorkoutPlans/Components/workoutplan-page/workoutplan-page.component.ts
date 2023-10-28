import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Days } from 'src/app/Model/Enums/Days';
import { Exercise } from 'src/app/Model/Exercise';
import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { WorkoutplanService } from 'src/app/Services/workoutplan.service';

@Component({
  selector: 'app-workoutplan-page',
  templateUrl: './workoutplan-page.component.html',
  styleUrls: ['./workoutplan-page.component.css']
})
export class WorkoutplanPageComponent implements OnInit {

  @Input() workoutplan: WorkoutPlan | undefined;
  @Input() workoutplans: WorkoutPlan[] | undefined;
  @Input() readonly: boolean = false;

  public showingWorkoutPlan: WorkoutPlan | undefined;
  public showingWorkoutPlanId: string | undefined;

  public days: {label: string, value: number}[] = [];

  public exercises = new FormControl<Exercise[]>([]);
  public workoutDayForm: FormGroup = new FormGroup({
    name: new FormControl(),
    trainingDay: new FormControl(0),
    exercises: this.exercises
  });

  constructor(
    private workoutplanService: WorkoutplanService
  ) { }

  ngOnInit() {
    this.initFormDataBinding();
    this.initInputData();
  }

  private initInputData() {
    this.workoutplanService.workoutPlan$.subscribe(workoutplan => {
      this.showingWorkoutPlan = workoutplan;
      this.showingWorkoutPlanId = workoutplan.id;
      this.workoutDayForm.controls['trainingDay'].setValue(workoutplan?.workoutDays[0]?.day ?? 0);
      this.exercises.setValue(workoutplan.workoutDays[0].exercises ?? []);

      this.days = [];
      if (this.readonly) {
        const days = this.showingWorkoutPlan?.workoutDays?.length ?? 0
        for (let i = 0; i < days; i++) {
          const day = this.showingWorkoutPlan?.workoutDays[i].day ?? i;
          this.days.push({label: Days[day], value: day});
        }
        this.workoutDayForm.controls['trainingDay'].setValue(this.days[0].value);
      } else {
        for (let i = 0; i < 7; i++) {
          this.days.push({label: Days[i], value: i});
        }
      }
    });

  }

  private initFormDataBinding() {
    this.workoutDayForm.controls['name'].valueChanges.subscribe(name => {
      this.showingWorkoutPlan!.name = name;
    })

    this.workoutDayForm.controls['exercises'].valueChanges.subscribe(exercises => {
      const day = this.workoutDayForm.controls['trainingDay'].value;
      let workoutDay = this.showingWorkoutPlan!.workoutDays.find(x => x.day == day);
      if (workoutDay == null) {
        workoutDay = new WorkoutDay();
        workoutDay.day = day;
      }

      workoutDay.exercises = exercises;
    })

    this.workoutDayForm.controls['trainingDay'].valueChanges.subscribe(day => {
      const workoutDay = this.showingWorkoutPlan!.workoutDays.find(x => x.day == day);
      let exercises;
      if (workoutDay == undefined) {
        let newWorkoutDay = new WorkoutDay();
        newWorkoutDay.day = day;

        this.showingWorkoutPlan!.workoutDays.push(newWorkoutDay)
        exercises = newWorkoutDay.exercises;
      } else {
        exercises = workoutDay.exercises;
      }

      this.workoutDayForm.controls['exercises'].setValue(exercises);
    })
  }

  public switchWorkoutPlanToShow(workoutPlanId: any) {
    const workoutPlan = this.workoutplans?.find(x => x.id == workoutPlanId);
    if (workoutPlan) {
      this.workoutplanService.setWorkoutPlan(workoutPlan);
    }
  }

}
