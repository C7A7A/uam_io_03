import { Component, OnInit, OnDestroy } from '@angular/core';
import { faUser, faPlus, faUsers, faStream } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { WindowService } from '../../services/window.service';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar-bottom',
  templateUrl: './navbar-bottom.component.html',
  styleUrls: ['./navbar-bottom.component.scss']
})
export class NavbarBottomComponent implements OnInit, OnDestroy {

  faUser = faUser;
  faPlus = faPlus;
  faUsers = faUsers;
  faStream = faStream;

  activeTab = '';

  urlSubscription: Subscription;

  isLoggedIn = false;

  constructor(private windowService: WindowService, private authService: AuthService) { }

  ngOnInit(): void {
    this.windowService.location.subscribe((location) => {
      this.activeTab = location;
    });
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  ngOnDestroy(): void {
    this.urlSubscription && this.urlSubscription.unsubscribe();
  }

}
