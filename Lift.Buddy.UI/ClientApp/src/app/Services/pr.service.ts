import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { UserPR } from '../Model/UserPR';

@Injectable({
  providedIn: 'root'
})
export class PrService {

  constructor(
    private apiService: ApiCallsService
  ) { }

  private defaultUrl: string = "api/PR";

  public get() {
    const response = this.apiService.apiGet<UserPR>(this.defaultUrl);
    return response;
  }

  public savePR(userPR: UserPR, isUpdate: boolean) {
    let response;
    if (isUpdate) {
      response = this.apiService.apiPut<UserPR>(this.defaultUrl, userPR);
    } else {
      response = this.apiService.apiPost<UserPR>(this.defaultUrl, userPR);
    }
    return response;
  }

}
