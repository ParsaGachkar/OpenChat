import { Router } from '@angular/router';
import { UserService } from './../user.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { LoginState } from './LoginState';
import { ReCaptchaV3Service } from 'ngx-captcha';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  LoginState: typeof LoginState = LoginState;
  state = LoginState.EnterPhoneNumber;
  phoneLoginForm = this.formBuilder.group({
    phone: ['', [Validators.required, Validators.pattern('^09[0-9]{9}$')]],
    captcha: ['', [Validators.required]]
  });
  codeVerificationForm = this.formBuilder.group({
    code: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(4)]],
    captcha: ['', [Validators.required]]
  });
  constructor(private formBuilder: FormBuilder, private userService: UserService, private router: Router
    , private recaptcha: ReCaptchaV3Service) { }

  ngOnInit() {
    this.fillPhoneCaptcha();
  }
  fillPhoneCaptcha() {
    this.recaptcha.execute("6Levh6MUAAAAAHuI6D32CQlnw3TfRR0zY7rhD3Ss", 'EnterPhone', token => {
      this.phoneLoginForm.controls.captcha.setValue(token);
    }, {
        useGlobalDomain: false
      });
  }
  fillCodeCaptcha() {
    this.recaptcha.execute("6Levh6MUAAAAAHuI6D32CQlnw3TfRR0zY7rhD3Ss", 'EnterPhone', token => {
      this.codeVerificationForm.controls.captcha.setValue(token);
    }, {
        useGlobalDomain: false
      });
  }

  submitPhone() {
    console.log('Submit phone');
    this.state = LoginState.SendingCode;
    this.userService.GetUserAuthStart(this.phoneLoginForm.controls.phone.value
      , () => {
        this.state = LoginState.EnterCode;
        this.fillCodeCaptcha();
      },
      Error => {
        this.state = LoginState.Error;
      });
  }
  submitCode() {
    this.state = LoginState.VerifyingCode;
    this.userService.GetUserAuthVerify(this.phoneLoginForm.controls.phone.value,
      this.codeVerificationForm.controls.code.value
      , () => {
        this.state = LoginState.Success;
        setTimeout(() => {
          this.router.navigate(['/WebApp']);
        }, 5000);
      },
      Error => {
        this.state = LoginState.InvalidCode;
      });
  }

  retry() {
    this.state = LoginState.EnterPhoneNumber;
    this.fillPhoneCaptcha();
  }

}
