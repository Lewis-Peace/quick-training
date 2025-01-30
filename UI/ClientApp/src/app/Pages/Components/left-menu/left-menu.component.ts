import { Component, OnInit } from '@angular/core';
import { NavigationMenu } from 'src/app/Model/NavigationMenu';
import { generalNavigationMenu } from 'src/app/routes';

@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  public navigationData: NavigationMenu[] = generalNavigationMenu;

}
