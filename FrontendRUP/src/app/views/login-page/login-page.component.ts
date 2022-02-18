import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { faUser, faLock } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../services/auth.service';
import { IApiResponse } from '../../interfaces/api-response.interface';
import { Router } from '@angular/router';
import { SettingsService } from '../../services/settings.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  faUser = faUser;
  faLock = faLock;

  loginFormControl = new FormControl('', [
    Validators.required,
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required,
  ]);

  loginGroup = new FormGroup({
    password: this.passwordFormControl,
    login: this.loginFormControl
  });

  constructor(
    private authService: AuthService, private router: Router, private settingsService: SettingsService, private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
  }

  login(): void {
    this.loginFormControl.updateValueAndValidity();
    this.passwordFormControl.updateValueAndValidity();
    if (this.loginGroup.valid) {
      this.spinner.show();
      this.authService.login(this.loginGroup.value).subscribe((response: IApiResponse) => {
        if (response && response.result) {
          this.settingsService.getSettings().subscribe((data) => {
            if (data) {
              console.log(data);
              this.spinner.hide();
              this.router.navigateByUrl('/');
            }
          });
        } else if (response && !response.result) {
          this.loginFormControl.setErrors({
            incorrect: true,
          });
          this.passwordFormControl.setErrors({
            incorrect: true,
          });
          this.spinner.hide();
        }
      });
    }
  }

}
