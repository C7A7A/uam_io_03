import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { IApiResponse } from '../interfaces/api-response.interface';
import { Observable, BehaviorSubject, generate } from 'rxjs';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';
import { ILobby } from '../interfaces/lobby.interface';
import { Router } from '@angular/router';
import { toPromise } from '../libs/observables.lib';
import { NgxSpinnerService } from 'ngx-spinner';
import { IGenre } from '../interfaces/genre.interface';

@Injectable({
  providedIn: 'root'
})
export class LobbyService {

  clickedLobbyDetails = new BehaviorSubject<ILobby>(null);

  constructor(
    private apiService: ApiService,
    private authService: AuthService,
    private router: Router,
    private spinner: NgxSpinnerService,
  ) {
    sessionStorage.getItem('otr_lobby') && this.clickedLobbyDetails.next(JSON.parse(sessionStorage.getItem('otr_lobby')));
    this.clickedLobbyDetails.subscribe((lobby) => {
      sessionStorage.setItem('otr_lobby', JSON.stringify(lobby));
    });
  }

  public createLobby(lobbyName: string): Observable<IApiResponse> {
    return this.apiService.request('POST', `/lobby/${lobbyName}`, null, this.authService.credentials.getValue().auth_token).pipe(
      map(() => ({ result: true, message: 'success' }))
    );
  }

  public getLobbies(): Observable<any> {
    return this.apiService.request('GET', '/user/lobbies', null, this.authService.credentials.getValue().auth_token);
  }

  public getLobbyDetails(lobbyName: string): Observable<ILobby> {
    return this.apiService.request('GET', `/lobby/${lobbyName}`, null, this.authService.credentials.getValue().auth_token).pipe(
      map((lobby: { proposedMovies }) => {
        lobby.proposedMovies = lobby.proposedMovies.map((movie) => {
          movie.title = unescape(movie.title);
          movie.description = unescape(movie.description);
          movie.genres = movie.genres.map(genre => genre.name);
          movie.stills_path = movie.stills_path.split('\n');
          return movie;
        });
        return lobby as ILobby;
      })
    );
  }

  public isLobbyNameAvailable(lobbyName: string): Observable<boolean> {
    return this.getLobbyDetails(lobbyName).pipe(
      map((response) => {
        if (response) {
          return false;
        }
        return true;
      })
    );
  }

  public async navigateToLobbyDetails(lobbyName: string): Promise<void> {
    this.spinner.show();
    const details = await toPromise(this.getLobbyDetails(lobbyName));
    this.clickedLobbyDetails.next(details);
    this.router.navigateByUrl('/lobby');
    this.spinner.hide();
  }

  /* ============ LOBBY ACTIONS ============ */

  public acceptLobbyDate(lobbyName: string): Observable<IApiResponse> {
    return this.apiService.request('POST', `/lobby/${lobbyName}/date`, {
      body: {
        vote: true,
      }
    }, this.authService.credentials.getValue().auth_token).pipe(
      map(() => ({ result: true, message: 'success' }))
    );
  }

  public declineLobbyDate(lobbyName: string): Observable<IApiResponse> {
    return this.apiService.request('POST', `/lobby/${lobbyName}/date`, {
      body: {
        vote: false,
      }
    }, this.authService.credentials.getValue().auth_token).pipe(
      map(() => ({ result: true, message: 'success' }))
    );
  }

}
