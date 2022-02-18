import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoviePageComponent } from './movie-page.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';
import { ButtonComponent } from '../../components/button/button.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MovieGenreComponent } from '../../components/movie-genre/movie-genre.component';
import { MovieRatingComponent } from '../../components/movie-rating/movie-rating.component';

describe('MoviePageComponent', () => {
  let component: MoviePageComponent;
  let fixture: ComponentFixture<MoviePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        MoviePageComponent,
        ButtonComponent,
        MovieGenreComponent,
        MovieRatingComponent
      ],
      imports: [ FontAwesomeModule ],
      schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoviePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
