using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetUserDetailsRequest:IRequest<GetUserDetailsResponse>
    {
        public Guid UserId { get; set; }
    }
}
