using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed class CreateBudgetLimitHandler : IRequestHandler<CreateBudgetLimitCommand, CreateBudgetLimitResponse>
    {
        private readonly IDashboardService _dashboardService;
        public CreateBudgetLimitHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<CreateBudgetLimitResponse> Handle(CreateBudgetLimitCommand request, CancellationToken cancellationToken)
        {
            var response = await _dashboardService.CreateBudgetLimit(request);
            return new CreateBudgetLimitResponse(response);
        }
    }
}
