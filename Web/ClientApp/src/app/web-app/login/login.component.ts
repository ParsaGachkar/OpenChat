import { UserService } from './../user.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { LoginState } from './LoginState';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  LoginState: typeof LoginState = LoginState;
  state = LoginState.EnterPhoneNumber;
  phoneLoginForm = this.formBuilder.group({
    phone: ['', [Validators.required, Validators.pattern('^09[0-9]{9}$')]]
  });
  codeVerificationForm = this.formBuilder.group({
    code: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(4)]]
  });
  constructor(private formBuilder: FormBuilder, private userService: UserService) { }

  ngOnInit() {
  }

  submitPhone() {
    console.log('Submit phone');
    this.state = LoginState.SendingCode;
    this.userService.GetUserAuthStart(this.phoneLoginForm.controls.phone.value
      , () => {
        this.state = LoginState.EnterCode;
        this.codeVerificationForm.controls.phone.setValue(this.phoneLoginForm.controls.phone.value);
      },
      Error => {
        this.state = LoginState.Error;
      });
  }
  submitCode() {
    this.state = LoginState.VerifyingCode;
    this.userService.GetUserAuthVerify(this.codeVerificationForm.controls.phone.value,
      this.codeVerificationForm.controls.code.value
      , () => {
        this.state = LoginState.Success;
      },
      Error => {
        this.state = LoginState.InvalidCode;
      });
  }

}
