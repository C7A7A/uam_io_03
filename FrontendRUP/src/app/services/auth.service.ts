import { IUser } from '../interfaces/user.interface';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';
import { ApiService } from './api.service';
import { IApiResponse } from '../interfaces/api-response.interface';
import { IAuth } from '../interfaces/auth.interface';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public user = new BehaviorSubject<IUser>(null);
  public credentials = new BehaviorSubject<IAuth>(null);
  public isLoggedInObservable = new BehaviorSubject<boolean>(false);

  constructor(private apiService: ApiService, private router: Router, private spinner: NgxSpinnerService) {
    const credentials = sessionStorage.getItem('otr_credentials');
    if (credentials) {
      this.credentials.next(JSON.parse(credentials));
    }

    const user = sessionStorage.getItem('otr_user');
    if (credentials) {
      this.user.next(JSON.parse(user));
    }

    this.credentials.subscribe((credentials) => {
      sessionStorage.setItem('otr_credentials', JSON.stringify(credentials));
    });

    this.user.subscribe((user) => {
      sessionStorage.setItem('otr_user', JSON.stringify(user));
      this.isLoggedInObservable.next(this.isLoggedIn());
    });
  }

  public getUser(): Observable<IUser> {
    return this.user.pipe(distinctUntilChanged());
  }

  public login({login, password}): Observable<IApiResponse> {
    const response = new BehaviorSubject<IApiResponse>(null);
    this.apiService.request('POST', '/auth/login', {
      body: { login, password }
    }).subscribe((data: IAuth) => {
      if (data) {
        this.user.next({ login, password, email: '', name: '', surname: '' });
        this.credentials.next(data);
        response.next({ result: true, message: 'success' });
      } else {
        response.next({ result: false, message: 'failure' });
      }
    });
    return response;
  }

  public logout(): void {
    this.spinner.show();
    this.user.next(null);
    this.credentials.next(null);
    sessionStorage.removeItem('otr_user');
    sessionStorage.removeItem('otr_credentials');
    setTimeout(() => {
      this.router.navigateByUrl('/');
      this.spinner.hide();
    }, 1000);
  }

  public register({ login, password, email, name, surname }): Observable<IApiResponse> {
    const response = new BehaviorSubject<IApiResponse>(null);
    this.apiService.request('POST', '/auth/register', {
      body: { login, password, email, name, surname }
    }).subscribe((data: string | null) => {
        if (data) {
          response.next({ result: true, message: 'success' });
        } else {
          response.next({ result: false, message: 'failure' });
        }
    });
    return response;
  }

  public isLoggedIn(): boolean {
    return (Boolean(this.user?.value?.login) && Boolean(this.user?.value?.name));
  }
}
