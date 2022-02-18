import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileBlockComponent } from './profile-block.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

describe('ProfileBlockComponent', () => {
  let component: ProfileBlockComponent;
  let fixture: ComponentFixture<ProfileBlockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileBlockComponent ],
      imports: [ FontAwesomeModule ],
      schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
