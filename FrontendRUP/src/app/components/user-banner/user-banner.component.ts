import { Component, OnInit, Input } from '@angular/core';
import { IUser } from '../../interfaces/user.interface';
import { IFriend } from '../../interfaces/friend.interface';

@Component({
  selector: 'app-user-banner',
  templateUrl: './user-banner.component.html',
  styleUrls: ['./user-banner.component.scss']
})
export class UserBannerComponent implements OnInit {

  @Input() user: IUser | IFriend;
  @Input() variants = '';

  fullName: string;

  constructor() { }

  ngOnInit(): void {
    this.fullName = this.user?.name + ' ' + this.user?.surname;
  }

}
