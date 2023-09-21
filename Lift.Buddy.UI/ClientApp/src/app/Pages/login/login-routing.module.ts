import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './Components/login-page/login-page.component';
import { RegisterPageComponent } from './Components/register-page/register-page.component';
import { ForgotPasswordPageComponent } from './Components/forgot-password-page/forgot-password-page.component';

const routes: Routes = [
  {path: '', component: LoginPageComponent},
  {path: 'register', component: RegisterPageComponent},
  {path: 'forgot-password', component: ForgotPasswordPageComponent}
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRoutingModule { }
