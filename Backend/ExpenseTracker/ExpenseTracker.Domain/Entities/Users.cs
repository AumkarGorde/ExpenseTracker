using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain
{
    public class Users
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<CategoryUser> CategoryUsers { get; set; } = new List<CategoryUser>();
        public List<Expenses> Expenses { get; set; } = new List<Expenses>();
        public Budget Budget { get; set; }
    }
}

