import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageStructureComponent } from '../Components/page-structure/page-structure.component';
import { LeftMenuComponent } from '../Components/left-menu/left-menu.component';
import { HeaderComponent } from './header/header.component';
import { NavigationMenuComponent } from './navigation-menu/navigation-menu.component';
import { StarsRatingComponent } from './stars-rating/stars-rating.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTreeModule } from '@angular/material/tree';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { WorkoutNavigationMenuComponent } from './workout-navigation-menu/workout-navigation-menu.component';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    MatTreeModule,
    AppRoutingModule,
    MatButtonModule
  ],
  declarations: [
    HeaderComponent,
    LeftMenuComponent,
    NavigationMenuComponent,
    PageStructureComponent,
    StarsRatingComponent
  ],
  exports: [
    HeaderComponent,
    LeftMenuComponent,
    NavigationMenuComponent,
    PageStructureComponent,
    StarsRatingComponent
  ]
})
export class UIElementsModule { }
