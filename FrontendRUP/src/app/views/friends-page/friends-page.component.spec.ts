import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FriendsPageComponent } from './friends-page.component';
import { HttpClientModule } from '@angular/common/http';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatMenuModule } from '@angular/material/menu';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';
import { ButtonComponent } from '../../components/button/button.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ModalSearchUserComponent } from '../../components/modal-search-user/modal-search-user.component';
import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalComponent } from '../../components/modal/modal.component';
import { RouterTestingModule } from '@angular/router/testing';
import { UserBannerComponent } from '../../components/user-banner/user-banner.component';

describe('FriendsPageComponent', () => {
  let component: FriendsPageComponent;
  let fixture: ComponentFixture<FriendsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        FriendsPageComponent,
        ButtonComponent,
        ModalSearchUserComponent,
        ModalComponent,
        UserBannerComponent
      ],
      imports: [
        HttpClientModule,
        MatFormFieldModule,
        MatMenuModule,
        FontAwesomeModule,
        MatInputModule,
        BrowserAnimationsModule,
        FormsModule,
        RouterTestingModule,
        ReactiveFormsModule
      ],
      schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FriendsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
