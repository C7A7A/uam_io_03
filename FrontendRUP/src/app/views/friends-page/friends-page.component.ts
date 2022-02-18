import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { FriendService } from '../../services/friend.service';
import { Subscription } from 'rxjs';
import { IFriend } from '../../interfaces/friend.interface';
import { NotificationService } from '../../services/notification.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-friends-page',
  templateUrl: './friends-page.component.html',
  styleUrls: ['./friends-page.component.scss']
})
export class FriendsPageComponent implements OnInit, OnDestroy {

  faPlus = faPlus;

  friendsSubscription: Subscription;
  friends: IFriend[] = [];

  @ViewChild('modalSearchUser') modalSearchUser;

  constructor(
    private friendService: FriendService,
    private notificationService: NotificationService,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit(): void {
    this.updateFriends();
  }

  ngOnDestroy(): void {
    this.friendsSubscription && this.friendsSubscription.unsubscribe();
  }

  updateFriends(): void {
    this.friendsSubscription = this.friendService.getFriends().subscribe((friends) => {
      this.friends = friends;
      this.spinner.hide();
    });
  }

  removeFromFriends(login: string): void {
    this.spinner.show();
    this.friendService.removeFriend(login).subscribe((response) => {
      if (response) {
        this.updateFriends();
      }
    });
  }

  addToFriends(user: IFriend): void {
    this.spinner.show();
    this.notificationService.sendFriendsInvitation(user.login).subscribe((response) => {
      if (response) {
        this.spinner.hide();
      }
    });
  }

}
