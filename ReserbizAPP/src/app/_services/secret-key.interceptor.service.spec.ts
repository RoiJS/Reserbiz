/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SecretKey.interceptorService } from './secret-key.interceptor.service';

describe('Service: SecretKey.interceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SecretKey.interceptorService]
    });
  });

  it('should ...', inject([SecretKey.interceptorService], (service: SecretKey.interceptorService) => {
    expect(service).toBeTruthy();
  }));
});
