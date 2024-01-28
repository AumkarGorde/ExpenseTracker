import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { BarChartService } from '../shared/chart.bar.service';
import { PolarAreaChartService } from '../shared/chart.polar.service';
import { RadarChartService } from '../shared/chart.radar.service';
import { ToastrService } from 'ngx-toastr';
import { map, pipe, forkJoin } from 'rxjs';
import { Observable } from 'rxjs';
import { ApiService } from '../shared/api.service';
import { Store } from '@ngrx/store';
import { AuthState } from '../store/auth/auth.reducer';
import { selectUserId } from '../store/auth/auth.selectors';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css'],
})
export class ReportComponent implements OnInit {
  @ViewChild(BaseChartDirective) categoryChart: BaseChartDirective | undefined;
  @ViewChild(BaseChartDirective) weeklyRadarChart:
    | BaseChartDirective
    | undefined;
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  @ViewChild(BaseChartDirective) chartCat: BaseChartDirective | undefined;
  getReportForm: FormGroup;
  userId: string;
  constructor(
    private store: Store<{ auth: AuthState }>,
    private chartService: BarChartService,
    private radarChartService: RadarChartService,
    private polarAreaService: PolarAreaChartService,
    private apiService: ApiService,
    private fb: FormBuilder,
    public toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {
    this.getReportForm = this.fb.group({
      month: ['', Validators.required],
      year: ['', Validators.required],
    });
  }

  weeklyWeekNumbers: [];
  weeklyExpenses: [];
  weeklyIncome: [];

  weeklyRadarChartOptions: ChartConfiguration['options'];
  weeklyRadarChartType: ChartType;
  weeklyRadarChartData: ChartData<'radar'>;

  categoryChartType: ChartType;
  categoryChartData: ChartData<'polarArea'>;
  categoryChartpolarAreaLegend: boolean = true;

  barChartOptions: ChartConfiguration['options'];
  barChartType: ChartType;
  barChartData: ChartData<'bar'>;

  categoryBarChartOptions: ChartConfiguration['options'];
  categoryBarChartType: ChartType;
  categoryBarChartData: ChartData<'bar'>;

  ngOnInit(): void {
    console.log('Before showing spinner');
    this.spinner.show();
    this.store.select(selectUserId).subscribe((userId) => {
      this.userId = userId;
    });

    forkJoin([
      this.loadCategoryReport('2024', '1'),
      this.loadWeeklyReport('2024', '1'),
      this.loadCategoryExpenseReport('2024', '1'),
    ]).subscribe(() => {
      this.spinner.hide();
    });
  }

  //load categorywise Expense;
  loadCategoryExpenseReport(year: string, month: string) {
    return this.fetchCategoryExpenseData(this.userId, year, month).pipe(
      map((res) => {
        if (!res.success) {
          this.toastr.warning('Failed to retrive Category Report');
        }
        this.categoryChartData = this.polarAreaService.getChartData(
          res.data.map((item) => item.categoryName),
          res.data.map((item) => item.expenseAmount),
          null
        );
        this.categoryChartType = this.polarAreaService.getChartType();
        return res;
      })
    );
  }

  loadWeeklyReport(year: string, month: string) {
    return this.fetchWeeklyData(this.userId, year, month).pipe(
      map((res) => {
        if (!res.success) {
          this.toastr.warning('Failed to retrive Weekly Report');
        }
        this.weeklyRadarChartOptions = this.radarChartService.getChartOptions();
        this.weeklyRadarChartType = this.radarChartService.getChartType();
        this.weeklyRadarChartData = this.radarChartService.getChartData(
          res.data.map((item) => item.weekNumber),
          res.data.map((item) => item.expensePerWeek),
          res.data.map((item) => item.incomePerWeek)
        );
        this.barChartOptions = this.chartService.getChartOptions();
        this.barChartType = this.chartService.getChartType();
        this.barChartData = this.chartService.perWeekIncomeExpense(
          res.data.map((item) => item.weekNumber),
          res.data.map((item) => item.expensePerWeek),
          res.data.map((item) => item.incomePerWeek)
        );
        return res;
      })
    );
  }

  loadCategoryReport(year: string, month: string): Observable<any> {
    return this.fetchCategoryData(this.userId, year, month).pipe(
      map((res) => {
        if (!res.success) {
          this.toastr.warning('Failed to retrive Weekly Report');
        }
        this.categoryBarChartOptions = this.chartService.getChartOptions();
        this.categoryBarChartType = this.chartService.getChartType();
        this.categoryBarChartData = this.chartService.perWeekIncomeExpense(
          res.data.map((item) => item.categoryName),
          res.data.map((item) => item.expenseAmount),
          res.data.map((item) => item.incomeAmount)
        );
        return res;
      })
    );
  }

  onSubmit() {
    if (this.getReportForm.valid) {
      this.spinner.show();

      const formData = this.getReportForm.value;
      forkJoin([
        this.loadWeeklyReport(formData.year.toString(), formData.month),
        this.loadCategoryExpenseReport(
          formData.year.toString(),
          formData.month
        ),
        this.loadCategoryReport(formData.year.toString(), formData.month),
      ]).subscribe(() => {
        this.spinner.hide();
      });
    }
  }

  //API Requests
  fetchWeeklyData(userId: string, year: string, month: string) {
    return this.apiService.get(`Report/get-weekly-report`, {
      UserId: userId,
      Year: year,
      Month: month,
    });
  }

  fetchCategoryData(userId: string, year: string, month: string) {
    return this.apiService.get(`Report/get-category-report`, {
      UserId: userId,
      Year: year,
      Month: month,
    });
  }

  fetchCategoryExpenseData(userId: string, year: string, month: string) {
    return this.apiService.get(`Report/get-category-expense-report`, {
      UserId: userId,
      Year: year,
      Month: month,
    });
  }
}
