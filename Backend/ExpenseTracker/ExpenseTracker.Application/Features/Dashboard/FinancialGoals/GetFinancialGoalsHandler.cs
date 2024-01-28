using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetFinancialGoalsHandler : IRequestHandler<GetFinancialGoalsQuery, GetFinancialGoalsResponse>
    {
        private readonly IDashboardService _dashboardService;
        public GetFinancialGoalsHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<GetFinancialGoalsResponse> Handle(GetFinancialGoalsQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardService.GetFinancialGoals(request);
        }
    }
}
