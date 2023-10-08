import { Component, OnInit } from '@angular/core';
import { NavigationMenu } from 'src/app/Model/NavigationMenu';
import { trainerNavigationMenu } from 'src/app/routes';

@Component({
  selector: 'app-my-athletes-right-menu',
  templateUrl: './my-athletes-right-menu.component.html',
  styleUrls: ['./my-athletes-right-menu.component.css']
})
export class MyAthletesRightMenuComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


  public navigationData: NavigationMenu[] = trainerNavigationMenu;

}
