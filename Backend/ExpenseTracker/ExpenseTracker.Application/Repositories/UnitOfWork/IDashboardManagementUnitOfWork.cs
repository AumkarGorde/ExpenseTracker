using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface IDashboardManagementUnitOfWork
    {
        IBudgetRepository BudgetRepository { get; }
        IExpensesRepository ExpensesRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }
        Task SaveChanges();

        Task<List<GetTopSpentsResponse>> GetTotalSpents(DateTime startDate, DateTime endDate, Guid userId);
        Task<IEnumerable<GetRecentTransactionsResponse>> GetRecentTransactions(Guid usedId);

    }
}
