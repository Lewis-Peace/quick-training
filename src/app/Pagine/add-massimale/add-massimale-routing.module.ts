import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AddMassimalePage } from './add-massimale.page';

const routes: Routes = [
  {
    path: '',
    component: AddMassimalePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AddMassimalePageRoutingModule {}
