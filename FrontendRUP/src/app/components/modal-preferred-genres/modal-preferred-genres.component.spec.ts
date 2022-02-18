import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalPreferredGenresComponent } from './modal-preferred-genres.component';
import { ModalComponent } from '../modal/modal.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { HttpClientModule } from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';
import { ButtonComponent } from '../button/button.component';

describe('ModalPreferredGenresComponent', () => {
  let component: ModalPreferredGenresComponent;
  let fixture: ComponentFixture<ModalPreferredGenresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalPreferredGenresComponent, ModalComponent, ButtonComponent ],
      imports: [ FontAwesomeModule, MatFormFieldModule, MatCheckboxModule, HttpClientModule, RouterTestingModule, MatCheckboxModule ],
      schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalPreferredGenresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
