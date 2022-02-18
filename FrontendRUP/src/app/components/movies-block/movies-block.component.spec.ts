import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoviesBlockComponent } from './movies-block.component';

describe('MoviesBlockComponent', () => {
  let component: MoviesBlockComponent;
  let fixture: ComponentFixture<MoviesBlockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoviesBlockComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoviesBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
