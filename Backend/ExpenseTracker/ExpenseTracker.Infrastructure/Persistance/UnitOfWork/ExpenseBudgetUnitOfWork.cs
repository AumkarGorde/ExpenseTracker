using ExpenseTracker.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    public class ExpenseBudgetUnitOfWork : IExpenseBudgetUnitOfWork
    {
        private readonly ExpenseTrackerDBContext _dbContext;
        public IBudgetRepository BudgetRepository { get; }

        public IExpensesRepository ExpensesRepository { get; }

        public ExpenseBudgetUnitOfWork(ExpenseTrackerDBContext dbContext)
        {
            _dbContext = dbContext;
            BudgetRepository = new BudgetRepository(_dbContext);
            ExpensesRepository = new ExpensesRepository(_dbContext);
        }
        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
