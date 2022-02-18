import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { faTimes, faTrash } from '@fortawesome/free-solid-svg-icons';
import { INotificationView } from '../../interfaces/notification-view.interface';
import { UserService } from '../../services/user.service';
import { NotificationService } from '../../services/notification.service';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { INotification } from '../../interfaces/notification.interface';
import { NgxSpinnerService } from 'ngx-spinner';
import { toPromise } from 'src/app/libs/observables.lib';

@Component({
  selector: 'app-notifications-panel',
  templateUrl: './notifications-panel.component.html',
  styleUrls: ['./notifications-panel.component.scss']
})
export class NotificationsPanelComponent implements OnInit {

  faTimes = faTimes;
  faTrash = faTrash;

  notificationsViews: INotificationView[];

  @Input() open = false;
  @Output() openChange = new EventEmitter<boolean>();

  constructor(
    private userService: UserService,
    private notificationService: NotificationService,
    private router: Router,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit(): void {
    console.log(typeof this.notificationService.getNotifications);
    this.notificationService.getNotifications().pipe(
      map(notifications => this.notificationService.parseNotifications(notifications))
    ).subscribe((notifications) => {
      this.notificationsViews = notifications;
    });
  }

  togglePanel(): void {
    this.notificationService.markAllNotificationsAsRead();
    this.open = !this.open;
    this.openChange.emit(this.open);
  }

  openPanel(): void {
    this.notificationService.markAllNotificationsAsRead();
    this.open = true;
    this.openChange.emit(true);
  }

  closePanel(): void {
    this.open = false;
    this.openChange.emit(false);
  }

  async deleteNotification(notification: INotification): Promise<void> {
    this.spinner.show();
    await toPromise(this.notificationService.deleteNotification(notification));
    await toPromise(this.notificationService.getNotifications(true));
    this.spinner.hide();
  }
}
