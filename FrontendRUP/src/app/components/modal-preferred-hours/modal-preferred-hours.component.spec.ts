import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalPreferredHoursComponent } from './modal-preferred-hours.component';
import { ModalComponent } from '../modal/modal.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';
import { ButtonComponent } from '../button/button.component';

describe('ModalPreferredHoursComponent', () => {
  let component: ModalPreferredHoursComponent;
  let fixture: ComponentFixture<ModalPreferredHoursComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalPreferredHoursComponent, ModalComponent, ButtonComponent ],
      imports: [
        MatFormFieldModule,
        FontAwesomeModule,
        HttpClientModule,
        RouterTestingModule,
        FormsModule,
        ReactiveFormsModule,
        MatInputModule,
        BrowserAnimationsModule
      ],
      schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalPreferredHoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
