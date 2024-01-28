using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record CreateBudgetLimitCommand : IRequest<CreateBudgetLimitResponse>
    {
        public long BudgetLimit { get; set; }
        public Guid UserId { get; set; }
    }
}
