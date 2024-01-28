using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain
{
    public class CategoryUser
    {
        public Guid CategoryId { get; set; }
        public Categories Category { get; set; }

        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}
