using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface IDashboardService
    {
        Task<GetSummaryResponse> GetExpenseSummary(GetSummaryQuery request);
        Task<bool> CreateBudgetLimit(CreateBudgetLimitCommand request);
        Task<GetBudgetOverviewResponse> GetBudgetOverview(GetBudgetOverviewQuery request);
        Task<List<GetTopSpentsResponse>> GetTotalSpents(GetTopSpentsQuery request);
        Task<GetFinancialGoalsResponse> GetFinancialGoals(GetFinancialGoalsQuery request);
        Task<IEnumerable<GetRecentTransactionsResponse>> GetRecentTransactions(GetRecentTransactionsQuery request);
    }
}
