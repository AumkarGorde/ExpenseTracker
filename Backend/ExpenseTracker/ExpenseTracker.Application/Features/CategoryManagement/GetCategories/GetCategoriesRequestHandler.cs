using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetCategoriesRequestHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesPaginatedResponse>
    {
        private readonly ICategoryManagementService _categoryManagementService;
        public GetCategoriesRequestHandler(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }

        public async Task<GetCategoriesPaginatedResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryManagementService.GetuserSpecificCategories(request);
        }
    }
}
