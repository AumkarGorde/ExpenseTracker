import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registrationForm: FormGroup;
  registrationFailed: boolean;
  serverSideFailure: string;
  loading: boolean;
  constructor(
    private authService: AuthService,
    private router: Router,
    public toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder
  ) {
    this.registrationForm = this.fb.group(
      {
        firstname: ['', Validators.required],
        lastname: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.pattern(
              /^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$/
            ),
          ],
        ],
        budgetlimit: [null, Validators.required],
        savingsgoal: [null, Validators.required],
        balance: [null, Validators.required],
      },
      { validators: this.compareValues('budgetlimit', 'savingsgoal') }
    );
  }

  compareValues(budgetControlName: string, savingsControlName: string) {
    return (formGroup: FormGroup) => {
      const budgetControl = formGroup.get(budgetControlName);
      const savingsControl = formGroup.get(savingsControlName);

      if (
        budgetControl.value !== null &&
        savingsControl.value !== null &&
        budgetControl.value <= savingsControl.value
      ) {
        savingsControl.setErrors({ invalidComparison: true });
      } else {
        savingsControl.setErrors(null);
      }
    };
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      this.spinner.show();
      const formValue = this.registrationForm.value;
      this.loading = true;
      this.authService
        .register(
          formValue.firstname,
          formValue.lastname,
          formValue.email,
          formValue.password,
          formValue.budgetlimit,
          formValue.savingsgoal,
          formValue.balance
        )
        .subscribe(
          (response) => {
            this.spinner.hide();
            if (response.success) {
              this.authService.setRegistrationStatus(true);
              this.router.navigate(['/login']);
              this.toastr.success('Registration Successfull');
              this.loading = false;
            } else {
              this.registrationFailed = true;
              this.serverSideFailure =
                'Registration Failed, Please Contact Admin';
              this.loading = false;
            }
          },
          (error) => {
            if (error.status === 400) {
              this.registrationFailed = true;
              this.serverSideFailure = error.error.data[0].errorMessage;
              this.loading = false;
            }
          }
        );
    }
  }
}
