import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DialogService } from 'src/app/Services/Utils/dialog.service';
import { LoginService } from 'src/app/Services/login.service';
import { SwapRoleConfirmationDialogComponent } from './Components/swap-role-confirmation-dialog/swap-role-confirmation-dialog.component';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';
import { Subscription } from 'rxjs';
import { SettingsService } from 'src/app/Services/settings.service';
import { Settings } from 'src/app/Model/Settings';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';

@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.css']
})
export class GeneralSettingsComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private dialogService: DialogService,
    private loadingVisService: LoadingVisualizationService,
    private snackbarService: SnackBarService,
    private settingsService: SettingsService
  ) { }

  ngOnInit() {
    this.loadingSubscription = this.loadingVisService.$isLoading.subscribe(loading => {
      this.isLoading = loading;
    })
    this.initSettings();
  }

  ngOnDestroy() {
    this.loadingSubscription?.unsubscribe();
  }

  private async initSettings() {
    const settings = await this.settingsService.getSettings();
    if (!settings.result) {
      this.settings = new Settings();
      return;
    }
    this.settings = settings.body[0];
  }

  public settings: Settings | undefined;
  public isTrainer = this.loginService.isTrainer;
  public isLoading: boolean = false;
	public loadingSubscription: Subscription | undefined;

  public language = 'en';
  public languages = [
    {value: 'en', label: 'English'},
    {value: 'it', label: 'Italiano'},
  ];

  public UOM = true;
  public UOMs = [
    {value: 'kg', label: 'KG'},
    {value: 'lb', label: 'LB'},
  ];

  public generalSettingsForm: FormGroup = new FormGroup({
    UOM: new FormControl('kg'),
    language: new FormControl('en')
  });

  public swapRole() {
    const dialogRef = this.dialogService.openCenterDialog(SwapRoleConfirmationDialogComponent, {});

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.isTrainer = !this.isTrainer;
      }
    })
  }

  public getFormControl(controlName: string) {
    return this.generalSettingsForm.get(controlName) as FormControl;
  }

  public async reset() {
    const response = await this.settingsService.deleteSettings()
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to reset settings. Ex: ${response.notes}`)
    }
  }

  public async save() {
    if (!this.settings) {
      this.snackbarService.operErrorSnackbar('Failed to save settings');
      return;
    }

    const response = await this.settingsService.updateSettings(this.settings);
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to update settings. Ex: ${response.notes}`)
    }
  }
}
