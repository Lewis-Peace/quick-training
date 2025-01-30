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
import { UserService } from 'src/app/Services/user.service';
import { User } from 'src/app/Model/User';

@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.css']
})
export class GeneralSettingsComponent implements OnInit {

  constructor(
    private dialogService: DialogService,
    private loadingVisService: LoadingVisualizationService,
    private snackbarService: SnackBarService,
    private settingsService: SettingsService,
    private userService: UserService
  ) { }

  async ngOnInit() {
    this.loadingVisService.setIsLoading(true);
    await this.initSettings();
    await this.initLabels();
    await this.initUserInfo();
    this.loadingVisService.setIsLoading(false);
  }

  ngOnDestroy() {
  }

  private async initSettings() {
    const settings = await this.settingsService.getSettings();
    if (!settings.result) {
      this.settings = new Settings();
      return;
    }
    this.settings = settings.body[0];
  }

  private async initLabels() {
    const langLabelsResp = await this.settingsService.getLanguageLabels();
    if (!langLabelsResp.result) {
      this.snackbarService.operErrorSnackbar(`Error loading labels.`);
    }

    this.languages = langLabelsResp.body;
    
    const UOMLabelResp = await this.settingsService.getUnitOfMeasureLabels();
    if (!UOMLabelResp.result) {
      this.snackbarService.operErrorSnackbar(`Error loading labels.`);
    }

    this.UOMs = UOMLabelResp.body;
  }

  private async initUserInfo() {
    const response = await this.userService.getUserData();
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load user information. ${response.notes}`);
    }

    this.user = response.body[0];
  }

  public settings: Settings | undefined;
  public user: User | undefined;
  public isLoading: boolean = false;

  public languages: string[] = [];

  public UOMs: string[] = [];

  public generalSettingsForm: FormGroup = new FormGroup({
    UOM: new FormControl(0),
    language: new FormControl(0)
  });

  public swapRole() {
    const dialogRef = this.dialogService.openCenterDialog(SwapRoleConfirmationDialogComponent, {});

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.user!.isTrainer = !this.user!.isTrainer;
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

    this.snackbarService.openSuccessSnackbar(`Settings saved.`)
  }
}
