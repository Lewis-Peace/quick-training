import { Frontpage } from './../Model/Frontpage';
import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { CRUD } from '../Model/Enums/CRUD';

@Injectable({
  providedIn: 'root'
})
export class FrontpageService {

  constructor(
    private apiService: ApiCallsService
  ) { }

  private defaultUrl = 'api/Frontpage';

  public async getFrontpage() {
    const response = await this.apiService.apiGet<Frontpage>(this.defaultUrl);
    return response;
  }

  public async addFrontpage(frontpage: Frontpage) {
    const response = await this.apiService.apiPost<Frontpage>(this.defaultUrl, frontpage);
    return response;
  }

  public async updateFrontpage(frontpage: Frontpage) {
    const response = await this.apiService.apiPut<Frontpage>(this.defaultUrl, frontpage);
    return response;
  }
}
