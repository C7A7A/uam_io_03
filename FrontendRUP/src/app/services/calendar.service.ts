import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { ApiService } from './api.service';
import { Observable, interval } from 'rxjs';
import { SettingsService } from './settings.service';

const loginUri = 'https://login.microsoftonline.com/common/oauth2/v2.0/authorize?';
const graphMeEndpoint = 'https://graph.microsoft.com/v1.0/me';
const graphUsersEndpoint = 'https://graph.microsoft.com/v1.0/users';
const graphUsersCalendar = 'https://graph.microsoft.com/v1.0/me/calendar';
const scope = [
  'user.read',
  'profile',
  'calendars.readwrite',
  'offline_access',
  'openid',
  'calendars.read.shared',
  'calendars.readWrite.shared'
].join(' ');
const clientId = '49f1cc6a-8058-4fd2-89bb-44caebf553cb';
const authority = 'https://login.microsoftonline.com/common';
const redirectUri = `${environment.apiUrl}/auth/microsoft`;
const responseType = 'code';
const responseMode = 'query';

@Injectable({
  providedIn: 'root'
})
export class CalendarService {

  constructor(
    private apiService: ApiService,
    private authService: AuthService,
    private settingsService: SettingsService
  ) { }

  public authorize(): void {
    const windowObject = window.open(loginUri + new HttpParams({
      fromObject: {
        scope,
        client_id: clientId,
        response_type: responseType,
        redirect_uri: redirectUri,
        response_mode: responseMode,
        state: this.authService.credentials.getValue().auth_token,
      }
    }), 'Microsoft', 'width=400px, height=800px');
    const intervalSubscription = interval(5 * 1000).subscribe(() => {
      const settingsSubscription = this.settingsService.getSettings().subscribe((settings) => {
        if (settings) {
          if (settings.microsoft) {
            intervalSubscription.unsubscribe();
            windowObject.close();
          }
          settingsSubscription.unsubscribe();
        }
      });
    });
  }

  public getCalendar(): Observable<any> {
    return this.apiService.request('GET', '/auth/test/calendar', null, this.authService.credentials.getValue().auth_token);
  }

  public getEvents(): Observable<any> {
    return this.apiService.request('GET', '/auth/test/events', null, this.authService.credentials.getValue().auth_token);
  }
}
