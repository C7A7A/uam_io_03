import { Component, OnInit } from '@angular/core';
import { ILobby } from '../../interfaces/lobby.interface';
import { LobbyService } from '../../services/lobby.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lobbies-page',
  templateUrl: './lobbies-page.component.html',
  styleUrls: ['./lobbies-page.component.scss']
})
export class LobbiesPageComponent implements OnInit {

  lobbies: ILobby[] = [];

  constructor(
    private lobbyService: LobbyService,
    private authService: AuthService,
    private router: Router,
  ) {
    if (!this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/');
    }
  }

  ngOnInit(): void {
    this.lobbyService.getLobbies().subscribe((lobbies) => {
      if (lobbies) {
        this.lobbies = lobbies;
      }
    });
  }

  openLobbyDetails(lobbyName: string): void {
    this.lobbyService.navigateToLobbyDetails(lobbyName);
  }

}
