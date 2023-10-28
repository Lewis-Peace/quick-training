import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyAthletesMainComponent } from './Pages/my-athletes-main/my-athletes-main.component';
import { FrontpageComponent } from './Pages/frontpage/frontpage.component';
import { RequestsComponent } from './Pages/requests/requests.component';

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
