import { IHours } from './hours.interface';

export interface IUser {
  login: string;
  email: string;
  name: string;
  surname: string;
  password: string;
  preferredGenres?: string[] | null;
  preferredHours?: IHours | null;
  microsoft?: boolean;
}
