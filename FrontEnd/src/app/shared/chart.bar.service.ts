import { Injectable } from '@angular/core';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import DataLabelsPlugin from 'chartjs-plugin-datalabels';

@Injectable({
  providedIn: 'root',
})
export class BarChartService {
  perWeekIncomeExpense(
    labels: string[],
    expenseData: number[],
    incomeData: number[]
  ): ChartData<'bar'> {
    return {
      labels: labels,
      datasets: [
        { data: expenseData, label: 'Expense' },
        { data: incomeData, label: 'Income' },
      ],
    };
  }

  getChartOptions(): ChartConfiguration['options'] {
    return {
      scales: {
        x: {},
        y: {
          min: 10,
        },
      },
      plugins: {
        legend: {
          display: true,
        },
        datalabels: {
          anchor: 'end',
          align: 'end',
        },
      },
    };
  }

  getChartType(): ChartType {
    return 'bar';
  }

  getChartPlugins(): any[] {
    return [DataLabelsPlugin];
  }
}
