import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';
import * as AuthActions from '../store/auth/auth.actions';
import { AuthState } from '../store/auth/auth.reducer';

interface AppState {
  auth: AuthState;
}
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  username: string = '';
  password: string = '';
  loginError$: Observable<string | null>;
  registrationStatus: boolean | null = null;

  constructor(
    private store: Store<AppState>,
    private authService: AuthService,
    public toastr: ToastrService
  ) {
    this.loginError$ = this.store.pipe(select('auth', 'error'));
  }

  ngOnInit(): void {
    this.toastr.success(
      'Hello, the API is hosted via ngrok. If you are lucky you can access it !!'
    );
    this.authService.registrationStatus$.subscribe((rstatus) => {
      this.registrationStatus = rstatus;
    });
  }

  onSubmit() {
    this.registrationStatus = false;
    this.store.dispatch(
      AuthActions.login({ userName: this.username, password: this.password })
    );
  }
}
