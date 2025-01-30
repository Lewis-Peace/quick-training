import { Injectable } from '@angular/core';
import { Response } from '../../Model/Response'

@Injectable({
    providedIn: 'root'
})
export class ApiCallsService {

    public static jwtToken: string | undefined;
    private apiUrl: string = "http://localhost:5200/";

    public async apiGet<T>(apiEndpoint: string): Promise<Response<T>> {
        let response = new Response<T>();
        try {
            const response = await fetch(this.apiUrl + apiEndpoint,
                {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${ApiCallsService.jwtToken}`
                    }
                });

            if (!response.ok) {
                throw new Error(`Error on get call ${apiEndpoint}: ${response.status}`);
            }

            const result = (await response.json()) as Response<T>;
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

    public async apiPost<T>(url: string, body: object): Promise<Response<T>> {
        return await this.apiGenericPost(url, body, "POST");
    }

    public async apiPut<T>(url: string, body: object): Promise<Response<T>> {
        return await this.apiGenericPost(url, body, "PUT");
    }

    public async apiDelete<T>(url: string, body: object): Promise<Response<T>> {
        return await this.apiGenericPost(url, body, "DELETE");
    }

    private async apiGenericPost<T>(url: string, body: object, type: string): Promise<Response<T>> {
        let apiResponse = new Response<T>();
        try {
            const response = await fetch(this.apiUrl + url,
                {
                    method: type,
                    body: JSON.stringify(body),
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${ApiCallsService.jwtToken}`
                    }
                });

            if (!response.ok) {
                throw new Error(`Error on post call: ${response.statusText} ${response.status}`);
            }

            if (response.status != 204) {
                const data = await response.json();
                const result = data as Response<T>;
                return result;
            }

            apiResponse.result = true;
        } catch (error) {
            apiResponse.result = false;

            if (error instanceof Error) {
                console.error(`Error ${error.message}`)
                apiResponse.notes = error.message;
            } else {
                console.error(`Unexpected error: `, error)
                apiResponse.notes = 'Unexpected error';
            }
        }
        return apiResponse;
    }
}
