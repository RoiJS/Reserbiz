/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { JWTInterceptorService } from './jwt-interceptor.service';

describe('Service: JWTInterceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JWTInterceptorService]
    });
  });

  it('should ...', inject([JWTInterceptorService], (service: JWTInterceptorService) => {
    expect(service).toBeTruthy();
  }));
});
