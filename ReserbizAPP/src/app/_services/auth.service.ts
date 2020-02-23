import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { catchError, tap } from 'rxjs/operators';
import { throwError, BehaviorSubject, of, Observable } from 'rxjs';
import { environment } from '../../environments/environment';

import { RoutingService } from './routing.service';
import { DialogService } from './dialog.service';
import { StorageService } from './storage.service';

import { User } from '../_models/user.model';
import { UserPersonalInfoFormSource } from '../_models/user-personal-form.model';
import { GenderEnum } from '../_enum/gender.enum';
import { AuthToken } from '../_models/auth-token.model';

interface AuthResponseData {
  token: string;
  currentUser: User;
  expiresIn: Date;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private _user = new BehaviorSubject<User>(null);
  private _tokenInfo = new BehaviorSubject<AuthToken>(null);
  private _tokenExpirationTimer: any;
  private _jwtHelper = new JwtHelperService();

  constructor(
    private http: HttpClient,
    private routingService: RoutingService,
    private dialogService: DialogService,
    private storageService: StorageService
  ) {}

  get user() {
    return this._user.asObservable();
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
      .post<AuthResponseData>(`${environment.reserbizAPIEndPoint}/auth/login`, {
        username: username,
        password: password
      })
      .pipe(
        catchError(errorRes => {
          this.handleError(errorRes.error.error.message);
          return throwError(errorRes);
        }),

        tap(resData => {
          if (resData && resData.token) {
            this.handleLogin(
              resData.currentUser,
              resData.token,
              resData.expiresIn
            );
          }
        })
      );
  }

  logout() {
    this._user.next(null);
    this.storageService.remove('userData');
    if (this._tokenExpirationTimer) {
      clearTimeout(this._tokenExpirationTimer);
    }
    this.routingService.replace(['/auth']);
  }

  updatePersonalInformation(user: UserPersonalInfoFormSource): Observable<any> {
    const tokenDecrypted = this._jwtHelper.decodeToken(
      this._tokenInfo.value.token
    );
    return this.http.put(
      `${environment.reserbizAPIEndPoint}/auth/updatePersonalInformation/${tokenDecrypted.nameid}`,
      user
    );
  }

  autoLogin() {
    if (!this.storageService.hasKey('authToken')) {
      return of(false);
    }

    const authToken: {
      _token: string;
      _tokenExpirationDate: Date;
    } = JSON.parse(this.storageService.getString('authToken'));

    const user: {
      firstName: string;
      middleName: string;
      lastName: string;
      username: string;
      gender: GenderEnum;
    } = JSON.parse(this.storageService.getString('userData'));

    const tokenInfo = new AuthToken(
      authToken._token,
      new Date(authToken._tokenExpirationDate)
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

  private handleLogin(currentUser: User, token: string, expiresIn: Date) {
    const expirationDate = new Date(expiresIn);

    const authToken = new AuthToken(token, expirationDate);

    const user = new User(
      currentUser.firstName,
      currentUser.middleName,
      currentUser.lastName,
      currentUser.username,
      currentUser.gender
    );

    this.storageService.storeString('authToken', JSON.stringify(authToken));
    this.storageService.storeString('userData', JSON.stringify(user));

    this.autoLogout(authToken.timeToExpiry);

    this._tokenInfo.next(authToken);
    this._user.next(user);
  }

  private handleError(errorMessage: string) {
    switch (errorMessage) {
      case 'EMAIL_EXISTS':
        this.dialogService.alert(
          'Authentication Failed',
          'This email address exists already.'
        );
        break;
      case 'INVALID_PASSWORD':
        this.dialogService.alert(
          'Authentication Failed',
          'Your password is invalid.'
        );
        break;
      default:
        this.dialogService.alert(
          'Authentication Failed',
          'Please check your credentials.'
        );
    }
    console.error(errorMessage);
  }
}
