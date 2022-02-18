import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IFriend } from '../interfaces/friend.interface';
import { distinctUntilChanged, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { IApiResponse } from '../interfaces/api-response.interface';
import { ApiService } from './api.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  friends = new BehaviorSubject<IFriend[]>([]);

  constructor(
    private apiService: ApiService,
    private authService: AuthService,
  ) { }

  public getFriends(): Observable<IFriend[]> {
    this.apiService.request<IFriend[]>('GET', '/user/friends', null, this.authService.credentials.getValue().auth_token)
    .subscribe((friends) => {
      this.friends.next(friends);
    });
    return this.friends;
  }

  public addFriend(login: string): Observable<IApiResponse> {
    return this.apiService.request('POST', '/user/friends', {
      params: {
        login
      }
    }, this.authService.credentials.getValue().auth_token).pipe(
      map(() => ({ result: true, message: 'success' }))
    );
  }

  public removeFriend(login: string): Observable<IApiResponse> {
    return this.apiService.request('DELETE', '/user/friends', {
      params: {
        login
      }
    }, this.authService.credentials.getValue().auth_token).pipe(
      map((data) => {
        this.getFriends();
        return data;
      }),
      map(() => ({ result: true, message: 'success' }))
    );
  }
}
