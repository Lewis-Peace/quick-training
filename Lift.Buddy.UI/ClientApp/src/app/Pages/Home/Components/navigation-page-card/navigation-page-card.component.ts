import { Component, Input, OnInit } from '@angular/core';
import { NavigationMenu } from 'src/app/Model/NavigationMenu';
import { ApiCallsService } from 'src/app/Services/Utils/api-calls.service';

@Component({
  selector: 'app-navigation-page-card',
  templateUrl: './navigation-page-card.component.html',
  styleUrls: ['./navigation-page-card.component.css']
})
export class NavigationPageCardComponent implements OnInit {

  @Input() route: NavigationMenu | null = null;
  @Input() index: number = -1;

  constructor(
  ) { }

  ngOnInit() {
  }

  public isLoggedIn: boolean = ApiCallsService.jwtToken != null;

  public getRoutePath() {
    return [''].concat(this.route?.path.split('/') ?? []);
  }

  public isLoginRequired() {
    switch (this.route?.name) {
      case 'search':
        return false;
      case 'settings':
        return false;

      default:
        return true;
    }
  }

}
