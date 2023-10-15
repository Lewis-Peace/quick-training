import { Component, OnInit } from '@angular/core';
import { Settings } from 'src/app/Model/Settings';
import { SettingsService } from 'src/app/Services/settings.service';

@Component({
  selector: 'app-Settings',
  templateUrl: './Settings.component.html',
  styleUrls: ['./Settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(
  ) { }

  ngOnInit() {
  }

}
