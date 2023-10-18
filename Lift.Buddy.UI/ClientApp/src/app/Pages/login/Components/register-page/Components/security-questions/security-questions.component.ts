import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Credentials } from 'src/app/Model/Credentials';
import { SecurityQuestion } from 'src/app/Model/SecurityQuestions';
import { User } from 'src/app/Model/User';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-security-questions',
  templateUrl: './security-questions.component.html',
  styleUrls: ['./security-questions.component.css']
})
export class SecurityQuestionsComponent implements OnInit {

  @Input() securityQuestionsForm: FormGroup | undefined;
  @Output() onRegister: EventEmitter<Credentials> = new EventEmitter<Credentials>();

  constructor(
  ) { }

  ngOnInit() {
  }

  private initSecurityQuestions() {

  }

  public questions: string[] = ["test", "t", "e", "s", "t"]
  public items: number[] = [1, 2, 3]

}
