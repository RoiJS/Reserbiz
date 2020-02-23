export class AuthToken {
  constructor(
    private _token: string,
    private _tokenExpirationDate: Date
  ) {}

  get isAuth(): boolean {
    return !!this.token;
  }

  get token(): string {
    if (!this._token) {
      return null;
    }

    if (!this._tokenExpirationDate || this._tokenExpirationDate < new Date()) {
      return null;
    }

    return this._token;
  }

  get timeToExpiry(): number {
    return this._tokenExpirationDate.getTime() - new Date().getTime();
  }
}
