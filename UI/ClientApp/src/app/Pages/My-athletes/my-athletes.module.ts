import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyAthletesMainComponent } from './Pages/my-athletes-main/my-athletes-main.component';
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
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { FrontpageComponent } from './Pages/frontpage/frontpage.component';
import { RequestsComponent } from './Pages/requests/requests.component';
import { TextFieldModule } from '@angular/cdk/text-field';

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
    MatIconModule,
    TextFieldModule,
    MatTableModule,
    MatPaginatorModule
  ],
  declarations: [
    MyAthletesComponent,
    MyAthletesMainComponent,
    MyAthletesRightMenuComponent,
    FrontpageComponent,
    RequestsComponent
  ]
})
export class MyAthletesModule { }
