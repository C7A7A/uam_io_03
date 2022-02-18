import { Component, Input, OnInit } from '@angular/core';
import { ILobby } from '../../interfaces/lobby.interface';

@Component({
  selector: 'app-lobby-banner',
  templateUrl: './lobby-banner.component.html',
  styleUrls: ['./lobby-banner.component.scss']
})
export class LobbyBannerComponent implements OnInit {

  @Input() lobby: ILobby;
  @Input() variants = '';

  constructor() { }

  ngOnInit(): void {
  }

}
