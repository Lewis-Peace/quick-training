import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AllenamentoPage } from './allenamento.page';

const routes: Routes = [
  {
    path: '',
    component: AllenamentoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AllenamentoPageRoutingModule {}
