import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-movie-rating',
  templateUrl: './movie-rating.component.html',
  styleUrls: ['./movie-rating.component.scss']
})
export class MovieRatingComponent implements OnInit {

  get fullStars(): number {
    return Math.floor(this.ratingRounded);
  }

  get emptyStars(): number {
    return this.MAX_NUMBER_OF_STARS - this.fullStars - Number(this.hasAnHalfStar);
  }

  get hasAnHalfStar(): boolean {
    return this.ratingRounded / 2 % 1 >= 0.5;
  }

  get ratingRounded(): number {
    return Math.floor(this.rating) / 2;
  }

  get ratingPadded(): string {
    return String(Math.round((this.rating / 2) * 100) / 100).padEnd(3, '0');
  }

  private readonly MAX_NUMBER_OF_STARS = 5;

  @Input() movieTitle = 'King Kongas';
  /**
   * (interval: 1 - 10)
   */
  @Input() rating = 3.5;

  constructor() { }

  ngOnInit(): void {
    console.log(this.rating, this.ratingRounded, this.fullStars, this.hasAnHalfStar, this.emptyStars);
  }
}
