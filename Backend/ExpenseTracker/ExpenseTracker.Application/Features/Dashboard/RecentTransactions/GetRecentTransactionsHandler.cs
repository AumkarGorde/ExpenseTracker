using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetRecentTransactionsHandler : IRequestHandler<GetRecentTransactionsQuery, IEnumerable<GetRecentTransactionsResponse>>
    {
        private readonly IDashboardService _dashboardService;
        public GetRecentTransactionsHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IEnumerable<GetRecentTransactionsResponse>> Handle(GetRecentTransactionsQuery request, CancellationToken cancellationToken)
        {
           return await _dashboardService.GetRecentTransactions(request);
        }
    }
}
