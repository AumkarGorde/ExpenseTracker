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
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        public BudgetRepository(ExpenseTrackerDBContext dBContext) : base(dBContext)
        {
        }

        public async Task<Budget> GetBudgetByUserId(Guid userId)
        {
            return await _entities.Where(b => b.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }
    }
}
