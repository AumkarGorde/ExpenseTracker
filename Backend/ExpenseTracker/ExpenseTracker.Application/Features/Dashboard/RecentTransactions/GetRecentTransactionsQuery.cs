using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetRecentTransactionsQuery(Guid userId):IRequest<IEnumerable<GetRecentTransactionsResponse>>;
}
