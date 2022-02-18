export interface IMovie {
  id: number;
  title: string;
  description: string;
  poster_path: string;
  rating: number;
  genres: string[];
  /**
   * Duration in minutes [m]
   */
  length: number;
  stills_path: string[];
}
