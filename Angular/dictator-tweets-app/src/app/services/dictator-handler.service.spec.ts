import { TestBed } from '@angular/core/testing';

import { DictatorHandlerService } from './dictator-handler.service';

describe('DictatorHandlerService', () => {
  let service: DictatorHandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DictatorHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
