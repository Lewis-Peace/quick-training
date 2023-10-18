import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { User } from '../Model/User';
import { WorkoutPlan } from '../Model/WorkoutPlan';
import { Frontpage } from '../Model/Frontpage';

@Injectable({
  providedIn: 'root'
})
export class TrainerService {

  constructor(
    private apiService: ApiCallsService
  ) { }

  private defaultUrl: string = "api/Trainer";

  public getMyAthletes() {
    const response = this.apiService.apiGet<User>(this.defaultUrl + '/athletes');
    return response;
  }

  public CRUDFrontpage(frontpage: Frontpage) {
    const response = this.apiService.apiPost<Frontpage>(this.defaultUrl + '/frontpage', frontpage);
    return response;
  }

  public removeAthleteSubscription(athlete: User) {
    const response = this.apiService.apiDelete<WorkoutPlan>(this.defaultUrl + '/athletes', athlete);
    return response;
  }

}
