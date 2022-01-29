import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AddMassimalePageRoutingModule } from './add-massimale-routing.module';

import { AddMassimalePage } from './add-massimale.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AddMassimalePageRoutingModule
  ],
  declarations: [AddMassimalePage]
})
export class AddMassimalePageModule {}
