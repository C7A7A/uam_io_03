import { TestBed } from '@angular/core/testing';

import { SettingsService } from './settings.service';
import { HttpClientModule } from '@angular/common/http';

describe('SettingsService', () => {
  let service: SettingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientModule ]
    });
    service = TestBed.inject(SettingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
