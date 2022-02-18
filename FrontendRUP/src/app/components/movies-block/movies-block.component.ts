import { Component, Input, OnInit } from '@angular/core';
import { faStar } from '@fortawesome/free-solid-svg-icons';
import { IMovie } from '../../interfaces/movie.interface';
import { MovieService } from '../../services/movie.service';

@Component({
  selector: 'app-movies-block',
  templateUrl: './movies-block.component.html',
  styleUrls: ['./movies-block.component.scss']
})
export class MoviesBlockComponent implements OnInit {

  @Input() movies = [] as IMovie[];

  faStar = faStar;

  constructor() { }

  ngOnInit(): void {
  }

}
