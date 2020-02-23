import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { take, switchMap, tap } from 'rxjs/operators';

import { AuthService } from '../_services/auth.service';
import { AuthToken } from '../_models/auth-token.model';

@Injectable()
export class AuthGuard implements CanLoad {
  constructor(private authService: AuthService, private router: Router) {}

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    return this.authService.authToken.pipe(
      take(1),
      switchMap((tokenInfo: AuthToken) => {
        if (!tokenInfo || !tokenInfo.token) {
          return this.authService.autoLogin();
        }

        return of(true);
      }),
      tap(isAuth => {
        if (!isAuth) {
          this.router.navigate(['/auth']);
        }
      })
    );
  }
}
