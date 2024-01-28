using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetRecentTransactionsResponse
    {
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
