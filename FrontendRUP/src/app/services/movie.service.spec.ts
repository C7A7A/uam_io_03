import { fakeAsync, TestBed } from '@angular/core/testing';

import { MovieService } from './movie.service';
import { IGenre } from '../interfaces/genre.interface';
import { async } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';

describe('MovieService', () => {
  let service: MovieService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientModule ]
    });
    service = TestBed.inject(MovieService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('genres list', () => {
    return new Promise((resolve, reject) => {
      service.getGenresList().subscribe((result: IGenre[]) => {
        expect(result).toBeTruthy();
        resolve();
      });
    });
  });
});
