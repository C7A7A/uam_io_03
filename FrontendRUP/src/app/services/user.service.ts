import { Injectable } from '@angular/core';
import { IFriend } from '../interfaces/friend.interface';
import { ApiService } from './api.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService: ApiService, private authService: AuthService) { }

  public search(queryString: string): Observable<IFriend[]> {
    return this.apiService.request('GET', '/user', {
      params: { q: queryString }
    }).pipe(
      map(
        (users: IFriend[]) => users?.filter(user => this.authService.user?.value?.login !== user.login),
      )
    );
  }

  public isLoginAvailable(login: string): Observable<boolean> {
    const response = new BehaviorSubject<boolean>(null);
    this.apiService.request('GET', '/user', {
      params: { q: login }
    }).subscribe((data: IFriend[]) => {
      if (data) {
        data.forEach((user: IFriend) => {
          if (user.login === login) {
            response.next(false);
          }
        });
        if (response.value === null) {
          response.next(true);
        }
      } else {
        response.next(true);
      }
    });
    return response;
  }

  public addToFriends(login: string): void {
    // This should be subscription with success response
  }

}
