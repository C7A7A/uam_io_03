import { Component, OnInit, OnDestroy } from '@angular/core';
import { INotificationView } from '../../interfaces/notification-view.interface';
import { NotificationService } from '../../services/notification.service';
import { Subscription } from 'rxjs';
import { faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.scss']
})
export class SnackbarComponent implements OnInit, OnDestroy {

  notification: INotificationView;

  faTimes = faTimes;

  // notificationSubscription: Subscription<INotification[]>();

  constructor(private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.notificationService.getNotifications().subscribe(() => {

    });
  }

  ngOnDestroy(): void {

  }

}
