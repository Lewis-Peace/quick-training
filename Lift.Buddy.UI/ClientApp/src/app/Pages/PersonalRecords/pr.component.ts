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
    this.isLoagingSubscription = this.loadingService.$isLoading.subscribe(isLoading => this.isLoading = isLoading);
    await this.initUserPersonalRecord();
  }

  ngOnDestroy() {
    this.isLoagingSubscription?.unsubscribe();
  }

  private updateCount: number = 0
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
      return;
    }

    if (response.body.length == 0) {
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

    const response = await this.personalRecordService.savePersonalRecord({ toUpdate, toAdd });

    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`An exception occurred during save. Ex ${response.notes}`);
      return;
    }

    this.snackbarService.openSuccessSnackbar('Personal results succesfully saved.')
  }
}
