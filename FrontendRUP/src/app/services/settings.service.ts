import { Injectable } from '@angular/core';
import { IUpdateableSettings } from '../interfaces/updateable-settings.interface';
import { ApiService } from './api.service';
import { AuthService } from './auth.service';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { IApiResponse } from '../interfaces/api-response.interface';
import * as _ from 'lodash';
import { IHours, IHourDBFormat } from '../interfaces/hours.interface';
import { map } from 'rxjs/operators';
import { omit } from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private apiService: ApiService, private authService: AuthService) { }

  public updateSettings(settings): Observable<IApiResponse> {
    settings = {
      ...this.authService.user.getValue(),
      ...settings,
    };

    const obs = new BehaviorSubject<IApiResponse>(null);
    this.apiService.request('POST', '/user/settings', {
      params: {
        ..._.omit(settings, ['preferredHours', 'preferredGenres']),
        preferredgenres: Array.from(settings.preferredGenres).join(','),
      },
      body: [
        ...this.convertHoursToDBFormat(settings.preferredHours),
      ],
    }, this.authService.credentials.getValue().auth_token).subscribe(() => {
      this.authService.user.next({
        ...this.authService.user.value,
        ...settings,
      });
      console.log(this.authService.user.value);
      obs.next({ result: true, message: 'success' });
    });
    return obs;
  }

  public getSettings(): Observable<IUpdateableSettings> {
    return this.apiService.request('GET', '/user/settings', null, this.authService.credentials.value.auth_token).pipe(
      map((settings: {
        preferredgenres: string[],
        preferredhours: {day: string, startTime: string, endTime: string}[]
      }) => ({
        ...this.authService.user.value,
        ...omit(settings, 'preferredgenres', 'preferredhours'),
        preferredGenres: settings.preferredgenres[0].split(','),
        preferredHours: this.convertHours(settings.preferredhours),
      } as IUpdateableSettings)),
      map((val) => { console.log(val); return val; }),
      map((settings) => {
        this.authService.user.next(settings);
        return settings;
      })
    );
  }

  public convertHoursToDBFormat(hours: IHours): IHourDBFormat[] {
    const _output = [] as IHourDBFormat[];
    Object.keys(hours).forEach((day: 'mon' | 'tue' | 'wed' | 'thu' | 'fri' | 'sat' | 'sun') => {
      if (hours[day]?.start && hours[day]?.end) {
        _output.push({
          day,
          StartTime: hours[day].start,
          EndTime: hours[day].end,
        });
      }
    })
    return _output;
  }

  public convertHours(hours: {day: string, startTime: string, endTime: string}[]): IHours {
    const _output = {
      mon: { start: null, end: null },
      tue: { start: null, end: null },
      wed: { start: null, end: null },
      thu: { start: null, end: null },
      fri: { start: null, end: null },
      sat: { start: null, end: null },
      sun: { start: null, end: null },
    } as IHours;
    hours.forEach((day) => {
      _output[day.day] = {start: day.startTime, end: day.endTime};
    });
    return _output;
  }
}
