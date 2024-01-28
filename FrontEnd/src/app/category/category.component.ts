import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { ApiService } from '../shared/api.service';
import { AuthState } from '../store/auth/auth.reducer';
import { selectUserId } from '../store/auth/auth.selectors';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmationDialogService } from '../components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
})
export class CategoryComponent implements OnInit {
  userId: string;
  constantPageSize: number = 7;
  categoryList: any = {};
  categoryForm: FormGroup;
  isSaving: boolean;
  isUpdating: boolean = false;
  isDelete: boolean;

  constructor(
    private store: Store<{ auth: AuthState }>,
    private apiService: ApiService,
    private fb: FormBuilder,
    public toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private dialog: ConfirmationDialogService
  ) {
    this.categoryForm = this.fb.group({
      categoryName: ['', Validators.required],
      categoryDescription: ['', Validators.required],
      categoryId: [''],
    });
  }

  ngOnInit(): void {
    this.store.select(selectUserId).subscribe((userId) => {
      this.userId = userId;
    });
    this.fetchCategories(1, this.constantPageSize);
  }

  fetchCategories(page: number, pageSize: number) {
    this.spinner.show();
    this.apiService
      .get('Category/get-categories', {
        UserId: this.userId,
        Page: page.toString(),
        PageSize: pageSize.toString(),
      })
      .subscribe((response) => {
        if (response.success) {
          this.categoryList = response.data;
          this.spinner.hide();
        }
      });
  }

  onPageChange(page: number): void {
    this.fetchCategories(page, this.categoryList.pageSize);
  }

  onSave() {
    this.spinner.show();
    if (this.categoryForm.valid) {
      this.isSaving = true;
      const data = {
        categoryName: this.categoryForm.value.categoryName,
        categoryDescription: this.categoryForm.value.categoryDescription,
        userId: this.userId,
        isDefault: false,
      };
      this.apiService
        .post('Category/create-category', data)
        .subscribe((response) => {
          console.log('post cat', response);
          this.categoryForm.reset();
          this.isSaving = false;
          this.fetchCategories(1, this.constantPageSize);
          this.spinner.show();
          this.toastr.success('Record Saved');
        });
    } else {
      console.log('not valid to do toaster');
    }
  }

  onEdit(category: any) {
    console.log(category);
    this.categoryForm.patchValue({
      categoryName: category.categoryName,
      categoryDescription: category.categoryDescription,
      categoryId: category.categoryId,
    });
    this.isSaving = true;
  }

  onUpdate() {
    this.spinner.show();
    this.isUpdating = true;
    this.apiService
      .put(`Category/${this.categoryForm.value.categoryId}`, {
        categoryId: this.categoryForm.value.categoryId,
        categoryName: this.categoryForm.value.categoryName,
        categoryDescription: this.categoryForm.value.categoryDescription,
        userId: this.userId,
        isDefault: false,
      })
      .subscribe((res) => {
        console.log(res);
        this.fetchCategories(1, this.constantPageSize);
        this.isUpdating = false;
        this.categoryForm.reset();
        this.spinner.hide();
        this.toastr.success('Record Updated');
      });
  }

  onDelete(categoryId: string) {
    this.dialog
      .openConfirmationDialog(
        'Deleting this category will delete, its related expenses. This will effect your current Balance'
      )
      .subscribe((res) => {
        if (res) {
          this.spinner.show();
          this.isDelete = true;
          this.apiService.delete(`Category/${categoryId}`).subscribe((res) => {
            console.log(res);
            this.isDelete = false;
            this.fetchCategories(1, this.constantPageSize);
            this.spinner.hide();
            this.toastr.warning('Record Deleted');
          });
        } else {
          this.toastr.warning('Delete Cancelled');
        }
      });
  }
}
