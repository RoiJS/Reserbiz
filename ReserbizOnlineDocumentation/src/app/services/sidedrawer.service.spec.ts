/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SidedrawerService } from './sidedrawer.service';

describe('Service: Sidedrawer', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SidedrawerService]
    });
  });

  it('should ...', inject([SidedrawerService], (service: SidedrawerService) => {
    expect(service).toBeTruthy();
  }));
});
