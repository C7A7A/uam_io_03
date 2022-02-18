import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LobbyCreationPageComponent } from './lobby-creation-page.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ButtonComponent } from '../../components/button/button.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('LobbyCreationPageComponent', () => {
  let component: LobbyCreationPageComponent;
  let fixture: ComponentFixture<LobbyCreationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LobbyCreationPageComponent, ButtonComponent ],
      imports: [ FontAwesomeModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule, FormsModule, BrowserAnimationsModule ],
      schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LobbyCreationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
