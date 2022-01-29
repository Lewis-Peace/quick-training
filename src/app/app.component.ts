import { Component } from '@angular/core';
import { Filesystem } from '@capacitor/filesystem';
import { DatabaseHandler } from './Tipi/DatabaseHandler';
import { Esercizio } from './Tipi/esercizio';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  constructor() {}
  
  ngOnInit() {
  }

}
