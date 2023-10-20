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
import { MatButtonModule } from '@angular/material/button';
import { SwapRoleConfirmationDialogComponent } from './Components/general-settings/Components/swap-role-confirmation-dialog/swap-role-confirmation-dialog.component';
import { MatSelectModule } from '@angular/material/select';
import { SettingSelectionRowComponent } from './Components/general-settings/Components/setting-selection-row/setting-selection-row.component';
import { SettingToggleRowComponent } from './Components/general-settings/Components/setting-toggle-row/setting-toggle-row.component';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    UIElementsModule,
    BrowserAnimationsModule,
    SettingsRoutingModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatSelectModule,
    MatSlideToggleModule
  ],
  declarations: [
    SettingsComponent,
    GeneralSettingsComponent,
    SwapRoleConfirmationDialogComponent,
    SwapRoleConfirmationDialogComponent,
    SettingSelectionRowComponent,
    SettingToggleRowComponent
  ],
  bootstrap: [SettingsComponent]
})
export class SettingsModule { }
