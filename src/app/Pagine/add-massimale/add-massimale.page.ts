import { TranslateService } from '@ngx-translate/core';
import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Esercizio } from 'src/app/Tipi/esercizio';

@Component({
  selector: 'app-add-massimale',
  templateUrl: './add-massimale.page.html',
  styleUrls: ['./add-massimale.page.scss'],
})
export class AddMassimalePage implements OnInit {

  @Input() esercizio: Esercizio

  modalController: ModalController
  public type: string = ''
  public peso: number = 0
  public rep: number = 0
  translate

  constructor(modalController: ModalController, translate: TranslateService) {
    this.modalController = modalController
    this.translate = translate
  }

  ngOnInit() {
    if (this.esercizio !== undefined) {
      this.type = this.esercizio.type
      this.peso = this.esercizio.peso
      this.rep = this.esercizio.rep
    }
  }

  public close(add: boolean) {
    if (add) {
      const es = new Esercizio(this.type, this.peso, this.rep)
      this.modalController.dismiss(es)
    } else {
      this.modalController.dismiss(undefined)
    }
  }
}
