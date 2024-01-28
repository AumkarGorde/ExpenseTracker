using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetCategoryReportHandler : IRequestHandler<GetCategoryReportRequest, IEnumerable<GetCategoryReportResponse>>
    {
        private readonly IReportService _reportService;
        public GetCategoryReportHandler(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IEnumerable<GetCategoryReportResponse>> Handle(GetCategoryReportRequest request, CancellationToken cancellationToken)
        {
            return await _reportService.GetCategoryReport(request);
        }
    }
}
