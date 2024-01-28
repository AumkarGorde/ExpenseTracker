using ExpenseTracker.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record class GetCategoryReportRequest:IRequest<IEnumerable<GetCategoryReportResponse>>
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public Months Month { get; set; }
    }
}
