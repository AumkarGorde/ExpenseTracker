using ExpenseTracker.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record UpdateExpenseCommand:IRequest<UpdateExpenseCommandResponse>
    {
        public long Amount { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public Guid ExpenseId { get; set; }
    }
}
