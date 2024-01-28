using ExpenseTracker.Application;
using ExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    public class DashboardManagementUnitOfWork : IDashboardManagementUnitOfWork
    {
        private readonly ExpenseTrackerDBContext _dbContext;
        public IBudgetRepository BudgetRepository { get; }
        public IExpensesRepository ExpensesRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }

        public DashboardManagementUnitOfWork(ExpenseTrackerDBContext dbContext)
        {
            _dbContext = dbContext;
            BudgetRepository = new BudgetRepository(_dbContext);
            ExpensesRepository = new ExpensesRepository(_dbContext);
            CategoriesRepository = new CategoriesRepository(_dbContext);
        }
        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<GetTopSpentsResponse>> GetTotalSpents(DateTime startDate, DateTime endDate, Guid userId)
        {
            return await _dbContext.Expenses
                .Where(e => e.UserId == userId && e.ExpenseType == ExpenseType.Expenditure && e.Date >= startDate && e.Date <= endDate)
                .OrderByDescending(e => e.Amount)
                .Take(2)
                .Select(e => new GetTopSpentsResponse
                {
                    CategoryName = e.Category.CategoryName,
                    Amount = e.Amount,
                }).ToListAsync();
        }

        public async Task<IEnumerable<GetRecentTransactionsResponse>> GetRecentTransactions(Guid usedId)
        {
            return await _dbContext.Expenses
                .Where(e => e.UserId == usedId)
                .OrderByDescending(e => e.Date)
                .Take(10)
                .Select(e => new GetRecentTransactionsResponse
                {
                    Description = e.Description,
                    CategoryName = e.Category.CategoryName,
                    ExpenseType = e.ExpenseType,
                    Amount = e.Amount,
                    Date = e.Date
                }).ToListAsync();
        }
    }
}
