import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchBarComponent } from './Components/search-bar/search-bar.component';

const routes: Routes = [
  {path: '', component: SearchBarComponent},
  {path: '**', redirectTo: ''},
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SearchRoutingModule { }
