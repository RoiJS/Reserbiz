import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { tap } from 'rxjs/operators';
import { BehaviorSubject, of, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

import { PushNotificationService } from './push-notification.service';
import { RoutingService } from './routing.service';
import { StorageService } from './storage.service';

import { User } from '../_models/user.model';
import { Client } from '../_models/client.model';
import { UserPersonalInfoFormSource } from '../_models/form/user-personal-form.model';
import { GenderEnum } from '../_enum/gender.enum';
import { AuthToken } from '../_models/auth-token.model';
import { UserAccountInfoFormSource } from '../_models/form/user-account-form.model';

interface IAuthResponseData {
  accessToken: string;
  refreshToken: string;
  currentUser: User;
  expiresIn: Date;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private _user = new BehaviorSubject<User>(null);
  private _tokenInfo = new BehaviorSubject<AuthToken>(null);
  private _currentUserFullname = new BehaviorSubject<string>('');
  private _currentUsername = new BehaviorSubject<string>('');
  private _tokenExpirationTimer: any;
  private _jwtHelper = new JwtHelperService();

  constructor(
    private http: HttpClient,
    private pushNotificationService: PushNotificationService,
    private routingService: RoutingService,
    private storageService: StorageService
  ) {}

  get user() {
    return this._user;
  }

  get currentFullname() {
    return this._currentUserFullname;
  }

  get currentUsername() {
    return this._currentUsername;
  }

  get authToken() {
    return this._tokenInfo.asObservable();
  }

  signUp(username: string, password: string) {
    // return this.http
    //   .post<AuthResponseData>(
    //     `http://localhost:5000/api/auth/login`,
    //     {
    //       username: username,
    //       password: password
    //     }
    //   )
    //   .pipe(
    //     catchError(errorRes => {
    //       this.handleError(errorRes.error.error.message);
    //       return throwError(errorRes);
    //     }),
    //     tap(resData => {
    //       if (resData && resData.token) {
    //         this.handleLogin(
    //           username,
    //           resData.token,
    //           resData.expiresIn
    //         );
    //       }
    //     })
    //   );
  }

  login(username: string, password: string) {
    return this.http
      .post<IAuthResponseData>(
        `${environment.reserbizAPIEndPoint}/auth/login`,
        {
          username: username,
          password: password,
        }
      )
      .pipe(
        tap((resData) => {
          if (resData && resData.accessToken) {
            this.handleLogin(
              resData.accessToken,
              resData.refreshToken,
              resData.expiresIn
            );
          }
        })
      );
  }

  checkCompany(company: string) {
    return this.http.get<Client>(
      `${environment.reserbizAPIEndPoint}/clients/${company}`
    );
  }

  refresh(): Observable<IAuthResponseData> {
    const storedTokenInfo = this.storageService.getString('authToken');

    const tokenInfo: {
      _accessToken: string;
      _refreshToken: string;
      _refreshTokenExpirationDate: Date;
    } = JSON.parse(storedTokenInfo);

    const authToken = new AuthToken(
      tokenInfo._accessToken,
      tokenInfo._refreshToken,
      new Date(tokenInfo._refreshTokenExpirationDate)
    );

    return this.http
      .post<IAuthResponseData>(
        `${environment.reserbizAPIEndPoint}/auth/refresh`,
        {
          accessToken: authToken.token,
          refreshToken: authToken.refreshToken,
        }
      )
      .pipe(
        tap((resData) => {
          if (resData && resData.accessToken) {
            this.handleLogin(
              resData.accessToken,
              resData.refreshToken,
              resData.expiresIn
            );
          }
        })
      );
  }

  logout(redirect: boolean = true) {
    this._user.next(null);
    this._tokenInfo.next(null);

    this.storageService.remove('userData');
    this.storageService.remove('authToken');
    this.storageService.remove('app-secret-token');

    if (this._tokenExpirationTimer) {
      clearTimeout(this._tokenExpirationTimer);
    }

    this.pushNotificationService.navigateToUrl.next(false);

    if (redirect) {
      this.routingService.replace(['/auth']);
    }
  }

  updatePersonalInformation(user: UserPersonalInfoFormSource): Observable<any> {
    return this.http.put(
      `${environment.reserbizAPIEndPoint}/auth/updatePersonalInformation/${this.userId}`,
      user
    );
  }

  updateAccountInformation(user: UserAccountInfoFormSource): Observable<any> {
    return this.http.put(
      `${environment.reserbizAPIEndPoint}/auth/updateAccountInformation/${this.userId}`,
      user
    );
  }

  validateUsernameExists(username: string): Observable<boolean> {
    return this.http
      .get(
        `${environment.reserbizAPIEndPoint}/auth/validateUsernameExists/${this.userId}/${username}`
      )
      .pipe(
        tap((res: boolean) => {
          return of(res);
        })
      );
  }

  autoLogin() {
    if (
      !this.storageService.hasKey('authToken') ||
      !this.storageService.hasKey('userData')
    ) {
      return of(false);
    }

    const authToken: {
      _accessToken: string;
      _refreshToken: string;
      _refreshTokenExpirationDate: Date;
    } = JSON.parse(this.storageService.getString('authToken'));

    const user: {
      firstName: string;
      middleName: string;
      lastName: string;
      username: string;
      gender: GenderEnum;
    } = JSON.parse(this.storageService.getString('userData'));

    const tokenInfo = new AuthToken(
      authToken._accessToken,
      authToken._refreshToken,
      new Date(authToken._refreshTokenExpirationDate)
    );

    const loadedUser = new User(
      user.firstName,
      user.middleName,
      user.lastName,
      user.username,
      user.gender
    );

    if (tokenInfo.isAuth) {
      this._user.next(loadedUser);
      this._tokenInfo.next(tokenInfo);
      this._currentUserFullname.next(loadedUser.fullname);
      this._currentUsername.next(loadedUser.username);

      this.autoLogout(tokenInfo.timeToExpiry);
      return of(true);
    }

    return of(false);
  }

  autoLogout(expiryDuration) {
    this._tokenExpirationTimer = setTimeout(() => {
      this.logout();
    }, expiryDuration);
  }

  private handleLogin(
    accessToken: string,
    refreshToken: string,
    expiresIn: Date
  ) {
    const expirationDate = new Date(expiresIn);

    const authToken = new AuthToken(accessToken, refreshToken, expirationDate);
    const currentUser = this._jwtHelper.decodeToken(authToken.token);

    const user = new User(
      currentUser.firstName,
      currentUser.middleName,
      currentUser.lastName,
      currentUser.username,
      parseInt(currentUser.gender)
    );

    this.storageService.storeString('authToken', JSON.stringify(authToken));
    this.storageService.storeString('userData', JSON.stringify(user));

    this.autoLogout(authToken.timeToExpiry);

    this._tokenInfo.next(authToken);
    this._user.next(user);

    this._currentUserFullname.next(user.fullname);
    this._currentUsername.next(user.username);
  }

  get userId(): number {
    const tokenDecrypted = this._jwtHelper.decodeToken(
      this._tokenInfo.value.token
    );

    return parseInt(tokenDecrypted.nameid);
  }
}
