import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LobbyBannerComponent } from './lobby-banner.component';

describe('LobbyBannerComponent', () => {
  let component: LobbyBannerComponent;
  let fixture: ComponentFixture<LobbyBannerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LobbyBannerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LobbyBannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
