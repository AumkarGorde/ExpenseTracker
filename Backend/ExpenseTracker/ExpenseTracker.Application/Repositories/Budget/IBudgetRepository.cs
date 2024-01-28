using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Task<Budget> GetBudgetByUserId(Guid userId);
        Task<int> SaveChangesAsync();
    }
}
