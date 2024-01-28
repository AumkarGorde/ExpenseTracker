using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface IReportService
    {
        Task<IEnumerable<GetWeeklyReportResponse>> GetWeeklyReport(GetWeeklyReportRequest request);
        Task<IEnumerable<GetCategoryExpenseReportResponse>> GetCategoryExpense(GetCategoryExpenseReportRequest request);
        Task<IEnumerable<GetCategoryReportResponse>> GetCategoryReport(GetCategoryReportRequest request);

    }
}
