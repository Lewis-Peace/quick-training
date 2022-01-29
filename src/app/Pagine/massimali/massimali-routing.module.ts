import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MassimaliPage } from './massimali.page';

const routes: Routes = [
  {
    path: '',
    component: MassimaliPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MassimaliPageRoutingModule {}
