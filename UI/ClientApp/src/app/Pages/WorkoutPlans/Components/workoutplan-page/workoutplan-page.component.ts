import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Days } from 'src/app/Model/Enums/Days';
import { Exercise } from 'src/app/Model/Exercise';
import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { WorkoutPlan } from 'src/app/Model/WorkoutPlan';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
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

  @Output() onSave: EventEmitter<WorkoutPlan> = new EventEmitter();

  public showingWorkoutPlan: WorkoutPlan | undefined;
  public showingWorkoutPlanId: string | undefined;

  public days: {label: string, value: number}[] = [];

  public exercises = new FormControl<Exercise[]>([]);
  public workoutForm: FormGroup = new FormGroup({
    name: new FormControl(''),
    trainingDay: new FormControl(0),
    exercises: this.exercises
  });

  constructor(
    private workoutplanService: WorkoutplanService,
    private snackbarService: SnackBarService
  ) { }

  ngOnInit() {
    if (this.workoutplan) {
      this.showingWorkoutPlan = this.workoutplan;
    } else if (this.workoutplans && this.workoutplans.length != 0) {
      this.showingWorkoutPlan = this.workoutplans[0];
    } else {
      this.snackbarService.operErrorSnackbar('No workplan has been inserted properly.');
      return;
    }
    this.initWorkoutSwitch();
    this.initFormDataBinding();
    this.fillFrom(this.showingWorkoutPlan);
  }

  private fillFrom(workoutplan: WorkoutPlan) {
    this.workoutForm.controls['name'].setValue(workoutplan.name);
    const workoutDay = workoutplan.workoutDays[0];
    this.workoutForm.controls['trainingDay'].setValue(workoutDay.day);
    this.workoutForm.controls['exercises'].setValue(workoutDay.exercises);
  }

  private initWorkoutSwitch() {
    this.workoutplanService.workoutPlan$.subscribe(workoutplan => {
      this.showingWorkoutPlan = workoutplan;
      this.showingWorkoutPlanId = workoutplan?.id;
      this.workoutForm.controls['trainingDay'].setValue(workoutplan?.workoutDays[0]?.day ?? 0);
      this.exercises.setValue(workoutplan?.workoutDays[0].exercises ?? []);

      this.days = [];
      if (this.readonly) {
        const days = this.showingWorkoutPlan.workoutDays?.length ?? 0
        for (let i = 0; i < days; i++) {
          const day = this.showingWorkoutPlan?.workoutDays[i].day ?? i;
          this.days.push({label: Days[day], value: day});
        }
        this.workoutForm.controls['trainingDay'].setValue(this.days[0]?.value);
      } else {
        for (let i = 0; i < 7; i++) {
          this.days.push({label: Days[i], value: i});
        }
      }
    });

  }

  private initFormDataBinding() {
    this.workoutForm.controls['name'].valueChanges.subscribe(name => {
      this.showingWorkoutPlan!.name = name;
    })

    this.workoutForm.controls['exercises'].valueChanges.subscribe(exercises => {
      const day = this.workoutForm.controls['trainingDay'].value;
      let workoutDay = this.showingWorkoutPlan?.workoutDays.find(x => x.day == day);
      if (workoutDay == null) {
        workoutDay = new WorkoutDay();
        workoutDay.day = day;
      }

      workoutDay.exercises = exercises;
    })

    this.workoutForm.controls['trainingDay'].valueChanges.subscribe(day => {
      const workoutDay = this.showingWorkoutPlan?.workoutDays.find(x => x.day == day);
      let exercises;
      if (workoutDay == undefined) {
        let newWorkoutDay = new WorkoutDay();
        newWorkoutDay.day = day;

        this.showingWorkoutPlan?.workoutDays.push(newWorkoutDay)
        exercises = newWorkoutDay.exercises;
      } else {
        exercises = workoutDay.exercises;
      }

      this.workoutForm.controls['exercises'].setValue(exercises);
    })
  }

  public switchWorkoutPlanToShow(workoutPlanId: any) {
    const workoutPlan = this.workoutplans?.find(x => x.id == workoutPlanId);
    if (workoutPlan) {
      this.workoutplanService.setWorkoutPlan(workoutPlan);
    }
  }

  public save() {
    this.onSave.emit(this.showingWorkoutPlan);
  }

}
