import { Component, OnInit } from '@angular/core';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoginService } from '../../../../Services/login.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { User } from 'src/app/Model/User';
import { SecurityQuestion } from 'src/app/Model/SecurityQuestions';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {

  constructor(
    private snackbarService: SnackBarService,
    private loginService: LoginService,
    private router: Router
  ) { }

  ngOnInit() { }
  
  public securityQuestionsForm: FormGroup = new FormGroup({
    question1: new FormControl('', Validators.required),
    question2: new FormControl('', Validators.required),
    question3: new FormControl('', Validators.required),
    response1: new FormControl('', [Validators.required, Validators.max(50)]),
    response2: new FormControl('', [Validators.required, Validators.max(50)]),
    response3: new FormControl('', [Validators.required, Validators.max(50)]),
  });
  
  public userInformationForm: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    name: new FormControl(''),
    surname: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    confirmPassword: new FormControl('', [Validators.required, this.notSamePasswordError])
  });
  
  public notSamePasswordError(g: FormControl): ValidationErrors {
    let confirmPassword = g.value;
    let password = g.parent?.value?.password;
    g.setErrors({})
    return confirmPassword == password ? {} : {noMatch: true};
  }

  public async register() {
      const userInformation = this.userInformationForm.value;
      let user = new User();
      user.credentials.username = userInformation.username;
      user.credentials.password = userInformation.password;
      user.email = userInformation.email;
      user.name = userInformation.name;
      user.surname = userInformation.surname;

      const securityQuestions = this.securityQuestionsForm.value;
      user.securityQuestions.push(new SecurityQuestion(
        securityQuestions.quesiton1, securityQuestions.response1
      ))
      user.securityQuestions.push(new SecurityQuestion(
        securityQuestions.quesiton2, securityQuestions.response2
      ))
      user.securityQuestions.push(new SecurityQuestion(
        securityQuestions.quesiton3, securityQuestions.response3
      ))

      let response = await this.loginService.register(user);
      if (!response.result) {
        this.snackbarService.operErrorSnackbar(`Failed to register. Ex: ${response.notes}`);
        return;
      }
      this.snackbarService.openSuccessSnackbar("Registration complete");
      this.router.navigate(['/', 'login']);
  }

}
