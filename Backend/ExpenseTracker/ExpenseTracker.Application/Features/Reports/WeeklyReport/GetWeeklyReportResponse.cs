using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetWeeklyReportResponse
    {
        public int Id { get; set; }
        public string WeekNumber { get; set; }
        public long ExpensePerWeek { get; set; }
        public long IncomePerWeek { get; set; }
    }
}
