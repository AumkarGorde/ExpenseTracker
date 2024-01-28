using ExpenseTracker.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class CreateExpenseCommand :IRequest<CreateExpenseResponse>
    {
        public string Description { get; set; } = string.Empty;
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
