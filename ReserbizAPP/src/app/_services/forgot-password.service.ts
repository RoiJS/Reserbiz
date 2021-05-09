import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { environment } from '@src/environments/environment';

import { User } from '../_models/user.model';
import { ForgotPasswordUser } from '../_models/forgot-password-user.model';

@Injectable({ providedIn: 'root' })
export class ForgotPasswordService {
  private _user = new BehaviorSubject<ForgotPasswordUser>(null);
  private _appSecretToken = new BehaviorSubject<string>('');

  constructor(private http: HttpClient) {}

  verifyUsernameOrEmailAddress(usernameOrEmailAddress: string) {
    return this.http
      .post<User>(
        `${environment.reserbizAPIEndPoint}/forgotPassword/verifyUsernameOrEmailAddress/${usernameOrEmailAddress}`,
        null
      )
      .pipe(
        tap((resData: ForgotPasswordUser) => {
          if (resData) {
            this.handleVerification(resData);
          }
        })
      );
  }

  saveNewPassword(id: number, newPassword: string): Observable<any> {
    return this.http.put(
      `${environment.reserbizAPIEndPoint}/forgotPassword/saveNewPassword/${id}/${newPassword}`,
      null
    );
  }

  private handleVerification(user: ForgotPasswordUser) {
    this._user.next(user);
  }

  get user(): BehaviorSubject<ForgotPasswordUser> {
    return this._user;
  }

  get appSecretToken(): BehaviorSubject<string> {
    return this._appSecretToken;
  }
}
