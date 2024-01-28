import { NgModule } from '@angular/core';
import { ExpenseComponent } from './expense.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';

const routes: Routes = [{ path: '', component: ExpenseComponent }];

@NgModule({
  declarations: [ExpenseComponent],
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    NgxPaginationModule,
  ],
  exports: [RouterModule],
})
export class ExpenseModule {}
