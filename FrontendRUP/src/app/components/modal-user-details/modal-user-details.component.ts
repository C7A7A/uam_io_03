import { Component, OnInit, OnDestroy } from '@angular/core';
import { faAt, faUser } from '@fortawesome/free-solid-svg-icons';
import { Validators, FormControl } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Subscription, NextObserver } from 'rxjs';
import { IUser } from '../../interfaces/user.interface';
import { FormGroup } from '@angular/forms';
import { SettingsService } from '../../services/settings.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-modal-user-details',
  templateUrl: './modal-user-details.component.html',
  styleUrls: ['./modal-user-details.component.scss']
})
export class ModalUserDetailsComponent implements OnInit, OnDestroy {

  userSubscription: Subscription;
  user: IUser;

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  nameFormControl = new FormControl('', [
    Validators.required,
  ]);

  surnameFormControl = new FormControl('', [
    Validators.required,
  ]);

  form = new FormGroup({
    email: this.emailFormControl,
    name: this.nameFormControl,
    surname: this.surnameFormControl,
  });

  open = false;

  faAt = faAt;
  faUser = faUser;

  constructor(private authService: AuthService, private settingsService: SettingsService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.userSubscription = this.authService.getUser().subscribe((user) => {
      this.user = user;

      this.emailFormControl.setValue(this.user?.email);
      this.nameFormControl.setValue(this.user?.name);
      this.surnameFormControl.setValue(this.user?.surname);
    });
  }

  _open(): void {
    this.open = true;
  }

  _close(): void {
    this.open = false;
  }

  ngOnDestroy(): void {
    this.userSubscription && this.userSubscription.unsubscribe();
  }

  submit(): void {
    if (this.form.valid) {
      this.spinner.show();
      this.settingsService.updateSettings(this.form.value).subscribe((data) => {
        if (data) {
          this.spinner.hide();
          this._close();
        }
      });
    }
  }

}
