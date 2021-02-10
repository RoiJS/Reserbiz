import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '@src/environments/environment';

import { StorageService } from './storage.service';

import { AuthToken } from '../_models/auth-token.model';

@Injectable({
  providedIn: 'root'
})
export class SecretKeyInterceptorService implements HttpInterceptor {
  constructor(private storageService: StorageService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

    const authReq = req.clone({
      setHeaders: {
        'App-Secret-Token': environment.appSecretToken,
        'Authorization': `Bearer ${this.tokenGetter()}`
      }
    });

    return next.handle(authReq);
  }

  tokenGetter() {
    const storedTokenInfo = this.storageService.getString('authToken');

    if (!storedTokenInfo) {
      return '';
    }

    const tokenInfo: {
      _accessToken: string;
      _refresTtoken: string;
      _refreshTokenExpirationDate: Date;
    } = JSON.parse(storedTokenInfo);

    const authToken = new AuthToken(
      tokenInfo._accessToken,
      tokenInfo._refresTtoken,
      new Date(tokenInfo._refreshTokenExpirationDate)
    );

    return authToken.token;
  }
}

export const SecretKeyInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: SecretKeyInterceptorService,
  multi: true
};

