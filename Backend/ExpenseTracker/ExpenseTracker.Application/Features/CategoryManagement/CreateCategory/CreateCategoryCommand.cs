using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record CreateCategoryCommand : IRequest<CreateCategoryResponse>
    {
        public string CategoryName { get; init; }
        public string CategoryDescription { get; init; }
        public Guid UserId { get; init; }
        public bool IsDefault { get; init; }
    }
}
