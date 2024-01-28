import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { AuthResponse } from './auth.model';
import { environment } from 'src/environments/environment';
import { RegisterResponse } from './auth.register.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private registrationStatusSubject = new BehaviorSubject<boolean | null>(null);
  registrationStatus$ = this.registrationStatusSubject.asObservable();
  constructor(private http: HttpClient) {}

  login(UserName: string, Password: string): Observable<AuthResponse> {
    const creds = { UserName, Password };
    return this.http.post<AuthResponse>(
      `${environment.apiUrl}User/login`,
      creds
    );
  }

  register(
    FirstName: string,
    LastName: string,
    Email: string,
    Password: string,
    BudgetLimit: number,
    SavingsGoal: number,
    Balance: number
  ) {
    const reg = {
      FirstName,
      LastName,
      Email,
      Password,
      BudgetLimit,
      SavingsGoal,
      Balance,
    };
    return this.http.post<RegisterResponse>(
      `${environment.apiUrl}User/register`,
      reg
    );
  }

  setRegistrationStatus(status: boolean) {
    this.registrationStatusSubject.next(status); // Emit a new value (true) to subscribers
  }
}
