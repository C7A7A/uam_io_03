import { IFriend } from './friend.interface';
import { IMovie } from './movie.interface';
import { IGenre } from './genre.interface';

export enum LobbyStatus {
  SETTING_DATE = 'AKT',
  SELECTING_MOVIES = 'AKF',
  ENDED = 'ZAK'
}

export interface ILobby {
  host: IFriend;
  name: string;
  status: LobbyStatus;
  participants: IFriend[];
  term: string;
  proposedMovies: IMovie[];
}
