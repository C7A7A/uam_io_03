import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as _ from 'lodash';
import { IGenre } from '../interfaces/genre.interface';
import { ApiService } from './api.service';
import { first, map } from 'rxjs/operators';
import { IMovie } from '../interfaces/movie.interface';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private apiService: ApiService) {
  }

  public getGenresList(): Observable<IGenre[]> {
    return this.apiService.request<IGenre[]>('GET', '/movie/genres').pipe(
      map((genres: IGenre[]) => genres.sort((g1, g2) => (g1.name < g2.name ? -1 : 1)))
    );
  }

  public search(queryString: string): Observable<IMovie[]> {
    return this.apiService.request<IMovie[]>('GET', '/movie', {
      params: {
        q: queryString,
      }
    }).pipe(
      map((movies: IMovie[]) => movies?.map(movie => ({
        ...movie,
        title: unescape(movie.title),
        rating: _.round(movie.rating, 1),
      }))),
      map((movies: IMovie[]) => movies?.sort((m1, m2) => {
        //const containsFirst = m1.name.toLowerCase()
        return 1;
      }))
    );
  }

}
