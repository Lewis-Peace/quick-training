import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginContainerComponent } from './Pages/login/login-container.component';
import { LoginPageComponent } from './Pages/login/Components/login-page/login-page.component';
import { RegisterPageComponent } from './Pages/login/Components/register-page/register-page.component';
import { HomePageComponent } from './Pages/Home/home-page/home-page.component';
import { ForgotPasswordPageComponent } from './Pages/login/Components/forgot-password-page/forgot-password-page.component';

const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: 'home', component: HomePageComponent},
  {
    path: 'login',
    component: LoginContainerComponent,
    children: [
      {path: '', component: LoginPageComponent},
      {path: 'register', component: RegisterPageComponent},
      {path: 'forgot-password', component: ForgotPasswordPageComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
