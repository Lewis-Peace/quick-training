import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyAthletesMainComponent } from './Components/my-athletes-main/my-athletes-main.component';
import { FrontpageComponent } from './Components/frontpage/frontpage.component';
import { RequestsComponent } from './Components/requests/requests.component';

const routes: Routes = [
  {path: 'home', component: MyAthletesMainComponent},
  {path: 'frontpage', component: FrontpageComponent},
  {path: 'requests', component: RequestsComponent}
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyAthletesRoutingModule { }
