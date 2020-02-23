import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
  HttpHandler,
  HttpRequest,
  HttpEvent
} from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';

@Injectable()
export class ErrorInterceptorService implements HttpInterceptor {
  constructor() {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error.status === 401) {
          return throwError(error.statusText);
        }

        if (error instanceof HttpErrorResponse) {
          const applicationError = error.headers.get('Application-Error');
          if (applicationError) {
            return throwError(applicationError);
          }
        }

        const serverError = error.error;
        let modalStateErrors = '';
        if (serverError.errors && typeof serverError.errors === 'object') {
          for (const key in serverError.errors) {
            if (serverError.errors[key]) {
              modalStateErrors += serverError.errors[key] + '\n';
            }
          }
        }

        return throwError(modalStateErrors || serverError || 'Server Error');
      })
    );
  }
}

export const ErrorInteceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptorService,
  multi: true
};
