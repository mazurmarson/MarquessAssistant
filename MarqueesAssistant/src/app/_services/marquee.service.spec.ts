/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MarqueeService } from './marquee.service';

describe('Service: Marquee', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MarqueeService]
    });
  });

  it('should ...', inject([MarqueeService], (service: MarqueeService) => {
    expect(service).toBeTruthy();
  }));
});
