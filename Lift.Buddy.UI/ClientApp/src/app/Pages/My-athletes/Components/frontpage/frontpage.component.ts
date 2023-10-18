import { Component, ElementRef, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {

  @ViewChild('header') header: ElementRef<HTMLDivElement> | undefined;

  constructor() { }

  public frontpageForm: FormGroup = new FormGroup({
    description: new FormControl('', [Validators.required, Validators.maxLength(500)])
  });

  ngOnInit() {
    if (!this.header) return;
  }

  public getDescriptionError() {
    if (this.frontpageForm.controls['description'].hasError('required')) {
      return 'You must enter a value';
    }
    return this.frontpageForm.controls['description'].hasError('maxlength') ? 'Max length is 500' : '';
  }

  public save() {
    
  }

}
