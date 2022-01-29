import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { MassimaliPageRoutingModule } from './massimali-routing.module';

import { MassimaliPage } from './massimali.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    MassimaliPageRoutingModule,
  ],
  declarations: [MassimaliPage]
})
export class MassimaliPageModule {}
