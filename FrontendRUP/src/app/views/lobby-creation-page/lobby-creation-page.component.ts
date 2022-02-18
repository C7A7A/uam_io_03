import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, AsyncValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { faUsers } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { LobbyService } from '../../services/lobby.service';
import { serial } from '../../libs/observables.lib';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-lobby-creation-page',
  templateUrl: './lobby-creation-page.component.html',
  styleUrls: ['./lobby-creation-page.component.scss']
})
export class LobbyCreationPageComponent implements OnInit {

  faUsers = faUsers;

  lobbyFormControl = new FormControl('', {
    validators: [
      Validators.required,
      Validators.minLength(3),
    ],
    asyncValidators: [
      ((): AsyncValidatorFn => {
        return (control: AbstractControl): Promise<ValidationErrors> => {
          return new Promise((resolve, reject) => {
            const subscription = this.lobbyService.isLobbyNameAvailable(control.value).subscribe((response: boolean) => {
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

  constructor(
    private authService: AuthService,
    private router: Router,
    private lobbyService: LobbyService,
    private spinner: NgxSpinnerService,
  ) {
    if (!this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/');
    }
  }

  ngOnInit(): void {
  }

  createLobby(): void {
    this.spinner.show();
    if (this.lobbyFormControl.valid) {
      this.lobbyService.createLobby(this.lobbyFormControl.value).subscribe((response) => {
        if (response) {
          this.lobbyService.navigateToLobbyDetails(this.lobbyFormControl.value);
          this.spinner.hide();
        }
      });
    }
  }

}
