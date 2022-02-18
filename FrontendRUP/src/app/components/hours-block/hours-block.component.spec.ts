import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HoursBlockComponent } from './hours-block.component';

describe('HoursBlockComponent', () => {
  let component: HoursBlockComponent;
  let fixture: ComponentFixture<HoursBlockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HoursBlockComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HoursBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
