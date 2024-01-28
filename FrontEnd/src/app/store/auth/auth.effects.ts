import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap, catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { Router } from '@angular/router';

import { AuthService } from '../../auth/auth.service';
import * as AuthActions from '../auth/auth.actions';

@Injectable()
export class AuthEffects {
  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private router: Router
  ) {}

  login$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.login),
      mergeMap((action) =>
        this.authService.login(action.userName, action.password).pipe(
          map((authResponse) => {
            if (authResponse.success) {
              this.router.navigate(['/dashboard']);
              return AuthActions.loginSuccess({ authResponse });
            } else {
              this.router.navigate(['/login']);
              return AuthActions.loginFailure({ error: authResponse.message });
            }
          }),
          catchError((error) => of(AuthActions.loginFailure({ error })))
        )
      )
    )
  );
}
