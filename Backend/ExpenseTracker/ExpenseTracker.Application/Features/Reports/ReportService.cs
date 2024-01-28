using AutoMapper;
using ExpenseTracker.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class ReportService: BaseService, IReportService
    {
        private readonly IExpensesRepository _expensesRepository;
        public ReportService(ICustomLogger logger, IMapper mapper, IExpensesRepository expensesRepository) : base(logger, mapper)
        {
            _expensesRepository = expensesRepository;
        }

        public async Task<IEnumerable<GetCategoryExpenseReportResponse>> GetCategoryExpense(GetCategoryExpenseReportRequest request)
        {
            try
            {
                var result = await _expensesRepository
                           .GetCategoryExpenseIncomeReport(request.UserId, request.Year, Convert.ToInt16(request.Month), true);
                var categoryReport = result.GroupBy(e => e.Category)
                                     .Select(g => new GetCategoryExpenseReportResponse()
                                     {
                                         CategoryName = g.Key.CategoryName,
                                         ExpenseAmount = g.Sum(e => e.Amount)
                                     });
                return categoryReport;

            }
            catch (Exception)
            {
                return new List<GetCategoryExpenseReportResponse>() { };
            }
        }

        public async Task<IEnumerable<GetCategoryReportResponse>> GetCategoryReport(GetCategoryReportRequest request)
        {
            try
            {
                var result = await _expensesRepository
                           .GetCategoryExpenseIncomeReport(request.UserId, request.Year, Convert.ToInt16(request.Month), false);

                var categoryReport = result.GroupBy(e => e.Category)
                                        .Select(g => new GetCategoryReportResponse()
                                        {
                                            CategoryName = g.Key.CategoryName,
                                            ExpenseAmount = g.Where(e => e.ExpenseType == ExpenseType.Expenditure).Sum(e => e.Amount),
                                            IncomeAmount = g.Where(e => e.ExpenseType == ExpenseType.Income).Sum(e => e.Amount)
                                        });
                return categoryReport;
            }
            catch (Exception)
            {

                return new List<GetCategoryReportResponse>() { };
            }
        }

        public async Task<IEnumerable<GetWeeklyReportResponse>> GetWeeklyReport(GetWeeklyReportRequest request)
        {
            try
            {
                var result = await _expensesRepository
                            .GetWeeklyExpenseIncomeReport(request.UserId, request.Year, Convert.ToInt16(request.Month));

                var weeklyReport = result
                .GroupBy(r => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(r.Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday))
                .Select(g => new GetWeeklyReportResponse()
                {
                    Id = g.Key,
                    WeekNumber = $"Week-{g.Key}",
                    ExpensePerWeek = g.Where(r => r.ExpenseType == ExpenseType.Expenditure).Sum(r => r.Amount),
                    IncomePerWeek = g.Where(r => r.ExpenseType == ExpenseType.Income).Sum(r => r.Amount)
                })
                 .OrderBy(r => r.Id);
                return weeklyReport;
            }
            catch (Exception ex)
            {
                return new List<GetWeeklyReportResponse>() { };
            }
        }
    }
}
