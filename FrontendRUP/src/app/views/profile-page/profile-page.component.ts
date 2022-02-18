import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { PartialObserver, Subscription } from 'rxjs';
import { IUser } from '../../interfaces/user.interface';
import { Router } from '@angular/router';
import { IGenre } from '../../interfaces/genre.interface';
import { SettingsService } from '../../services/settings.service';
import { ModalPreferredGenresComponent } from '../../components/modal-preferred-genres/modal-preferred-genres.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { IHours } from '../../interfaces/hours.interface';
import { ModalPreferredHoursComponent } from '../../components/modal-preferred-hours/modal-preferred-hours.component';
import { CalendarService } from '../../services/calendar.service';
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';
import { faCheck, faPlus, faPlusCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit {

  faPlus = faPlusCircle;
  faCheck = faCheck;

  userSubscription: Subscription;
  user: IUser;

  preferredGenres = new Set();
  preferredHours: IHours;

  microsoftIcon = faPlus;

  @ViewChild('modalPreferredGenres') modalPreferredGenres: ModalPreferredGenresComponent;
  @ViewChild('modalPreferredHours') modalPreferredHours: ModalPreferredHoursComponent;

  constructor(
    private authService: AuthService,
    private router: Router,
    private settingsService: SettingsService,
    private spinner: NgxSpinnerService,
    private calendarService: CalendarService,
  ) {
    if (!this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/');
    }
  }

  ngOnInit(): void {
    this.userSubscription = this.authService.getUser().subscribe((user) => {
      if (user) {
        this.user = user;
        this.preferredGenres = new Set(this.user.preferredGenres);
        this.preferredHours = this.user.preferredHours;

        this.user.microsoft && (this.microsoftIcon = this.faCheck);
      }
    });
  }

  updatePreferredGenres(): void {
    this.spinner.show();
    this.settingsService.updateSettings({ preferredGenres: this.preferredGenres }).subscribe(data => {
      if (data) {
        this.spinner.hide();
        this.modalPreferredGenres._close();
      }
    });
  }

  updatePreferredHours(): void {
    this.spinner.show();
    this.settingsService.updateSettings({ preferredHours: this.preferredHours }).subscribe(data => {
      if (data) {
        this.spinner.hide();
        this.modalPreferredHours._close();
      }
    });
  }

  authorizeMicrosoftAccount(): void {
    if (!this.user.microsoft) {
      this.calendarService.authorize();
    }
  }

  logout(): void {
    this.authService.logout();
  }

}
