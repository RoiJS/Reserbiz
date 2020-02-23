import { Injectable } from '@angular/core';

import { environment } from '@src/environments/environment';

import { GenderEnum } from '../_enum/gender.enum';
import { StorageService } from './storage.service';
import { User } from '../_models/user.model';
import { AuthToken } from '../_models/auth-token.model';

@Injectable({
  providedIn: 'root'
})
export class JWTInterceptorService {}

export function jwtOptionsFactory(storageService: StorageService) {
  return {
    tokenGetter: () => {
      const storedTokenInfo = storageService.getString('authToken');

      if (!storedTokenInfo) {
        return '';
      }

      const tokenInfo: {
        _token: string;
        _tokenExpirationDate: Date;
      } = JSON.parse(storedTokenInfo);

      const authToken = new AuthToken(
        tokenInfo._token,
        new Date(tokenInfo._tokenExpirationDate)
      );

      return authToken.token;
    },
    whitelistedDomains: environment.whitelistedDomains
  };
}
