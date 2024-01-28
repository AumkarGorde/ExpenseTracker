using AutoMapper;
using ExpenseTracker.Domain;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    internal class DashboardService : BaseService, IDashboardService
    {
        private readonly IDashboardManagementUnitOfWork _dashboardManagementUnitOfWork;
        public DashboardService(ICustomLogger logger, IMapper mapper, 
            IDashboardManagementUnitOfWork dashboardManagementUnitOfWork) : base(logger, mapper)
        {
            _dashboardManagementUnitOfWork = dashboardManagementUnitOfWork;
        }
        [Authorize(Roles = "USER")]
        public async Task<bool> CreateBudgetLimit(CreateBudgetLimitCommand request)
        {
            try
            {
                var budget = new Budget()
                {
                    BudgetLimit = request.BudgetLimit,
                    UserId = request.UserId,
                };
                await _dashboardManagementUnitOfWork.BudgetRepository.AddAsync(budget);
                await _dashboardManagementUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<GetSummaryResponse> GetExpenseSummary(GetSummaryQuery request)
        {
            try
            {
                var expense = _dashboardManagementUnitOfWork.ExpensesRepository
                               .GetExpenseByUserForSpecificPeriod(request.StartDate, request.EndDate, request.UserId,true);

                var income = _dashboardManagementUnitOfWork.ExpensesRepository
                               .GetExpenseByUserForSpecificPeriod(request.StartDate, request.EndDate, request.UserId);

                var budget = await _dashboardManagementUnitOfWork.BudgetRepository.GetBudgetByUserId(request.UserId);
                if(budget is not null)
                {
                    GetSummaryResponse summary = new GetSummaryResponse();
                    summary.TotalExpense = expense;
                    summary.TotalIncome = income;
                    summary.Balance = budget.Balance;
                    summary.RemainingBudget = budget.BudgetLimit - expense;
                    return summary;
                }
                else
                {
                    return new GetSummaryResponse()
                    {
                        TotalExpense = 0,
                        TotalIncome = 0,
                        Balance = 0,
                        RemainingBudget = 0,
                    };
                }

            }
            catch (Exception)
            {
                return new GetSummaryResponse()
                {
                    TotalExpense = 0,
                    TotalIncome = 0,
                    Balance = 0,
                    RemainingBudget = 0,
                };
            }
        }
        public async Task<GetBudgetOverviewResponse> GetBudgetOverview( GetBudgetOverviewQuery request)
        {
            try
            {
                var budget = await _dashboardManagementUnitOfWork.BudgetRepository.GetBudgetByUserId(request.UserId);
                var totalSpent = _dashboardManagementUnitOfWork.ExpensesRepository
                              .GetExpenseByUserForSpecificPeriod(request.StartDate, request.EndDate, request.UserId,true);
                if (budget is not null)
                    return new GetBudgetOverviewResponse(budget.BudgetLimit, totalSpent);
                else
                    return new GetBudgetOverviewResponse(0, 0);

            }
            catch (Exception)
            {
                return new GetBudgetOverviewResponse(0, 0);
            }
        }

        public async Task<List<GetTopSpentsResponse>> GetTotalSpents(GetTopSpentsQuery request)
        {
            try
            {
                return await _dashboardManagementUnitOfWork
                    .GetTotalSpents(request.StartDate, request.EndDate, request.UserId);
            }
            catch (Exception)
            {
                return new List<GetTopSpentsResponse> { };
            }
        }

        public async Task<GetFinancialGoalsResponse> GetFinancialGoals(GetFinancialGoalsQuery request)
        {
            try
            {
                var budget = await _dashboardManagementUnitOfWork.BudgetRepository.GetBudgetByUserId(request.UserId);
                return new GetFinancialGoalsResponse(budget.SavingsGoal);
            }
            catch (Exception)
            {
                return new GetFinancialGoalsResponse(0);
            }
        }

        public async Task<IEnumerable<GetRecentTransactionsResponse>> GetRecentTransactions(GetRecentTransactionsQuery request)
        {
            try
            {
                return await _dashboardManagementUnitOfWork.GetRecentTransactions(request.userId);
            }
            catch (Exception)
            {
                return new List<GetRecentTransactionsResponse>() { };
            }
        }
    }
}
