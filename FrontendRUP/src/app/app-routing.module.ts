import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomePageComponent } from './views/home-page/home-page.component';
import { LoginPageComponent } from './views/login-page/login-page.component';
import { RegisterPageComponent } from './views/register-page/register-page.component';
import { MoviePageComponent } from './views/movie-page/movie-page.component';
import { ProfilePageComponent } from './views/profile-page/profile-page.component';
import { FriendsPageComponent } from './views/friends-page/friends-page.component';
import { LobbyCreationPageComponent } from './views/lobby-creation-page/lobby-creation-page.component';
import { LobbiesPageComponent } from './views/lobbies-page/lobbies-page.component';
import { NotFoundPageComponent } from './views/not-found-page/not-found-page.component';
import { LobbyDetailsPageComponent } from './views/lobby-details-page/lobby-details-page.component';
import { HistoryPageComponent } from './views/history-page/history-page.component';

const routes: Routes = [
  { path: '', component: HomePageComponent, data: { title: 'Strona główna' } },
  { path: 'login', component: LoginPageComponent, data: { title: 'Logowanie' } },
  { path: 'register', component: RegisterPageComponent, data: { title: 'Rejestracja' } },
  { path: 'movie', component: MoviePageComponent, data: { title: 'Selekcja filmów' } },
  { path: 'profile', component: ProfilePageComponent, data: { title: 'Profil' } },
  { path: 'friends', component: FriendsPageComponent, data: { title: 'Znajomi' } },
  { path: 'lobby/new', component: LobbyCreationPageComponent, data: { title: 'Tworzenie spotkania' } },
  { path: 'lobby', component: LobbyDetailsPageComponent, data: { title: 'Szczegóły spotkania' } },
  { path: 'lobbies', component: LobbiesPageComponent, data: { title: 'Spotkania' } },
  { path: 'history', component: HistoryPageComponent, data: { title: 'Historia' } },
  { path: '**', component: NotFoundPageComponent, data: { title: 'Strony nie znaleziono' } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
