import { Component, Input, OnInit } from '@angular/core';
import { SubscriptionState } from 'src/app/Model/Enums/SubscriptionState';
import { Subscription } from 'src/app/Model/Subscription';
import { User } from 'src/app/Model/User';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';
import { LoginService } from 'src/app/Services/login.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css', '../../../../app.component.scss']
})
export class SearchResultComponent implements OnInit {

  @Input() user: User | undefined;
  public requestSent: boolean = false;
  public isLoading: boolean = false;

  constructor(
    private userService: UserService,
    private snackbarService: SnackBarService,
    private loginService: LoginService
  ) { }

  ngOnInit() {
  }

  public get isSubscribed(): boolean {
    return this.user?.subscriptionState == SubscriptionState.Subscribed;
  }

  public get isLoggedin(): boolean {
    return this.user?.subscriptionState != SubscriptionState.Undefined;
  }

  async subscribe(trainer: User | undefined) {
    if (!trainer) {
      this.snackbarService.operErrorSnackbar(`Failed to subscribe to trainer`);
      this.isLoading = false;
      return;
    }
    this.isLoading = true;
    let subscription = new Subscription()
    subscription.trainerId = trainer.id;
    subscription.athleteId = this.loginService.userId;
    
    const response = await this.userService.subscribeToTrainer(subscription);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to subscribe to ${trainer.credentials.username}. Ex: ${response.notes}`);
      this.isLoading = false;
      return;
    }

    this.user!.subscriptionState = SubscriptionState.Subscribed;
    this.isLoading = false;
  }

  async unsubscribe(trainer: User | undefined) {
    if (!trainer) {
      this.snackbarService.operErrorSnackbar(`Failed to unsubscribe to trainer`);
      this.isLoading = false;
      return;
    }
    this.isLoading = true;
    let subscription = new Subscription()
    subscription.trainerId = trainer.id;
    subscription.athleteId = this.loginService.userId;

    const response = await this.userService.unsubscribeToTrainer(subscription);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to unsubscribe to ${trainer.credentials.username}. Ex: ${response.notes}`);
      this.isLoading = false;
      return;
    }

    this.user!.subscriptionState = SubscriptionState.Unsubscribed;
    this.isLoading = false;
  }

}
