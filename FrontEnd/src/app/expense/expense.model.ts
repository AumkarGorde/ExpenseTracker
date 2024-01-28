export interface ExpenseModel {
  Description: string;
  Amount: number;
  Date: Date;
  ExpenseType: number;
  CategoryId: string;
  UserId: string;
}

export interface ExpenseEditModel {
  Description: string;
  Amount: number;
  Date: Date;
  ExpenseType: number;
  CategoryId: string;
  CategoryName: string;
  ExpenseId: string;
}
