import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { WorkoutPlan } from '../Model/WorkoutPlan';
import { BehaviorSubject } from 'rxjs';
import { SnackBarService } from './Utils/snack-bar.service';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutplanService {

constructor(
  private apiService: ApiCallsService,
  private loginService: LoginService,
  private snackbarService: SnackBarService
) { }

//#region Day observable
private _day: number | undefined
private day = new BehaviorSubject<number>(0);
public day$ = this.day.asObservable();

public getDay() {
  return this._day
}

public setDay(day: number) {
  this._day = day;
  this.day.next(day);
}
//#endregion

private defaultUrl: string = 'api/WorkoutPlan';

public async getWorkoutPlanById(id: number) {
  const response = await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl + `/${id}`);
  return response;
}

public async getWorkoutPlanByUser() {
  const response = await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl);
  return response;
}

public async getWorkoutPlansCreatedByUsername() {
  const response = await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl + `/CreatedBy/${this.loginService.currentUsername}`);
  return response;
}

public async getWorkoutPlanSubscribersCount(workoutPlan: WorkoutPlan) {
  const response = await this.apiService.apiGet<number>(this.defaultUrl + `/subscribers/${workoutPlan.id}`);
  return response;
}

public async saveWorkoutPlan(workout: WorkoutPlan) {
  try {
    if (!workout) {
      throw new Error('No workout to save');
    }
    let saveResp;
    if (!workout.id) {
      workout.createdBy = this.loginService.currentUsername;
      saveResp = await this.apiService.apiPost('api/WorkoutPlan', workout);
    } else {
      saveResp = await this.apiService.apiPut('api/WorkoutPlan', workout);
    }
    if (!saveResp.result) {
      throw new Error(`${saveResp.notes}`);
    }
    this.snackbarService.openSuccessSnackbar('Workout saved')
  } catch (e) {
    this.snackbarService.operErrorSnackbar(`Failed to save workout schedule. Error: ${e}`);
  }
}

public async deleteWorkoutPlan(workout: WorkoutPlan) {
  const response = await this.apiService.apiDelete(this.defaultUrl, workout);
  return response;
}

}
