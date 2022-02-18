import { Component, OnInit, ViewChild } from '@angular/core';
import { faUser, faLock, faAt} from '@fortawesome/free-solid-svg-icons';
import { Validators, FormControl, ValidatorFn, AbstractControl, FormGroup, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ModalPreferredGenresComponent } from '../../components/modal-preferred-genres/modal-preferred-genres.component';
import { ModalPreferredHoursComponent } from '../../components/modal-preferred-hours/modal-preferred-hours.component';
import { IHours } from '../../interfaces/hours.interface';
import { IApiResponse } from '../../interfaces/api-response.interface';
import { Router } from '@angular/router';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { UserService } from '../../services/user.service';
import { SettingsService } from '../../services/settings.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss']
})
export class RegisterPageComponent implements OnInit {

  faUser = faUser;
  faLock = faLock;
  faAt = faAt;

  loginFormControl = new FormControl('', {
    validators: [Validators.required],
    asyncValidators: [,
      ((): AsyncValidatorFn => {
        return (control: AbstractControl): Promise<ValidationErrors> => {
          return new Promise((resolve, reject) => {
            const subscription = this.userService.isLoginAvailable(control.value).subscribe((response: boolean) => {
              if (response === true) {
                resolve(null);
                subscription.unsubscribe();
              } else if (response === false) {
                resolve({ available: {value: control.value} } as ValidationErrors);
                subscription.unsubscribe();
              }
            });
          });
        };
      })()
    ]
  });

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  nameFormControl = new FormControl('', [
    Validators.required,
  ]);

  surnameFormControl = new FormControl('', [
    Validators.required,
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\!\@\#\$\%\^\&\*\-\_\=\+])[A-Za-z\d\!\@\#\$\%\^\&\*\-\_\=\+]{6,}$/)
  ]);

  rePasswordFormControl = new FormControl('', [
    Validators.required,
    ((): ValidatorFn => {
      return (control: AbstractControl): ValidationErrors => {
        const matching = this.passwordFormControl.value === control.value;
        return !matching ? {notMatchingPasswords: {value: control.value}} : null;
      };
    })()
  ]);

  registerGroup = new FormGroup({
    login: this.loginFormControl,
    password: this.passwordFormControl,
    email: this.emailFormControl,
    name: this.nameFormControl,
    surname: this.surnameFormControl
  });

  preferredGenres: Set<string> = new Set<string>();
  preferredHours: IHours = {
    mon: { start: null, end: null },
    tue: { start: null, end: null },
    wed: { start: null, end: null },
    thu: { start: null, end: null },
    fri: { start: null, end: null },
    sat: { start: null, end: null },
    sun: { start: null, end: null },
  };

  @ViewChild('modalPreferredGenres') modalPreferredGenres: ModalPreferredGenresComponent;
  @ViewChild('modalPreferredHours') modalPreferredHours: ModalPreferredHoursComponent;

  constructor(
    private authService: AuthService,
    private router: Router,
    private userService: UserService,
    private settingsService: SettingsService,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    if (this.registerGroup.valid) {
      this.modalPreferredGenres._open();
    }
  }

  register(): void {
    console.log(this.registerGroup.value);
    console.log(this.preferredGenres);
    console.log(this.preferredHours);
    this.spinner.show();
    this.authService.register(this.registerGroup.value).subscribe((response: IApiResponse) => {
      if (response) {
        this.authService.login(this.registerGroup.value).subscribe((response2: IApiResponse) => {
          if (response2) {
            console.log(this.authService.user.getValue());

            this.authService.credentials.subscribe(async (credentials) => {
              if (credentials) {
                await this.settingsService.updateSettings({
                  ...this.registerGroup.value,
                  preferredGenres: this.preferredGenres,
                  preferredHours: this.preferredHours,
                });
                this.spinner.hide();
                this.router.navigateByUrl('/');
                //@TODO: SNACKBAR
              }
            });
          }
        });
      }
    });
  }

  moveToNexWindow(): void {
    console.log('Moving to next window');
    this.modalPreferredGenres._close();
    this.modalPreferredHours._open();
  }

}
