import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, of } from 'rxjs';
import { tap, distinctUntilChanged, share, catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public request<T>(method: string, path: string, options: {
    body?: any,
    params?: any
  // tslint:disable-next-line: align
  } = {}, token = null): Observable<T> {
    return this.http.request<T>(method, this.apiUrl + path, {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*',
        Authorization: (token) ? 'Bearer ' + token : '',
      }),
      ...options,
    })
    .pipe(
      catchError(() => {
        return of(null);
      }),
      tap(
        (data: T) => this.logger(`${method} ${path}`, options, data),
        (err) => { console.error(err); }
      ),
      distinctUntilChanged(),
      share()
    );
  }

  private logger(path: string, request: any, response: any) {
    console.groupCollapsed(`%c ${path}`, 'color: #bada55');
    console.log(request);
    console.log(response);
    console.groupEnd();
  }
}
