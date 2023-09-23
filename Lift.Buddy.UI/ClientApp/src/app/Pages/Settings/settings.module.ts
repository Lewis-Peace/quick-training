import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingsComponent } from './settings.component';
import { UIElementsModule } from '../Components/UI.Elements.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { SettingsRoutingModule } from './setings-routing.module';
import { GeneralSettingsComponent } from './Components/general-settings/general-settings.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    UIElementsModule,
    BrowserAnimationsModule,
    RouterModule,
    SettingsRoutingModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule
  ],
  declarations: [
    SettingsComponent,
    GeneralSettingsComponent
  ],
  bootstrap: [SettingsComponent]
})
export class SettingsModule { }
