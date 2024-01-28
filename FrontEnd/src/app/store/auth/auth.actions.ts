import { createAction, props } from '@ngrx/store';

import { AuthResponse } from '../../auth/auth.model';
export const login = createAction(
  '[Auth] Login',
  props<{ userName: string; password: string }>()
);

export const loginSuccess = createAction(
  '[Auth] Login Success',
  props<{ authResponse: AuthResponse }>()
);

export const loginFailure = createAction(
  '[Auth] Login Failure',
  props<{ error: string }>()
);

export const logout = createAction('[Auth] Logout');
