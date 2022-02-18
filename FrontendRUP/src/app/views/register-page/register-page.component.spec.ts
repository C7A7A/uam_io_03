import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterPageComponent } from './register-page.component';
import { ButtonComponent } from '../../components/button/button.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalPreferredGenresComponent } from '../../components/modal-preferred-genres/modal-preferred-genres.component';
import { ModalPreferredHoursComponent } from '../../components/modal-preferred-hours/modal-preferred-hours.component';
import { HttpClientModule } from '@angular/common/http';
import { ModalComponent } from '../../components/modal/modal.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('RegisterPageComponent', () => {
  let component: RegisterPageComponent;
  let fixture: ComponentFixture<RegisterPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        RegisterPageComponent,
        ButtonComponent,
        ModalPreferredGenresComponent,
        ModalPreferredHoursComponent,
        ModalComponent
      ],
      imports: [
        MatFormFieldModule,
        MatInputModule,
        ReactiveFormsModule,
        FormsModule,
        FontAwesomeModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterTestingModule
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
