/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BreakdownService } from './breakdown.service';

describe('Service: Breakdown', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BreakdownService]
    });
  });

  it('should ...', inject([BreakdownService], (service: BreakdownService) => {
    expect(service).toBeTruthy();
  }));
});
