import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewslistSliderComponent } from './newslist-slider.component';

describe('NewslistSliderComponent', () => {
  let component: NewslistSliderComponent;
  let fixture: ComponentFixture<NewslistSliderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewslistSliderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewslistSliderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
