import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatMenuModule } from '@angular/material/menu';
import { NgxSpinnerModule } from 'ngx-spinner';

/* Reusable components */
import { ButtonComponent } from './components/button/button.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NavbarBottomComponent } from './components/navbar-bottom/navbar-bottom.component';
import { FooterComponent } from './components/footer/footer.component';
import { BadgeComponent } from './components/badge/badge.component';
import { ModalComponent } from './components/modal/modal.component';
import { ModalPreferredGenresComponent } from './components/modal-preferred-genres/modal-preferred-genres.component';
import { ModalPreferredHoursComponent } from './components/modal-preferred-hours/modal-preferred-hours.component';
import { ProfileBlockComponent } from './components/profile-block/profile-block.component';
import { UserBannerComponent } from './components/user-banner/user-banner.component';
import { MenuComponent } from './components/menu/menu.component';
import { ModalSearchUserComponent } from './components/modal-search-user/modal-search-user.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { MovieRatingComponent } from './components/movie-rating/movie-rating.component';
import { MovieInfoComponent } from './components/movie-info/movie-info.component';
import { ModalUserDetailsComponent } from './components/modal-user-details/modal-user-details.component';
import { NotificationsPanelComponent } from './components/notifications-panel/notifications-panel.component';
import { SnackbarComponent } from './components/snackbar/snackbar.component';
import { NewslistSliderComponent } from './components/newslist-slider/newslist-slider.component';
import { LobbyBannerComponent } from './components/lobby-banner/lobby-banner.component';
import { HoursBlockComponent } from './components/hours-block/hours-block.component';
import { MoviesBlockComponent } from './components/movies-block/movies-block.component';

/* Services */
import { ApiService } from './services/api.service';
import { AuthService } from './services/auth.service';
import { FriendService } from './services/friend.service';
import { MovieService } from './services/movie.service';
import { NotificationService } from './services/notification.service';
import { SettingsService } from './services/settings.service';
import { UserService } from './services/user.service';
import { WindowService } from './services/window.service';

/* Views */
import { HomePageComponent } from './views/home-page/home-page.component';
import { RegisterPageComponent } from './views/register-page/register-page.component';
import { LoginPageComponent } from './views/login-page/login-page.component';
import { MoviePageComponent } from './views/movie-page/movie-page.component';
import { ProfilePageComponent } from './views/profile-page/profile-page.component';
import { FriendsPageComponent } from './views/friends-page/friends-page.component';
import { LobbyCreationPageComponent } from './views/lobby-creation-page/lobby-creation-page.component';
import { LobbiesPageComponent } from './views/lobbies-page/lobbies-page.component';
import { NotFoundPageComponent } from './views/not-found-page/not-found-page.component';
import { LobbyDetailsPageComponent } from './views/lobby-details-page/lobby-details-page.component';
import { HistoryPageComponent } from './views/history-page/history-page.component';
import { ModalSearchMovieComponent } from './components/modal-search-movie/modal-search-movie.component';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    ButtonComponent,
    NavbarComponent,
    FooterComponent,
    BadgeComponent,
    RegisterPageComponent,
    LoginPageComponent,
    MoviePageComponent,
    NavbarBottomComponent,
    NavbarBottomComponent,
    ModalComponent,
    ModalPreferredGenresComponent,
    ModalPreferredHoursComponent,
    ProfilePageComponent,
    ProfileBlockComponent,
    FriendsPageComponent,
    UserBannerComponent,
    MenuComponent,
    ModalSearchUserComponent,
    LobbyCreationPageComponent,
    CalendarComponent,
    MovieRatingComponent,
    MovieInfoComponent,
    ModalUserDetailsComponent,
    NotificationsPanelComponent,
    SnackbarComponent,
    HoursBlockComponent,
    MoviesBlockComponent,
    LobbiesPageComponent,
    NotFoundPageComponent,
    LobbyBannerComponent,
    LobbyDetailsPageComponent,
    NewslistSliderComponent,
    HistoryPageComponent,
    ModalSearchMovieComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FontAwesomeModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatMenuModule,
    NgxSpinnerModule,
  ],
  providers: [
    ApiService,
    AuthService,
    FriendService,
    MovieService,
    NotificationService,
    SettingsService,
    UserService,
    WindowService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
