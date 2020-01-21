import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { throwError, BehaviorSubject, of } from 'rxjs';

import { RoutingService } from './routing.service';
import { DialogService } from './dialog.service';
import { StorageService } from './storage.service';
import { User } from '../_models/auth.model';

import { environment } from '../../environments/environment';

interface AuthResponseData {
  token: string;
  expiresIn: Date;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private _user = new BehaviorSubject<User>(null);
  private tokenExpirationTimer: any;

  constructor(
    private http: HttpClient,
    private routingService: RoutingService,
    private dialogService: DialogService,
    private storageService: StorageService
  ) {}

  get user() {
    return this._user.asObservable();
  }

  signUp(username: string, password: string) {
    return this.http
      .post<AuthResponseData>(
        `http://localhost:5000/api/auth/login`,
        {
          username: username,
          password: password
        }
      )
      .pipe(
        catchError(errorRes => {
          this.handleError(errorRes.error.error.message);
          return throwError(errorRes);
        }),

        tap(resData => {
          if (resData && resData.token) {
            this.handleLogin(
              username,
              resData.token,
              resData.expiresIn
            );
          }
        })
      );
  }

  login(username: string, password: string) {
    return this.http
      .post<AuthResponseData>(
        `${environment.reserbizAPIEndPoint}/auth/login`,
        {
          username: username,
          password: password
        }
      )
      .pipe(
        catchError(errorRes => {
          this.handleError(errorRes.error.error.message);
          return throwError(errorRes);
        }),

        tap(resData => {
          if (resData && resData.token) {
            this.handleLogin(
              username,
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
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.routingService.replace(['/auth']);
  }

  autoLogin() {
    if (!this.storageService.hasKey('userData')) {
      return of(false);
    }
    const userData: {
      username: string;
      _token: string;
      _tokenExpirationDate: string;
    } = JSON.parse(this.storageService.getString('userData'));

    const loadedUser = new User(
      userData.username,
      userData._token,
      new Date(userData._tokenExpirationDate)
    );

    if (loadedUser.isAuth) {
      this._user.next(loadedUser);
      this.autoLogout(loadedUser.timeToExpiry);
      return of(true);
    }

    return of(false);
  }

  autoLogout(expiryDuration) {
    this.tokenExpirationTimer = setTimeout(() => {
      this.logout();
    }, expiryDuration);
  }

  private handleLogin(
    username: string,
    token: string,
    expiresIn: Date
  ) {
    const expirationDate = new Date(expiresIn);
    const user = new User(username, token, expirationDate);
    this.storageService.storeString('userData', JSON.stringify(user));
    this.autoLogout(user.timeToExpiry);
    this._user.next(user);
  }

  private handleError(errorMessage: string) {
    switch (errorMessage) {
      case 'EMAIL_EXISTS':
        this.dialogService.alert('This email address exists already.');
        break;
      case 'INVALID_PASSWORD':
        this.dialogService.alert('Your password is invalid.');
        break;
      default:
        this.dialogService.alert(
          'Authentication failed. Please check your credentials.'
        );
    }
    console.error(errorMessage);
  }
}
