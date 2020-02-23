import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpHeaders,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SecretKeyInterceptorService implements HttpInterceptor {
  constructor() {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    const authReq = req.clone({
      setHeaders: {
        'App-Secret-Token': environment.appSecretToken
      }
    });

    return next.handle(authReq);
  }
}

export const SecretKeyInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: SecretKeyInterceptorService,
  multi: true
};

