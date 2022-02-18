import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { INewsfeed } from '../interfaces/newsfeed.interface';
import { Observable } from 'rxjs';
import { map, distinctUntilChanged, mergeMap } from 'rxjs/operators';
import * as xml2js from 'xml2js';

@Injectable({
  providedIn: 'root'
})
export class NewsfeedService {

  constructor(private http: HttpClient) { }

  private parseString(data): Promise<any> {
    return new Promise((resolve) => {
      xml2js.parseString(data, (err, res) => resolve(res));
    });
  }

  public getNewses(): Observable<INewsfeed> {
    return this.http.get('https://czerniak.ddns.net/feed/news/latest', {
      observe: 'body',
      responseType: 'text',
    }).pipe(
      mergeMap(this.parseString),
      map(res => res.rss.channel[0]),
      distinctUntilChanged(),
    );
  }
}
