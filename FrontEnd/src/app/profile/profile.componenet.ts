import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Store } from '@ngrx/store';
import { AuthState } from '../store/auth/auth.reducer';
import { selectUserId, selectUserName } from '../store/auth/auth.selectors';
import { ApiService } from '../shared/api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmationDialogService } from '../components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.compoenet.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  editForm: FormGroup;
  showProfileEditor: boolean;
  userId: string;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  budgetLimit: number;
  savingGoal: number;
  budgetId: string;
  balance: number;
  constructor(
    private store: Store<{ auth: AuthState }>,
    public toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private apiService: ApiService,
    private fb: FormBuilder,
    private dialog: ConfirmationDialogService
  ) {
    this.editForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', Validators.required],
      budgetlimit: ['', Validators.required],
      savingsgoal: ['', Validators.required],
      budgetId: ['', Validators.required],
      balance: ['', Validators.required],
    });
  }
  ngOnInit(): void {
    this.showProfileEditor = false;
    this.store.select(selectUserId).subscribe((userId) => {
      this.userId = userId;
    });
    this.store.select(selectUserName).subscribe((userId) => {
      this.userName = userId;
    });
    this.fetchUserDetails();
  }
  onEditProfile() {
    this.showProfileEditor = true;
    this.editForm.patchValue({
      firstname: this.firstName,
      lastname: this.lastName,
      email: this.email,
      budgetlimit: this.budgetLimit,
      savingsgoal: this.savingGoal,
      budgetId: this.budgetId,
      balance: this.balance,
    });
  }

  onSubmit() {
    this.spinner.show();
    this.apiService
      .put(`User/update-user-details/${this.userId}`, {
        userId: this.userId,
        firstName: this.editForm.value.firstname,
        lastName: this.editForm.value.lastname,
        email: this.editForm.value.email,
        budgetLimit: this.editForm.value.budgetlimit,
        savingsGoal: this.editForm.value.savingsgoal,
        budgetId: this.editForm.value.budgetId,
        balance: this.editForm.value.balance,
      })
      .subscribe((res) => {
        if (!res.success) {
          this.toastr.warning('Failed to Save Data');
          this.spinner.hide();
        } else {
          this.toastr.success('Updated User Details');
          this.spinner.hide();
          this.showProfileEditor = false;
          this.editForm.reset();
          this.fetchUserDetails();
        }
      });
  }
  fetchUserDetails() {
    this.apiService
      .get(`User/get-user-details`, { UserId: this.userId })
      .subscribe((res) => {
        if (!res.success) {
          this.toastr.warning('Failed to retrive Weekly Report');
        } else {
          this.firstName = res.data.firstName;
          this.lastName = res.data.lastName;
          this.email = res.data.email;
          this.budgetId = res.data.budgetId;
          this.budgetLimit = res.data.budgetLimit;
          this.savingGoal = res.data.savingsGoal;
          this.balance = res.data.balance;
        }
      });
  }
}
