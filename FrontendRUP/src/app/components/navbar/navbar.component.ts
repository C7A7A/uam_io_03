import { Component, OnDestroy, OnInit } from '@angular/core';
import { WindowService } from '../../services/window.service';
import { NotificationService } from '../../services/notification.service';
import { Subscription } from 'rxjs';
import { faArrowLeft, faBell, faStream, faUser, faUsers, faPlus } from '@fortawesome/free-solid-svg-icons';
import { Router, RoutesRecognized } from '@angular/router';
import { filter, distinctUntilChanged } from 'rxjs/operators';
import { AuthService } from '../../services/auth.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {

  faArrowLeft = faArrowLeft;
  faBell = faBell;
  faStream = faStream;
  faUser = faUser;
  faUsers = faUsers;
  faPlus = faPlus;

  title = '';
  hide = false;
  notificationsCount = 0;
  isLoggedIn = false;

  titleSubscription: Subscription;
  toolbarSubscription: Subscription;
  notificationSubscription: Subscription;
  userSubscription: Subscription;

  constructor(
    private windowService: WindowService,
    private router: Router,
    private location: Location,
    private notificationService: NotificationService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.titleSubscription = this.windowService.getTitle().subscribe((title) => {
      this.title = title;
    });
    this.toolbarSubscription = this.router.events.pipe(
      filter(event => event instanceof RoutesRecognized),
      distinctUntilChanged()
    ).subscribe((event: RoutesRecognized) => {
      this.hide = event.state.root.firstChild.data.toolbar === false;
      this.windowService.scrollToTop();
    });
    this.notificationSubscription = this.notificationService.getNewNotificationsCount().subscribe((count) => {
      this.notificationsCount = count;
    });
    this.userSubscription = this.authService.getUser().subscribe((user) => {
      if (!user) {
        this.isLoggedIn = false;
      } else {
        this.isLoggedIn = true;
      }
    });
  }

  logout() {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.titleSubscription && this.titleSubscription.unsubscribe();
    this.toolbarSubscription && this.toolbarSubscription.unsubscribe();
    this.notificationSubscription && this.notificationSubscription.unsubscribe();
    this.userSubscription && this.userSubscription.unsubscribe();
  }

  goBack() {
    this.location.back();
  }
}
