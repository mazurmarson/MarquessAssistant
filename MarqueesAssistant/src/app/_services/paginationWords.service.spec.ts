/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PaginationWordsService } from './paginationWords.service';

describe('Service: PaginationWords', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PaginationWordsService]
    });
  });

  it('should ...', inject([PaginationWordsService], (service: PaginationWordsService) => {
    expect(service).toBeTruthy();
  }));
});
