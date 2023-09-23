import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-general-settings',
  templateUrl: './general-settings.component.html',
  styleUrls: ['./general-settings.component.css']
})
export class GeneralSettingsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  public generalSettingsForm: FormGroup = new FormGroup({
    UOM: new FormControl(false)
  });


}
