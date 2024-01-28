using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain
{
    public class Budget
    {
        public Guid BudgetId { get; set; }
        public long BudgetLimit { get; set; }
        public Guid UserId { get; set; }
        public long SavingsGoal { get;set; }
        public long Balance { get; set; }
        public Users User { get; set; }
    }
}
