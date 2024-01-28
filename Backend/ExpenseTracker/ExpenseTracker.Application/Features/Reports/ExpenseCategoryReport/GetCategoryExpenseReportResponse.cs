using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetCategoryExpenseReportResponse
    {
        public string CategoryName { get; set; }
        public long ExpenseAmount { get; set; }
    }
}
