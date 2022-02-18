import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilePageComponent } from './profile-page.component';
import { HttpClientModule } from '@angular/common/http';
import { MatMenuModule } from '@angular/material/menu';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';
import { ButtonComponent } from '../../components/button/button.component';
import { ProfileBlockComponent } from '../../components/profile-block/profile-block.component';
import { CalendarComponent } from '../../components/calendar/calendar.component';
import { ModalUserDetailsComponent } from '../../components/modal-user-details/modal-user-details.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalComponent } from '../../components/modal/modal.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('ProfilePageComponent', () => {
  let component: ProfilePageComponent;
  let fixture: ComponentFixture<ProfilePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        ProfilePageComponent,
        ButtonComponent,
        CalendarComponent,
        ProfileBlockComponent,
        ModalUserDetailsComponent,
        ModalComponent
      ],
      imports: [
        HttpClientModule,
        MatMenuModule,
        FontAwesomeModule,
        MatInputModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        FormsModule,
        BrowserAnimationsModule,
        RouterTestingModule
      ],
      schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
