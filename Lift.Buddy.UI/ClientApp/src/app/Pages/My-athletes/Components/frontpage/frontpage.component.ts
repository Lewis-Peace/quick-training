import { animate, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CRUD } from 'src/app/Model/Enums/CRUD';
import { Frontpage } from 'src/app/Model/Frontpage';
import { User } from 'src/app/Model/User';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { FrontpageService } from 'src/app/Services/frontpage.service';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css'],
  animations: [
    trigger('backgroundButton', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('200ms', style({ opacity: 0.5 })),
      ]),
      transition(':leave', [
        animate('200ms', style({ opacity: 0 }))
      ])
    ]),
  ]
})
export class FrontpageComponent implements OnInit {

  public viewBackgroundButton: boolean = false;
  public frontpage: Frontpage = new Frontpage();
  public username: string | undefined;
  public isLoading: boolean = false;

  constructor(
    private frontpageService: FrontpageService,
    private snackbarService: SnackBarService,
    private loginService: LoginService,
    private loadingVisualizationService: LoadingVisualizationService
  ) { }

  public frontpageForm: FormGroup = new FormGroup({
    description: new FormControl('', [Validators.required, Validators.maxLength(500)])
  });

  async ngOnInit() {
    this.loadingVisualizationService.setIsLoading(true);
    await this.initFrontpage();
    this.username = this.loginService.username;
    this.loadingVisualizationService.setIsLoading(false);
  }

  private async initFrontpage() {
    const response = await this.frontpageService.getFrontpage();
    if (!response.result) {
      return;
    }
    if (response.body == null) {
      this.frontpage = new Frontpage();
    } else {
      this.frontpage = response.body[0];

      this.frontpageForm.controls['description'].setValue(this.frontpage.description);
    }
  }

  public getDescriptionError() {
    if (this.frontpageForm.controls['description'].hasError('required')) {
      return 'You must enter a value';
    }
    return this.frontpageForm.controls['description'].hasError('maxlength') ? 'Max length is 500' : '';
  }

  public async save() {
    let response;
    this.frontpage.description = this.frontpageForm.controls['description'].value;
    if (this.frontpage.state == CRUD.Create) {
      response = await this.frontpageService.addFrontpage(this.frontpage);
    } else {
      response = await this.frontpageService.updateFrontpage(this.frontpage);
    }
    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to save frontpage. Ex: ${response.notes}`);
    }
    this.snackbarService.openSuccessSnackbar('Frontpage saved correctly.');
  }

}
