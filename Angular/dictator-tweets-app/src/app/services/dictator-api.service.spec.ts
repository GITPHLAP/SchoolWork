import { TestBed } from '@angular/core/testing';

import { DictatorApiService } from './dictator-api.service';

describe('DictatorApiService', () => {
  let service: DictatorApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DictatorApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
