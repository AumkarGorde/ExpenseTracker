import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthResponse } from '../auth/auth.model';
import { ApiService } from '../shared/api.service';
import { AuthState } from '../store/auth/auth.reducer';
import { selectUserId } from '../store/auth/auth.selectors';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  user$: Observable<AuthResponse['data'] | null>;
  userName$: Observable<String | ''>;
  userId: string;

  summaryTotalExpense: string = '0';
  summaryRemainigBudget: string = '0';
  budgetOverviewBudgetLimit: string = '0';
  budgetOverviewSpent: string = '0';
  summaryTotalBalance: string = '0';
  summaryTotalIncome: string = '0';
  totalSpentsData: [];
  savingsGoal: string = '0';
  isTotalSpentGreaterThanBudgetLimit: boolean;
  isRemainingBudgetNegative: boolean;
  recentTransactions: [];

  constructor(
    private store: Store<{ auth: AuthState }>,
    private apiService: ApiService,
    public toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {
    this.user$ = this.store.pipe(select('auth', 'user'));
    this.userName$ = this.user$.pipe(map((user) => user?.firstName));
  }

  ngOnInit(): void {
    this.store.select(selectUserId).subscribe((userId) => {
      this.userId = userId;
    });
    const queryParams = {
      StartDate: new Date(
        new Date().getFullYear(),
        new Date().getMonth(),
        1
      ).toLocaleString(),
      EndDate: new Date().toLocaleString(),
      UserId: this.userId,
    };
    //this.spinner.show();
    /* summary */
    this.apiService
      .get('Dashboard/dashboard-summary', queryParams)
      .pipe(
        catchError((error) => {
          console.error('Error fetching data:', error.error.error);
          return throwError(error);
        })
      )
      .subscribe((response) => {
        console.log('summary ', response);
        if (response.success) {
          console.log('check bal', response);
          this.summaryTotalExpense = response.data.totalExpense;
          this.summaryRemainigBudget = response.data.remainingBudget;
          this.summaryTotalBalance = response.data.balance;
          this.summaryTotalIncome = response.data.totalIncome;
          const remainingBudgetNumber = parseInt(
            response.data.remainingBudget,
            10
          );
          this.isRemainingBudgetNegative = remainingBudgetNumber < 0;
          this.spinner.hide();
          if (this.isRemainingBudgetNegative) {
            this.toastr.warning('No Savings This Month');
          }
        } else {
          this.summaryTotalExpense = '0';
          this.summaryRemainigBudget = '0';
          this.spinner.hide();
        }
      });
    /* budget overview */
    this.apiService
      .get('Dashboard/dashboard-budget-overview', queryParams)
      .subscribe((response) => {
        if (response.success) {
          this.budgetOverviewBudgetLimit = response.data.budegetLimit;
          this.budgetOverviewSpent = response.data.totalSpent;
          this.isTotalSpentGreaterThanBudgetLimit =
            +this.budgetOverviewSpent > +this.budgetOverviewBudgetLimit;
          if (this.isTotalSpentGreaterThanBudgetLimit) {
            this.toastr.warning('Budget Limit Exceded');
          }
        } else {
          this.budgetOverviewBudgetLimit = '0';
          this.budgetOverviewSpent = '0';
          this.isTotalSpentGreaterThanBudgetLimit = false;
        }
      });
    /*Top Spents*/
    this.apiService
      .get('Dashboard/dashboard-top-spents', queryParams)
      .subscribe((response) => {
        if (response.success) {
          this.totalSpentsData = response.data;
        }
      });
    /*Financial Goals*/
    const userIdQuery = {
      UserId: this.userId,
    };
    this.apiService
      .get('Dashboard/dashboard-financial-goals', userIdQuery)
      .subscribe((response) => {
        if (response.success) {
          this.savingsGoal = response.data.savingsGoal;
        }
      });
    /*Recent Transactions*/

    this.apiService
      .get('Dashboard/dashboard-recent-transactions', userIdQuery)
      .subscribe((response) => {
        if (response.success) {
          this.recentTransactions = response.data;
        }
      });
  }
}
