import { Component } from '@angular/core';
import { Filesystem } from '@capacitor/filesystem';
import { DatabaseHandler } from '../Tipi/DatabaseHandler';
import { Esercizio } from '../Tipi/esercizio';

@Component({
  selector: 'app-tabs',
  templateUrl: 'tabs.page.html',
  styleUrls: ['tabs.page.scss']
})
export class TabsPage {

  constructor() {}

  ngOnInit() {
    this.isDatabaseReady()
  }

  private async isDatabaseReady() {
    const database = new DatabaseHandler<Esercizio>('massimali.json')
    Filesystem.stat({
      path: database.filename,
      directory: database.directory
    })
    .then(() => console.log('File exists'))
    .catch(() => {database.initDatabase()})
  }
}
