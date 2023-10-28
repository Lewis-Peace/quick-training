import { Review } from './../Model/Review';
import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { WorkoutPlan } from '../Model/WorkoutPlan';
import { BehaviorSubject } from 'rxjs';
import { SnackBarService } from './Utils/snack-bar.service';
import { LoginService } from './login.service';
import { User } from '../Model/User';

@Injectable({
	providedIn: 'root'
})

export class WorkoutplanService {

	private defaultUrl: string = 'api/WorkoutPlan';

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

  //#region WorkoutPlan Observable
	private _workoutPlan: WorkoutPlan | undefined
	private workoutPlan = new BehaviorSubject<WorkoutPlan>(new WorkoutPlan());
	public workoutPlan$ = this.workoutPlan.asObservable();

	public getWorkoutPlan() {
		return this._workoutPlan
	}

	public setWorkoutPlan(workoutPlan: WorkoutPlan) {
		this._workoutPlan = workoutPlan;
		this.workoutPlan.next(workoutPlan);
	}
  //#endregion

	public async getWorkoutPlanById(id: string) {
		return await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl + `/${id}`);
	}

	public async getWorkoutPlanByUser() {
		return await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl);
	}

	public async getWorkoutPlansCreatedByUser(userId: string) {
		return await this.apiService.apiGet<WorkoutPlan>(this.defaultUrl + `/created-by/${userId}`);
	}

	public async getWorkoutPlanSubscribers(workoutPlan: WorkoutPlan) {
		return await this.apiService.apiGet<User>(this.defaultUrl + `/subscribers/${workoutPlan.id}`);
	}

	public async setWorkoutPlanSubscribers(workoutPlan: WorkoutPlan, users: User[]) {
		return await this.apiService.apiPost<User>(this.defaultUrl + `/subscribers/${workoutPlan.id}`, users);
	}

	public async addWorkoutPlan(workout: WorkoutPlan) {
		try {
			if (!workout) {
				throw new Error('No workout to save');
			}

			workout.createdBy = this.loginService.userId;
			let response = await this.apiService.apiPost(this.defaultUrl, workout);

			if (!response.result) {
				throw new Error(`${response.notes}`);
			}

			this.snackbarService.openSuccessSnackbar('Workout saved')
		} catch (e) {
			this.snackbarService.operErrorSnackbar(`Failed to save workout schedule. Error: ${e}`);
		}
	}

	public async updateWorkoutPlan(workout: WorkoutPlan) {
		try {
			if (!workout) {
				throw new Error('No workout to save');
			}

			let response = await this.apiService.apiPut(this.defaultUrl, workout);

			if (!response.result) {
				throw new Error(`${response.notes}`);
			}

			this.snackbarService.openSuccessSnackbar('Workout saved')
		} catch (e) {
			this.snackbarService.operErrorSnackbar(`Failed to save workout schedule. Error: ${e}`);
		}
	}

	public async deleteWorkoutPlan(workoutId: string) {
		return this.apiService.apiDelete(this.defaultUrl, { workoutId });
	}

  public async reviewWorkoutPlan(review: Review) {
    return await this.apiService.apiPut<WorkoutPlan>(this.defaultUrl + '/review', review);
  }
}
