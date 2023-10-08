import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyAthletesMainComponent } from './Components/my-athletes-main/my-athletes-main.component';
import { MyAthletesRoutingModule } from './my-athletes-routing.module';
import { UIElementsModule } from '../Components/UI.Elements.module';
import { RouterModule } from '@angular/router';
import { MyAthletesComponent } from './my-athletes.component';
import { BrowserModule } from '@angular/platform-browser';
import { MyAthletesRightMenuComponent } from './Components/my-athletes-right-menu/my-athletes-right-menu.component';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  imports: [
    CommonModule,
    MyAthletesRoutingModule,
    BrowserModule,
    RouterModule,
    UIElementsModule,
    MatCardModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  declarations: [
    MyAthletesComponent,
    MyAthletesMainComponent,
    MyAthletesRightMenuComponent
  ]
})
export class MyAthletesModule { }
