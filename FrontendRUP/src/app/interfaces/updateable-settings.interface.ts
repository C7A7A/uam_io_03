import { IGenre } from './genre.interface';
import { IHours } from './hours.interface';

export interface IUpdateableSettings {
  login: string;
  email: string;
  name: string;
  surname: string;
  password: string;
  preferredGenres: string[];
  preferredHours: IHours;
  microsoft?: boolean;
}
