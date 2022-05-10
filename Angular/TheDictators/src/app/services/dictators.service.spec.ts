import { TestBed } from '@angular/core/testing';

import { DictatorsService } from './dictators.service';

describe('DictatorsService', () => {
  let service: DictatorsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DictatorsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
