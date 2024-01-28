import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { ApiService } from '../shared/api.service';
import { AuthState } from '../store/auth/auth.reducer';
import { selectUserId } from '../store/auth/auth.selectors';
import { ExpenseModel } from './expense.model';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationDialogService } from '../components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.css'],
})
export class ExpenseComponent implements OnInit {
  expenseForm: FormGroup;
  userId: string;
  categories: any[];
  postExpense: ExpenseModel;
  expensesList: any = {};
  itemsPerPage: number;
  totalItems: number;
  constantPageSize: number = 7;
  editingOrSaving: boolean;
  deletingRecord: boolean;
  updatingRecord: boolean;

  constructor(
    private store: Store<{ auth: AuthState }>,
    private apiService: ApiService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    public toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: ConfirmationDialogService
  ) {
    this.expenseForm = this.fb.group({
      description: ['', Validators.required],
      category: ['', Validators.required],
      expenseType: ['', Validators.required],
      amount: ['', Validators.required],
      date: ['', Validators.required],
      expenseId: [''],
    });
  }

  ngOnInit(): void {
    this.store.select(selectUserId).subscribe((userId) => {
      this.userId = userId;
    });
    this.apiService
      .get('Category/get-categories', {
        UserId: this.userId,
        Page: '1',
        PageSize: '100',
      })
      .subscribe((response) => {
        this.categories = response.data.categories;
      });
    this.fetchExpenses(1, this.constantPageSize);
  }

  onSave() {
    if (this.expenseForm.valid) {
      this.spinner.show();
      this.editingOrSaving = true;
      const formValue = this.expenseForm.value;
      this.postExpense = {
        Description: formValue.description,
        Amount: formValue.amount,
        Date: formValue.date,
        ExpenseType: Number(formValue.expenseType),
        CategoryId: formValue.category,
        UserId: this.userId,
      };
      console.log('post : ', this.postExpense);
      this.apiService
        .post('Expense/create-expense', this.postExpense)
        .subscribe(
          (response) => {
            if (response.success) {
              console.log('Res : ', response);
              this.expenseForm.reset();
              this.fetchExpenses(1, this.constantPageSize);
              this.editingOrSaving = false;
              this.spinner.hide();
              this.toastr.success('Record Saved');
            } else {
              console.log('Res : ', response);
              this.editingOrSaving = false;
            }
          },
          (error) => {
            console.log('Error', error);
            this.editingOrSaving = false;
          }
        );
    } else {
      console.log('not valid to do toaster');
    }
  }

  fetchExpenses(page: number, pageSize: number): void {
    this.spinner.show();
    this.apiService
      .get('Expense/get-expenses', {
        UserId: this.userId,
        Page: page.toString(),
        PageSize: pageSize.toString(),
      })
      .subscribe((response) => {
        this.expensesList = response.data;
        this.spinner.hide();
      });
  }
  onPageChange(page: number): void {
    console.log(page);
    this.fetchExpenses(page, this.expensesList.pageSize);
  }
  onEditExpense(editExpense: any) {
    this.expenseForm.patchValue({
      description: editExpense.description,
      category: editExpense.categoryId,
      expenseType: editExpense.expenseType,
      amount: editExpense.amount,
      date: this.datePipe.transform(editExpense.date, 'yyyy-MM-dd'),
      expenseId: editExpense.expenseId,
    });
    this.editingOrSaving = true;
  }
  onDeleteExpense(expenseId: string) {
    this.dialog.openConfirmationDialog('').subscribe((res) => {
      if (res) {
        this.spinner.show();
        console.log('delete: ', expenseId);
        this.deletingRecord = true;
        this.apiService.delete(`Expense/${expenseId}`).subscribe((response) => {
          if (response.success) {
            console.log('deleted : ', response);
            this.fetchExpenses(1, this.constantPageSize);
            this.deletingRecord = false;
            this.spinner.hide();
            this.toastr.warning('Record Deleted');
          }
        });
      } else {
        this.toastr.warning('Record Deletion Cancelled');
      }
    });
  }

  onUpdateExpense() {
    this.updatingRecord = true;
    const updateFormValue = this.expenseForm.value;
    const data = {
      amount: updateFormValue.amount,
      categoryId: updateFormValue.category,
      date: updateFormValue.date,
      description: updateFormValue.description,
      expenseType: Number(updateFormValue.expenseType),
      expenseId: updateFormValue.expenseId,
    };
    console.log(data);
    this.apiService
      .put(`Expense/${updateFormValue.expenseId}`, data)
      .subscribe((response) => {
        if (response.success) {
          this.fetchExpenses(1, this.constantPageSize);
          this.editingOrSaving = false;
          this.updatingRecord = false;
          this.toastr.success('Record Updated');
        } else {
          this.toastr.error('Update Failed');
        }
      });

    this.expenseForm.reset();
  }
}
