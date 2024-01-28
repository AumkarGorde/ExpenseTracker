using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetUserDetailsResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid BudgetId { get; set; }
        public long BudgetLimit { get; set; }
        public long SavingsGoal { get; set; }
        public long Balance { get; set; }
    }
}
