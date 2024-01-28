using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetCategoryExpenseReportHandler : IRequestHandler<GetCategoryExpenseReportRequest
        , IEnumerable<GetCategoryExpenseReportResponse>>
    {
        private readonly IReportService _reportService;
        public GetCategoryExpenseReportHandler(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IEnumerable<GetCategoryExpenseReportResponse>> Handle(GetCategoryExpenseReportRequest request, CancellationToken cancellationToken)
        {
            return await _reportService.GetCategoryExpense(request);
        }
    }
}
