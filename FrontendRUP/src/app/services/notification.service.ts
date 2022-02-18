import { Injectable } from '@angular/core';
import { INotification, NotificationType } from '../interfaces/notification.interface';
import { BehaviorSubject, from, interval, Observable } from 'rxjs';
import { ApiService } from './api.service';
import { AuthService } from './auth.service';
import { map, pairwise, distinctUntilChanged } from 'rxjs/operators';
import { Router } from '@angular/router';
import { UserService } from './user.service';
import { INotificationView } from '../interfaces/notification-view.interface';
import { IApiResponse } from '../interfaces/api-response.interface';
import { FriendService } from './friend.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { serial } from '../libs/observables.lib';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private notifications = new BehaviorSubject<INotification[]>([]);
  private newNotificationsCount = new BehaviorSubject<number>(0);

  constructor(
    private apiService: ApiService,
    private authService: AuthService,
    private friendService: FriendService,
    private router: Router,
    private spinner: NgxSpinnerService,
  ) {
    interval(5 * 1000).subscribe(() => {
      this.getNotifications();
    });

    this.notifications.next(JSON.parse(localStorage.getItem('otr_notifications') || '[]'));

    this.getNotifications().pipe(
      pairwise()
    ).subscribe(([oldNotifications, newNotifications]) => {
      localStorage.setItem('otr_notifications', JSON.stringify(this.notifications.getValue()));
      let newCount = newNotifications.length - (oldNotifications?.length || 0);
      if (newCount < 0) { newCount = 0; }
      this.newNotificationsCount.next(
        this.newNotificationsCount.getValue() + newCount,
      );
      if (newNotifications.filter(n => !oldNotifications.map(o => o.content).includes(n.content)).length > 0) {
        this.friendService.getFriends();
      }
    });
  }

  public getNotifications(waitForResponse = false): Observable<INotification[]> {
    if (this.authService.isLoggedIn()) {
      const request = new Promise<INotification[]>((resolve) => {
        this.apiService.request<INotification[]>('GET', '/notifications', {
        }, this.authService.credentials.getValue().auth_token).subscribe((notifications) => {
          this.notifications.next(notifications);
          resolve(notifications);
        });
      });
      if (waitForResponse) {
        return from(request);
      }
    }
    return this.notifications;
  }

  public getNewNotificationsCount(): Observable<number> {
    return this.newNotificationsCount.pipe(
      distinctUntilChanged(),
    );
  }

  public markAllNotificationsAsRead(): void {
    this.newNotificationsCount.next(0);
  }

  public sendNotification(notification: INotification, recipient: string): Observable<IApiResponse> {
    return this.apiService.request('POST', `/notifications/${recipient}`, {
      body: notification
    }, this.authService.credentials.getValue().auth_token).pipe(
      map(() => ({ result: true, message: 'success' })),
    );
  }

  public sendFriendsInvitation(recipient: string): Observable<IApiResponse> {
    const { name, surname } = this.authService.user.getValue();

    return this.sendNotification({
      type: NotificationType.FRIEND_INVITATION,
      content: `Otrzymałeś(aś) zaproszenie do grona znajomych od: ~${name} ${surname}~`,
      from: this.authService.user.getValue().login,
    }, recipient);
  }

  public sendFriendsInvitationDeclined(recipient: string): Observable<IApiResponse> {
    const { name, surname } = this.authService.user.getValue();

    return this.sendNotification({
      type: NotificationType.TEXT,
      content: `~${name} ${surname}~ odrzucił(a) twoje zaproszenie do znajomych`,
      from: this.authService.user.getValue().login,
    }, recipient);
  }

  public sendFriendsInvitationAccepted(recipient: string): Observable<IApiResponse> {
    const { name, surname } = this.authService.user.getValue();

    return this.sendNotification({
      type: NotificationType.TEXT,
      content: `~${name} ${surname}~ przyjął(ęła) twoje zaproszenie do znajomych`,
      from: this.authService.user.getValue().login,
    }, recipient);
  }

  public acceptFriendsInvitation(notification: INotification): void {
    this.spinner.show();
    serial(
      this.friendService.addFriend(notification.from),
      this.sendFriendsInvitationAccepted(notification.from),
      this.deleteNotification(notification),
      this.getNotifications(true),
      this.friendService.getFriends()
    ).then(() => {
      this.spinner.hide();
    });
  }

  public declineFriendsInvitation(notification: INotification): void {
    this.spinner.show();
    serial(
      this.sendFriendsInvitationDeclined(notification.from),
      this.deleteNotification(notification),
      this.getNotifications(true)
    ).then(() => {
      this.spinner.hide();
    });
  }

  public deleteNotification(notification: INotification): Observable<IApiResponse> {
    this.notifications.next(this.notifications.getValue()?.filter(n => n.id !== notification.id));
    return this.apiService.request('DELETE', `/notifications/${notification.id}`, null, this.authService.credentials.getValue().auth_token)
    .pipe(
      map(() => ({ result: true, message: 'success' }))
    );
  }

  /* ============= UTIL FUNCTIONS ============= */

  public parseNotifications(notifications: INotification[]): INotificationView[] {
    const output = [];
    notifications.forEach(notification => {
      switch (notification.type) {
        case NotificationType.FRIEND_INVITATION:
          output.push({
            ...notification,
            content: this.boldNotificationContent(notification.content),
            buttons: [
              {
                text: 'Odrzuć',
                action: () => {
                  this.declineFriendsInvitation(notification);
                },
                variants: 'small'
              },
              {
                text: 'Akceptuj',
                action: () => {
                  this.acceptFriendsInvitation(notification);
                },
                variants: 'small primary'
              },
            ]
          });
          break;
        case NotificationType.STATUS_CHANGE:
          output.push({
            ...notification,
            content: this.boldNotificationContent(notification.content),
            buttons: [
              {
                text: 'Przejdź do spotkania',
                action: () => {
                  this.router.navigateByUrl(`/lobby/${notification?.lobbyName}`);
                },
                variants: 'small'
              }
            ]
          });
          break;
        default:
          output.push({
            ...notification,
            content: this.boldNotificationContent(notification.content),
            buttons: [],
          });
      }
    });
    return output;
  }

  public boldNotificationContent(content: string): string {
    return content.replace(/~(.*)~/g, '<strong>$1</strong>');
  }
}
