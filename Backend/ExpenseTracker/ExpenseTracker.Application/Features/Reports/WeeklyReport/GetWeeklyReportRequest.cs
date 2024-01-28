using ExpenseTracker.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetWeeklyReportRequest:IRequest<IEnumerable<GetWeeklyReportResponse>>
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public Months Month { get; set; }
    }
}
