import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UnitOfMeasure, UnitOfMeasureToString } from 'src/app/Model/Enums/UnitOfMeasures';
import { PersonalRecord } from 'src/app/Model/PersonalRecord';

@Component({
  selector: 'app-pr-exercise',
  templateUrl: './pr-exercise.component.html',
  styleUrls: ['./pr-exercise.component.css']
})

export class PrExerciseComponent implements OnInit {

  @Input() exercise: PersonalRecord = new PersonalRecord();
  @Input() index: number = -1;
  @Output() onRemove: EventEmitter<null> = new EventEmitter();

  public exerciseForm: FormGroup = new FormGroup({
    name: new FormControl(''),
    weight: new FormControl(undefined, [Validators.min(0)]),
    unitOfMeasure: new FormControl(UnitOfMeasure.KG)
  });

  constructor() { }

  public UOMs: UnitOfMeasure[] = []

  ngOnInit() {
    this.initFormValues();
    this.initUOMs();
    this.initDataBinding();
  }

  private initFormValues() {
    this.exerciseForm.controls['name'].setValue(this.exercise.exerciseName);
    this.exerciseForm.controls['weight'].setValue(this.exercise.weight);
    this.exerciseForm.controls['unitOfMeasure'].setValue(this.exercise.unitOfMeasure);
  }

  private initUOMs() {
    this.UOMs.push(UnitOfMeasure.KG, UnitOfMeasure.LB);
  }

  private initDataBinding() {
    this.exerciseForm.controls['name'].valueChanges.subscribe(name => {
      this.exercise.exerciseName = name;
    });
    this.exerciseForm.controls['weight'].valueChanges.subscribe(weight => {
      this.exercise.weight = weight;
    });
    this.exerciseForm.controls['unitOfMeasure'].valueChanges.subscribe(unitOfMeasure => {
      this.exercise.unitOfMeasure = unitOfMeasure;
    });
  }

  public unitOfMeasureToString(uom: number) {
    return UnitOfMeasureToString(uom);
  }

}
