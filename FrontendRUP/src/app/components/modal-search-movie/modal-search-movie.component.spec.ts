import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalSearchMovieComponent } from './modal-search-movie.component';

describe('ModalSearchMovieComponent', () => {
  let component: ModalSearchMovieComponent;
  let fixture: ComponentFixture<ModalSearchMovieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalSearchMovieComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalSearchMovieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
