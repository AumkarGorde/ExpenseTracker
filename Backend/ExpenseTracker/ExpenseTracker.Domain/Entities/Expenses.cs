using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain
{
    public class Expenses
    {
        public Guid ExpenseId { get; set; }
        public string Description { get; set; } =string.Empty;
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; }
        public Categories Category { get; set; }
    }
}
