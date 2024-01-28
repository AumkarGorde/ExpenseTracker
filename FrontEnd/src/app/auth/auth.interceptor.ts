import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { AuthState } from '../store/auth/auth.reducer';
import { selectAuthToken } from '../store/auth/auth.selectors';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private store: Store<AuthState>, private router: Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let authToken: string | null = null;
    // Retrieve the authentication token from the NgRx store
    this.store.select(selectAuthToken).subscribe((token) => {
      authToken = token;
      console.log('token retrived');
    });
    if (authToken) {
      // If a token is available, clone the request and add the authorization header
      const authRequest = req.clone({
        setHeaders: {
          Authorization: `Bearer ${authToken}`,
          'ngrok-skip-browser-warning': '2516',
        },
      });
      // Pass the cloned request to the next handler in the chain
      return next.handle(authRequest).pipe(
        catchError((error: any) => {
          if (error instanceof HttpErrorResponse && error.status === 401) {
            console.log('token expired , redirectung to login');
            this.router.navigate(['/login']);
          }
          return throwError(error);
        })
      );
    }
    // If no token is available, proceed with the original request
    return next.handle(req);
  }
}
