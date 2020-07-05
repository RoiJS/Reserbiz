import { StorageService } from '../_services/storage.service';
import { AuthToken } from '../_models/auth-token.model';

import { environment } from '@src/environments/environment';

export function jwtOptionsFactory(storageService: StorageService) {
  return {
    tokenGetter: () => {
      const storedTokenInfo = storageService.getString('authToken');

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
    },
    whitelistedDomains: environment.whitelistedDomains
  };
}
