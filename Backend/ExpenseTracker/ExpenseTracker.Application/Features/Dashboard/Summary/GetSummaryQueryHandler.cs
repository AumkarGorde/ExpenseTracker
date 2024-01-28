using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetSummaryQueryHandler : IRequestHandler<GetSummaryQuery, GetSummaryResponse>
    {
        private readonly IDashboardService _dashboardService;
        public GetSummaryQueryHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<GetSummaryResponse> Handle(GetSummaryQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardService.GetExpenseSummary(request);
        }
    }
}
