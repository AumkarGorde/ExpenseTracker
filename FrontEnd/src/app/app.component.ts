import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import * as AuthActions from './store/auth/auth.actions';
import { Observable } from 'rxjs';
import { AuthState } from './store/auth/auth.reducer';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  isAuthenticated$: Observable<boolean>;

  constructor(private store: Store<{ auth: AuthState }>) {
    this.isAuthenticated$ = this.store.select(
      (state) => !!state.auth.user?.firstName
    );
  }

  logout() {
    this.store.dispatch(AuthActions.logout());
  }
}
