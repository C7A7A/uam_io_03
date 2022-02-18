import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieRatingComponent } from './movie-rating.component';
describe('MovieRatingComponent', () => {
  let component: MovieRatingComponent;
  let fixture: ComponentFixture<MovieRatingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MovieRatingComponent]
    });
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieRatingComponent);
    component = fixture.componentInstance;
  });

  describe('when the rating has no remainder', () => {
    it('shows five empty stars when the rating is zero', () => {
      component.rating = 0;
      fixture.detectChanges();

      expect(getEmptyStars().length).toEqual(5);
    });

    it('shows zero empty stars when the rating is five', () => {
      component.rating = 5;
      fixture.detectChanges();

      expect(getEmptyStars().length).toEqual(0);
    });

    it('shows "five minus rating" empty stars', () => {
      component.rating = 3;

      fixture.detectChanges();

      expect(getEmptyStars().length).toEqual(2);
    });

    it('shows five full stars when the rating is five', () => {
      component.rating = 5;
      fixture.detectChanges();

      expect(getFullStars().length).toEqual(5);
    });

    it('shows zero full stars when the rating is zero', () => {
      component.rating = 0;
      fixture.detectChanges();

      expect(getFullStars().length).toEqual(0);
    });

    it('shows "rating" full stars', () => {
      component.rating = 3;
      fixture.detectChanges();

      expect(getFullStars().length).toEqual(3);
    });

    it('shows no half star', () => {
      component.rating = 3;
      fixture.detectChanges();

      expect(getHalfStar()).toBeNull();
    });
  });

  describe('when the rating has a remainder', () => {
    const ratingWithRemainder = 2.5;

    it('shows a half star', () => {
      component.rating = ratingWithRemainder;

      fixture.detectChanges();

      expect(getHalfStar()).not.toBeNull();
    });

    it('shows 5 - (ratingWithRemainder + 0.5) empty stars', () => {
      component.rating = ratingWithRemainder;

      fixture.detectChanges();

      expect(getEmptyStars().length).toEqual(2);
    });

    it('shows ratingWithRemainder - 0.5 full stars', () => {
      component.rating = ratingWithRemainder;
      fixture.detectChanges();

      expect(getFullStars().length).toEqual(2);
    });
  });

  function getEmptyStars(): HTMLSpanElement[] {
    return fixture.nativeElement.querySelectorAll('[data-jest="empty star"]');
  }

  function getFullStars(): HTMLSpanElement[] {
    return fixture.nativeElement.querySelectorAll('[data-jest="full star"]');
  }

  function getHalfStar(): HTMLSpanElement {
    return fixture.nativeElement.querySelector('[data-jest="half star"]');
  }
});


// describe('MovieRatingComponent', () => {
//   let component: MovieRatingComponent;
//   let fixture: ComponentFixture<MovieRatingComponent>;

//   beforeEach(async () => {
//     await TestBed.configureTestingModule({
//       declarations: [ MovieRatingComponent ]
//     })
//     .compileComponents();
//   });

//   beforeEach(() => {
//     fixture = TestBed.createComponent(MovieRatingComponent);
//     component = fixture.componentInstance;
//     fixture.detectChanges();
//   });

//   it('should create', () => {
//     expect(component).toBeTruthy();
//   });
// });
