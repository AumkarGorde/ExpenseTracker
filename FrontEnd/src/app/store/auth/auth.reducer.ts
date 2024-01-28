import { createReducer, on } from '@ngrx/store';

import { AuthResponse } from 'src/app/auth/auth.model';
import * as AuthActions from './auth.actions';

export interface AuthState {
  isAuthenticated: boolean;
  user: AuthResponse['data'] | null;
  error: string | null;
}

const persistUserToLocalStorage = (user: AuthResponse['data']) => {
  localStorage.setItem('user', JSON.stringify(user));
};

const getUserFromLocalStorage = (): AuthResponse['data'] | null => {
  const storedUserData = localStorage.getItem('user');
  return storedUserData ? JSON.parse(storedUserData) : null;
};

export const initialState: AuthState = {
  isAuthenticated: false,
  user: getUserFromLocalStorage(),
  error: null,
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.loginSuccess, (state, { authResponse }) => {
    persistUserToLocalStorage(authResponse.data);
    return {
      ...state,
      isAuthenticated: true,
      user: authResponse.data,
      error: null,
    };
  }),
  on(AuthActions.loginFailure, (state, { error }) => ({
    ...state,
    isAuthenticated: false,
    user: null,
    error,
  })),
  on(AuthActions.logout, (state) => {
    localStorage.removeItem('user');
    return {
      ...state,
      isAuthenticated: false,
      user: null,
      error: null,
    };
  })
);
