import { TestBed } from '@angular/core/testing';

import { TweetsHandlerService } from './tweets-handler.service';

describe('TweetsHandlerService', () => {
  let service: TweetsHandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TweetsHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
