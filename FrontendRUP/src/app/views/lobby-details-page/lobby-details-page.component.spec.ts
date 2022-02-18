import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LobbyDetailsPageComponent } from './lobby-details-page.component';

describe('LobbyDetailsPageComponent', () => {
  let component: LobbyDetailsPageComponent;
  let fixture: ComponentFixture<LobbyDetailsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LobbyDetailsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LobbyDetailsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
