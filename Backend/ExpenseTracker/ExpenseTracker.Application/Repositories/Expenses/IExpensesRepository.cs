using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface IExpensesRepository : IRepository<Expenses>
    {
        Task<GetExpensePaginatedResponse> GetExpenseByUser(Guid userId, int page, int pageSize);
        long GetExpenseByUserForSpecificPeriod(DateTime startDate, DateTime endTime, Guid userId, bool isExpense = false);

        Task<IEnumerable<Expenses>> GetExpensePerCategoryEachUser(Guid userId, Guid categoryId);

        #region Reports
        Task<IEnumerable<Expenses>> GetWeeklyExpenseIncomeReport(Guid userId, int Year, int Month);
        Task<IEnumerable<Expenses>> GetCategoryExpenseIncomeReport(Guid userId, int Year, int Month, bool isExpense);
        #endregion
    }
}
