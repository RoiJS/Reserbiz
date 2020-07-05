export class AuthToken {
  constructor(
    private _accessToken: string,
    private _refreshToken: string,
    private _refreshTokenExpirationDate: Date
  ) {}

  get isAuth(): boolean {
    return this._refreshTokenExpirationDate > new Date();
  }

  get token(): string {
    if (!this._accessToken) {
      return null;
    }

    return this._accessToken;
  }

  get refreshToken(): string {
    if (!this._refreshToken) {
      return null;
    }

    return this._refreshToken;
  }

  get timeToExpiry(): number {
    return this._refreshTokenExpirationDate.getTime() - new Date().getTime();
  }
}
