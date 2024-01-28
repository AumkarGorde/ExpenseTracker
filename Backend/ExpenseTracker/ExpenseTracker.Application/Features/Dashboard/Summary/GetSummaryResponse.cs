using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetSummaryResponse
    {
        public long TotalExpense { get; set; }
        public long RemainingBudget { get; set;}
        public long TotalIncome { get; set; }
        public long Balance { get; set; }
    }
}
