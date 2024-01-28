using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed record GetCategoriesResponse
    {
        public Guid CategoryId { get; init; }
        public string CategoryName { get; init; }
        public string CategoryDescription { get; init; }
        public bool IsDefault { get; init; }

    }

    public sealed record GetCategoriesPaginatedResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<GetCategoriesResponse> Categories { get; set; }
    }
}
