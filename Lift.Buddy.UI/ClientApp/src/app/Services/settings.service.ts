import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { Settings } from '../Model/Settings';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  private defaultUrl: string = 'api/settings';

  constructor(
    private apiService: ApiCallsService
  ) { }
  
  public async getSettings() {
    const response = await this.apiService.apiGet<Settings>(this.defaultUrl);
    return response;
  }
  public async createSettings() {
    const response = await this.apiService.apiPost<Settings>(this.defaultUrl, {});
    return response;
  }
  public async updateSettings(settings: Settings) {
    const response = await this.apiService.apiPut<Settings>(this.defaultUrl, settings);
    return response;
  }
  public async deleteSettings() {
    const response = await this.apiService.apiDelete<Settings>(this.defaultUrl, {});
    return response;
  }
  public async getLanguageLabels() {
    const response = await this.apiService.apiGet<string>(this.defaultUrl + '/labels-languages');
    return response;
  }
  public async getUnitOfMeasureLabels() {
    const response = await this.apiService.apiGet<string>(this.defaultUrl + '/labels-unit-of-measures');
    return response;
  }
}
