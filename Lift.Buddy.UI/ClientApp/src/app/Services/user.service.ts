import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { User } from '../Model/User';
import { SnackBarService } from './Utils/snack-bar.service';
import { Subscription } from '../Model/Subscription';
import { WorkoutPlan } from '../Model/WorkoutPlan';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public defaultUrl: string = 'api/User';

  //#region Observables

	private _userSuggestion: User[] | undefined
	private userSuggestion = new BehaviorSubject<User[]>([]);
	public userSuggestion$ = this.userSuggestion.asObservable();

  public getUserSuggestion() {
    return this._userSuggestion;
  }

  public async setUserSuggestion(username: string) {
    const response = await this.getUsersByUsername(username);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Error loading suggested users. Ex: ${response.notes}`);
    }
    this.userSuggestion.next(response.body);
  }

  //#endregion

  constructor(
    private apiCalls: ApiCallsService,
    private snackbarService: SnackBarService
  ) { }

  public async getUsersByUsername(username: string) {
    return await this.apiCalls.apiGet<User>(`${this.defaultUrl}/${username}`);
  }

  public async getUserData() {
    return await this.apiCalls.apiGet<User>(this.defaultUrl);
  }

  public async updateUserData(userData: User) {
    return await this.apiCalls.apiPut<User>(this.defaultUrl, userData);
  }

  public async getAssignedWorkouts() {
    return await this.apiCalls.apiGet<WorkoutPlan>(this.defaultUrl + '/workouts');
  }
  
  public async getSubscriptions() {
    return await this.apiCalls.apiGet<Subscription>(this.defaultUrl + '/subscription');
  }
  public async subscribeToTrainer(subscription: Subscription) {
    return await this.apiCalls.apiPost<null>(this.defaultUrl + '/subscription', subscription);
  }
  public async unsubscribeToTrainer(subscription: Subscription) {
    return await this.apiCalls.apiDelete<null>(this.defaultUrl + '/subscription', subscription);
  }
}
