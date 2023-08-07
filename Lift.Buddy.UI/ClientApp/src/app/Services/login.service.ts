import { ApiCallsService } from './Utils/api.calls.service';
import { LoginCredetials } from '../Model/LoginCredentials';

import { Injectable } from '@angular/core';
import { Response } from '../Model/Response';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor (
    private apiCalls: ApiCallsService
  ) { }

  public async login(loginCredentials: LoginCredetials): Promise<Response> {
    const response = await this.apiCalls.apiPost("http://localhost:5200/api/Login", loginCredentials);

    if (response.result) {
      this.apiCalls.jwtToken = response.body;
    }
    return response;
  }
}
