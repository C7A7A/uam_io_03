import { Component, OnInit } from '@angular/core';
import { IMovie } from '../../interfaces/movie.interface';

@Component({
  selector: 'app-history-page',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.scss']
})
export class HistoryPageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  addMovieToHistory(movie: IMovie) {
    console.log(movie);
  }

}
