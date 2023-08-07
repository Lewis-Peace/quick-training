import { Injectable } from '@angular/core';
import { Response } from '../../Model/Response'

@Injectable({
  providedIn: 'root'
})
export class ApiCallsService {

  public jwtToken: string | null = "";

  public async apiGet(url: string) {
    let response = new Response();
    try {
      const response = await fetch(url,
        {
          method: 'GET',
          headers: {
            'Authorization': `Bearer ${this.jwtToken}`
          }
        });
      if (!response.ok) {
        throw new Error(`Error on post call: ${response.status}`);
      }
      const result = (await response.json()) as Response;
      return result;
    } catch (error) {

      response.result = false;
      if (error instanceof Error) {
        console.error(`Error ${error.message}`)
        response.notes = error.message;
        return response;
      } else {
        console.error(`Unexpected error: `, error)
        response.notes = 'Unexpected error';
        return response;
      }
    }
  }

  public async apiPost(url: string, body: object) {
    let response = new Response();
    try {
      const response = await fetch(url,
        {
          method: 'POST',
          body: JSON.stringify(body),
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${this.jwtToken}`
          }
        });
      if (!response.ok) {
        throw new Error(`Error on post call: ${response.status}`);
      }
      const result = (await response.json()) as Response;
      return result;
    } catch (error) {

      response.result = false;
      if (error instanceof Error) {
        console.error(`Error ${error.message}`)
        response.notes = error.message;
        return response;
      } else {
        console.error(`Unexpected error: `, error)
        response.notes = 'Unexpected error';
        return response;
      }
    }
  }
}
