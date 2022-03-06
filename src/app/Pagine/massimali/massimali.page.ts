import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit } from '@angular/core';
import { AlertController, ModalController } from '@ionic/angular';
import { DatabaseHandler } from 'src/app/Tipi/DatabaseHandler';
import { Esercizio } from 'src/app/Tipi/esercizio';
import { AddMassimalePage } from '../add-massimale/add-massimale.page';

@Component({
  selector: 'app-massimali',
  templateUrl: './massimali.page.html',
  styleUrls: ['./massimali.page.scss'],
})
export class MassimaliPage implements OnInit {

  public massimali: Esercizio[] = []

  alertController: AlertController;
  modalController: ModalController;
  database: DatabaseHandler<Esercizio>
  public reorderState: boolean = true
  translate: TranslateService

  constructor(alertController: AlertController, modalController: ModalController, translate: TranslateService) {
    this.modalController = modalController
    this.alertController = alertController
    this.translate = translate
  }

  ngOnInit() {
    this.database = new DatabaseHandler<Esercizio>('massimali.json')
    this.load()
  }

  private async load() {
    this.massimali = await this.database.readDatabase()
  }

  public reorder(event) {
    this.massimali = this.swap(this.massimali, event.detail.from, event.detail.to)
    this.database.writeDatabase(this.massimali)
    event.detail.complete()
  }

  private swap(list: Esercizio[], from, to): Esercizio[] {
    if (to >= list.length) {
      var k = to - list.length + 1;
      while (k--) {
        list.push(undefined);
      }
  }
  list.splice(to, 0, list.splice(from, 1)[0]);
  return list;
  }

  public async addMassimale(esercizio: Esercizio) {
    const modal = await this.modalController.create({
      component: AddMassimalePage,
      componentProps: {
        esercizio: esercizio
      }
    })
    modal.onDidDismiss()
      .then((esercizio) => {
        if (esercizio.data !== undefined) {
          this.massimali.push(esercizio.data)
          this.database.add(esercizio.data)
        }
      })
    return await modal.present()
  }

  public delete(esercizio: Esercizio) {
    const index = this.massimali.indexOf(esercizio)
    this.massimali = this.massimali.slice(0, index).concat(this.massimali.splice(index + 1))
    this.database.delete(esercizio)
  }

  public modify(esercizio: Esercizio) {
    this.addMassimale(esercizio)
    this.delete(esercizio)
  }

  public async presentAlert(esercizio: Esercizio) {
    const alert = await this.alertController.create({
      header: 'L\' 80% di ' + esercizio.type + ' è ' + esercizio.peso * 0.8
    })
    return await alert.present()
  }

}
