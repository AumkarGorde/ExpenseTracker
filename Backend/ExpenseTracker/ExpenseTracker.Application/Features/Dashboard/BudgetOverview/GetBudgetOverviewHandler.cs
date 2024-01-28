using MediatR;

namespace ExpenseTracker.Application
{
    public class GetBudgetOverviewHandler : IRequestHandler<GetBudgetOverviewQuery, GetBudgetOverviewResponse>
    {
        private readonly IDashboardService _dashboardService;
        public GetBudgetOverviewHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<GetBudgetOverviewResponse> Handle(GetBudgetOverviewQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardService.GetBudgetOverview(request);
        }
    }
}
