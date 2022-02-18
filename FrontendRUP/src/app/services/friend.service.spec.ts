import { TestBed } from '@angular/core/testing';

import { FriendService } from './friend.service';
import { HttpClientModule } from '@angular/common/http';

describe('FriendService', () => {
  let service: FriendService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientModule ]
    });
    service = TestBed.inject(FriendService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
