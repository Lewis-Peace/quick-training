import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AllenamentoPageRoutingModule } from './allenamento-routing.module';

import { AllenamentoPage } from './allenamento.page';

import { PreviewAnyFile } from '@ionic-native/preview-any-file/ngx';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AllenamentoPageRoutingModule
  ],
  declarations: [AllenamentoPage],
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    PreviewAnyFile
  ]
})
export class AllenamentoPageModule {}
