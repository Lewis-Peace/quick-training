import { Injectable } from '@angular/core';
import { ApiCallsService } from './Utils/api-calls.service';
import { User } from '../Model/User';
import { BehaviorSubject } from 'rxjs';
import { SnackBarService } from './Utils/snack-bar.service';

@Injectable({
	providedIn: 'root'
})
export class SearchService {

	public defaultUrl: string = 'api/Search'

	constructor(
		private apiCalls: ApiCallsService,
		private snackbarService: SnackBarService
	) { }

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
		this.userSuggestion.next(response.body.slice(0, 10));
	}

	//#endregion

	public getUsersByUsername(username: string) {
		return this.apiCalls.apiGet<User>(`${this.defaultUrl}/${username}`);
	}

}
