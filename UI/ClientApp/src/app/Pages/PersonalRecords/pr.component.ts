import { Subscription } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { PersonalRecord } from 'src/app/Model/PersonalRecord';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';
import { LoginService } from 'src/app/Services/login.service';
import { PersonalRecordService } from 'src/app/Services/pr.service';

@Component({
  selector: 'app-pr',
  templateUrl: './pr.component.html',
  styleUrls: ['./pr.component.css']
})

export class PersonalRecordComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private personalRecordService: PersonalRecordService,
    private snackbarService: SnackBarService,
    private loadingService: LoadingVisualizationService
  ) { }

  async ngOnInit() {
    await this.initUserPersonalRecord();
  }

  ngOnDestroy() {
  }

  private updateCount: number = 0
  private recordsToDelete: PersonalRecord[] = [];
  public isLoading: boolean = false;
  public isLoagingSubscription: Subscription | undefined;

  public prForm: FormGroup = new FormGroup({
    exercises: new FormControl<PersonalRecord[]>([])
  });

  private async initUserPersonalRecord() {
    this.loadingService.setIsLoading(true);
    const response = await this.personalRecordService.get();

    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load PR. Error ${response.notes}`)
      this.loadingService.setIsLoading(false);
      return;
    }

    if (response.body.length == 0) {
      this.loadingService.setIsLoading(false);
      return;
    }

    this.prForm.controls['exercises'].setValue(response.body);
    this.updateCount = response.body.length;
    this.loadingService.setIsLoading(false);
  }

  public addExercise() {
    let record = new PersonalRecord();
    record.userId = this.loginService.userId;

    this.prForm.controls['exercises'].value.push(record);
  }

  public async save() {
    const records = this.prForm.controls['exercises'].value;
    const toUpdate = records.slice(0, this.updateCount)
    const toAdd = records.slice(this.updateCount)
    const toRemove = this.recordsToDelete;

    const response = await this.personalRecordService.savePersonalRecord({ toUpdate, toAdd, toRemove});

    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`An exception occurred during save. Ex ${response.notes}`);
      return;
    }

    this.snackbarService.openSuccessSnackbar('Personal results succesfully saved.')
  }

  public removeExercise(index: number) {
    let newExerciseList: PersonalRecord[] = this.prForm.controls['exercises'].value;
    const deleteRecord = newExerciseList.splice(index, 1);
    this.recordsToDelete.push(deleteRecord[0]);
    if (index <= this.updateCount) {
      this.updateCount--;
    }
    this.prForm.controls['exercises'].setValue(newExerciseList)
  }
}
