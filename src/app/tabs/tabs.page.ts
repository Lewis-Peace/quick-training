import { Component } from '@angular/core';
import { Filesystem } from '@capacitor/filesystem';
import { DatabaseHandler } from '../Tipi/DatabaseHandler';
import { Esercizio } from '../Tipi/esercizio';
import {TranslateService} from '@ngx-translate/core'

@Component({
  selector: 'app-tabs',
  templateUrl: 'tabs.page.html',
  styleUrls: ['tabs.page.scss']
})
export class TabsPage {

  translate: TranslateService
  Tab1

  constructor(translate: TranslateService) {
    this.translate = translate
    this.loadTranslatedWords(navigator.language)
  }

  loadTranslatedWords(language: string) {
    if (language.includes('en')) {
      this.translate.use('en-US')
    } else if (language.includes('it')) {
      this.translate.use('it-IT')
    } else {
      this.translate.use(language)
    }
  }

  ngOnInit() {
    this.isDatabaseReady()
  }

  private async isDatabaseReady() {
    const database = new DatabaseHandler<Esercizio>('massimali.json')
    Filesystem.stat({
      path: database.filename,
      directory: database.directory
    })
    .catch(() => {database.initDatabase()})
  }
}
