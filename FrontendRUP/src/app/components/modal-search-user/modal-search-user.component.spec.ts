import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalSearchUserComponent } from './modal-search-user.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';
import { ModalComponent } from '../modal/modal.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MenuComponent } from '../menu/menu.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { UserBannerComponent } from '../user-banner/user-banner.component';
import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('ModalSearchUserComponent', () => {
  let component: ModalSearchUserComponent;
  let fixture: ComponentFixture<ModalSearchUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalSearchUserComponent, ModalComponent, MenuComponent, UserBannerComponent ],
      imports: [
        RouterTestingModule,
        FormsModule,
        MatFormFieldModule,
        FontAwesomeModule,
        MatInputModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
      ],
      schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalSearchUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
