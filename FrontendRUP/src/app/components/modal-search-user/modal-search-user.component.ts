import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { faSearch, faPlus } from '@fortawesome/free-solid-svg-icons';
import { FormControl } from '@angular/forms';
import { IFriend } from '../../interfaces/friend.interface';
import * as _ from 'lodash';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-modal-search-user',
  templateUrl: './modal-search-user.component.html',
  styleUrls: ['./modal-search-user.component.scss']
})
export class ModalSearchUserComponent implements OnInit {

  @Input() plusAction: (user: IFriend) => void;

  open = false;

  faSearch = faSearch;
  faPlus = faPlus;
  searchFormControl = new FormControl('', []);
  searchResult: IFriend[] = [];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    if (!this.plusAction) {
      this.plusAction = () => {};
    }
    this.searchFormControl.valueChanges.subscribe((val) => {
      this.userService.search(val).toPromise().then((users: IFriend[]) => {
        this.searchResult = users;
      });
    });
  }

  _open(): void {
    this.open = true;
  }

}
