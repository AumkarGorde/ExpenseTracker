import { Injectable } from '@angular/core';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';

@Injectable({
  providedIn: 'root',
})
export class PolarAreaChartService {
  getChartData(
    labels: string[],
    expenseData: number[],
    incomeData: number[] | null
  ): ChartData<'polarArea'> {
    if (incomeData === null) {
      return {
        labels: labels,
        datasets: [{ data: expenseData, label: 'Expense' }],
      };
    } else {
      return {
        labels: labels,
        datasets: [
          { data: expenseData, label: 'Expense' },
          { data: incomeData, label: 'Income' },
        ],
      };
    }
  }
  getChartType(): ChartType {
    return 'polarArea';
  }
}
