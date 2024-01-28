using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetTopSpentsHandler : IRequestHandler<GetTopSpentsQuery, List<GetTopSpentsResponse>>
    {
        private readonly IDashboardService _dashboardService;
        public GetTopSpentsHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<List<GetTopSpentsResponse>> Handle(GetTopSpentsQuery request, CancellationToken cancellationToken)
        {
            return await _dashboardService.GetTotalSpents(request);
        }
    }
}
