import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeneralSettingsComponent } from './Components/general-settings/general-settings.component';

const routes: Routes = [
  {
    path: 'test',
    component: GeneralSettingsComponent,
  },
  {path: '**', redirectTo: ''},
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
