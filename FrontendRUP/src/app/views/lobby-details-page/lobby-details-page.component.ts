import { Component, OnInit } from '@angular/core';
import { LobbyService } from '../../services/lobby.service';
import { ILobby, LobbyStatus } from '../../interfaces/lobby.interface';
import * as moment from 'moment';
import 'moment/locale/pl';
import { faCouch } from '@fortawesome/free-solid-svg-icons';

moment.locale('pl');

@Component({
  selector: 'app-lobby-details-page',
  templateUrl: './lobby-details-page.component.html',
  styleUrls: ['./lobby-details-page.component.scss']
})
export class LobbyDetailsPageComponent implements OnInit {

  LobbyStatus = LobbyStatus;
  faCouch = faCouch;

  lobby: ILobby;

  term = null;

  constructor(private lobbyService: LobbyService) { }

  ngOnInit(): void {
    this.lobbyService.clickedLobbyDetails.subscribe((lobby) => {
      this.lobby = lobby;
      this.term = moment(this.lobby.term).format('LL');
    });
  }

  sendDateVote(vote: boolean): void {
    if (vote) {
      this.lobbyService.acceptLobbyDate(this.lobby.name);
    } else {
      this.lobbyService.declineLobbyDate(this.lobby.name);
    }
  }

}
