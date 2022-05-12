import { TestBed } from '@angular/core/testing';

import { TweetsWsService } from './tweets-ws.service';

describe('TweetsWsService', () => {
  let service: TweetsWsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TweetsWsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
