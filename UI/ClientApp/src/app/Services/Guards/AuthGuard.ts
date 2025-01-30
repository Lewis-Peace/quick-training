import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  UrlTree,
  Navigation,
} from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from '../login.service';
import { SnackBarService } from '../Utils/snack-bar.service';
@Injectable({
  providedIn: 'root',
})
export class AuthGuard {
  constructor(
    private loginService: LoginService,
    private router: Router,
    private snackbarService: SnackBarService
  ) {}

  public previousPage: Navigation | null = null;

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | UrlTree | boolean {
    if (this.loginService.currentUsername == '') {
      this.snackbarService.operErrorSnackbar('Access denied, login is required');
      this.previousPage = this.router.getCurrentNavigation();
      this.router.navigate(['login']);
    }
    return true;
  }
}
