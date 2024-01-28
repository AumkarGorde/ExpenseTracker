import { Injectable } from '@angular/core';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import DataLabelsPlugin from 'chartjs-plugin-datalabels';

@Injectable({
  providedIn: 'root',
})
export class RadarChartService {
  getChartOptions(): ChartConfiguration['options'] {
    return {};
  }
  getChartData(
    labels: string[],
    expenseData: number[],
    incomeData: number[] | null
  ): ChartData<'radar'> {
    console.log('labels', labels);
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
    return 'radar';
  }
}
