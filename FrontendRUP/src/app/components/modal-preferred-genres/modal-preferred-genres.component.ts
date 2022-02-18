import { Component, OnInit, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { MovieService } from '../../services/movie.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-modal-preferred-genres',
  templateUrl: './modal-preferred-genres.component.html',
  styleUrls: ['./modal-preferred-genres.component.scss']
})
export class ModalPreferredGenresComponent implements OnInit, OnDestroy {

  genres: string[];
  open = false;

  @Input() hideCloseButton = true;

  @Input() selectedGenres = new Set<string>();
  @Output() selectedGenresChange = new EventEmitter<Set<string>>();

  @Output() accept = new EventEmitter();

  genresSubscription: Subscription;

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    console.log(this.genres);
    this.genresSubscription = this.movieService.getGenresList().subscribe((genres) => {
      this.genres = genres.map(g => g.name);
      console.log(this.genres);
    });
  }

  ngOnDestroy(): void {
    this.genresSubscription && this.genresSubscription.unsubscribe();
  }

  _open(): void {
    this.open = true;
  }

  _close(): void {
    this.open = false;
  }

  onAccept(): void {
    if (this.selectedGenres.size > 2) {
      console.log('Accepted preferred genres');
      this.accept.emit(true);
    }
  }
}
