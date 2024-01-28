using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetWeeklyReportHandler : IRequestHandler<GetWeeklyReportRequest, IEnumerable<GetWeeklyReportResponse>>
    {
        private readonly IReportService _reportService;
        public GetWeeklyReportHandler(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IEnumerable<GetWeeklyReportResponse>> Handle(GetWeeklyReportRequest request, CancellationToken cancellationToken)
        {
            return await _reportService.GetWeeklyReport(request);
        }
    }
}
