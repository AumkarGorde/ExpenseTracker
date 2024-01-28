using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetExpensesResponse
    {
        public Guid ExpenseId { get; set; }
        public string Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public sealed record GetExpensePaginatedResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<GetExpensesResponse> Expenses { get; set; }
    }
}
