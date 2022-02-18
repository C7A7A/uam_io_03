import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { faChevronRight, faChevronLeft, faHeart, faTimes} from '@fortawesome/free-solid-svg-icons';
import { LobbyService } from '../../services/lobby.service';
import { IMovie } from '../../interfaces/movie.interface';
import { Location } from '@angular/common';

@Component({
  selector: 'app-movie-page',
  templateUrl: './movie-page.component.html',
  styleUrls: ['./movie-page.component.scss']
})
export class MoviePageComponent implements OnInit {

  moviesList: IMovie[] = [];
  currentMovieIndex = 0;

  stillsOffset = 0;

  faHeart = faHeart;
  faTimes = faTimes;
  faChevronLeft = faChevronLeft;
  faChevronRight = faChevronRight;

  wrapperStyle: any = { };
  swipeTransform = '';

  isSwiping = false;
  shouldSwipeTransform = false;
  swipeStart;
  lastSwipePosition;

  constructor(
    private lobbyService: LobbyService,
    private location: Location,
  ) { }

  ngOnInit(): void {
    document.addEventListener('contextmenu', event => event.preventDefault());

    this.lobbyService.clickedLobbyDetails.subscribe((lobby) => {
      this.moviesList = lobby.proposedMovies;
    });
  }

  nextStill(): void {
    this.animateStill(1);
  }

  prevStill(): void {
    this.animateStill(-1);
  }

  animateStill(direction: number): void {
    const speed = 300;
    const moviesCount = this.moviesList[this.currentMovieIndex].stills_path.length;

    if (this.stillsOffset === 0 && direction < 0) {
      this.stillsOffset = moviesCount;
    } else if (this.stillsOffset === moviesCount && direction > 0) {
      this.stillsOffset = 0;
    }

    this.wrapperStyle = {
      transform: 'translateX(-' + this.stillsOffset + '00%)'
    };

    this.stillsOffset = (this.stillsOffset + (moviesCount + 1) + direction) % (moviesCount + 1);

    window.requestAnimationFrame(() => {
      this.wrapperStyle = {
        transition: 'transform ' + speed + 'ms ease',
        transform: 'translateX(-' + (this.stillsOffset) + '00%)',
      };
    });
  }

  startSwipe(event): void {
    this.swipeStart = {
      x: event.touches[0].clientX,
      y: event.touches[0].clientY,
    };

    this.isSwiping = true;
    console.log('startSwipe');
  }

  updateSwipe(event): void {
    if (this.isSwiping) {
      const swipePosition = {
        x: event.touches[0].clientX,
        y: event.touches[0].clientY,
      };

      this.lastSwipePosition = swipePosition;

      if (Math.abs(swipePosition.x - this.swipeStart.x) > 50) {
        this.shouldSwipeTransform = true;
      }

      if (this.shouldSwipeTransform) {
        const percentage = (swipePosition.x - this.swipeStart.x) / (window.innerWidth / 2) * 100;
        this.swipeTransform = `translateX(${(percentage / 100) * 40}%) rotateZ(${(percentage / 100) * 10}deg)`;
      }
    }
    console.log('updateSwipe');
  }

  endSwipe(event): void {
    if (!this.lastSwipePosition || Math.abs(this.lastSwipePosition.x - this.swipeStart.x) < (window.innerWidth / 1.5)) {
      this.swipeTransform = 'none';
    } else if (this.shouldSwipeTransform) {
      if (this.lastSwipePosition.x < window.innerWidth / 2) {
        this.decline();
      } else {
        this.accept();
      }
    }

    this.isSwiping = false;
    this.shouldSwipeTransform = false;
  }

  accept(): void {
    this.swipeTransform = 'translateX(100%) rotateZ(10deg)';
    window.setTimeout(() => {
      this.currentMovieIndex++;
      this.swipeTransform = 'none';
    }, 300);
    console.log('ACCEPTED');
  }

  decline(): void {
    this.swipeTransform = 'translateX(-100%) rotateZ(-10deg)';
    window.setTimeout(() => {
      this.currentMovieIndex++;
      this.swipeTransform = 'none';
    }, 300);
    console.log('DECLINED');
  }

  goBack(): void {
    this.location.back();
  }

}
