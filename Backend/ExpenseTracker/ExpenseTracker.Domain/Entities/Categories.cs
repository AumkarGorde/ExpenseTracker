using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain
{
    public class Categories
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public bool IsDefault { get; set; } // to make some category befault in call
        public List<CategoryUser> CategoryUsers { get; set; } = new List<CategoryUser>();
        public List<Expenses> Expenses { get; set; } = new List<Expenses>();
    }
}
