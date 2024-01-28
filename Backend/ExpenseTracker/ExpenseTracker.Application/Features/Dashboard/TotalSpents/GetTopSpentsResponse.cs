using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public record class GetTopSpentsResponse 
    {
        public string CategoryName { get; set; }
        public long Amount { get; set; }
    };
}
