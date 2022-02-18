import { Component, Input, OnInit } from '@angular/core';
import { IFriend } from '../../interfaces/friend.interface';
import { FormControl } from '@angular/forms';
import { faSearch, faPlus } from '@fortawesome/free-solid-svg-icons';
import { MovieService } from '../../services/movie.service';
import { IMovie } from '../../interfaces/movie.interface';

@Component({
  selector: 'app-modal-search-movie',
  templateUrl: './modal-search-movie.component.html',
  styleUrls: ['./modal-search-movie.component.scss']
})
export class ModalSearchMovieComponent implements OnInit {

  @Input() plusAction: (user: IMovie) => void;

  open = false;

  faSearch = faSearch;
  faPlus = faPlus;
  searchFormControl = new FormControl('', []);
  searchResult: IMovie[] = [];

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    if (!this.plusAction) {
      this.plusAction = () => {};
    }
    this.searchFormControl.valueChanges.subscribe((val) => {
      this.movieService.search(val).toPromise().then((movies: IMovie[]) => {
        this.searchResult = movies;
      });
    });
  }

  _open(): void {
    this.open = true;
  }

}
