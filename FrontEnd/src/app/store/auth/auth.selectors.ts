import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from './auth.reducer';

//creates a selector for a specific feature state in your store.
export const selectAuthState = createFeatureSelector<AuthState>('auth');

//creates a selector for extracting specific pieces of data from the store.
export const selectAuthToken = createSelector(
  selectAuthState,
  (authState: AuthState) => authState.user?.token
);

export const selectUserId = createSelector(
  selectAuthState,
  (authState: AuthState) => authState.user?.userId
);

export const selectUserName = createSelector(
  selectAuthState,
  (authState: AuthState) => authState.user?.userName
);
