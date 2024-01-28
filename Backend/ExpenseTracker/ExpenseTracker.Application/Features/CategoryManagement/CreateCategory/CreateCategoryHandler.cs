using ExpenseTracker.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        private readonly ICategoryManagementService _categoryManagementService;
        public CreateCategoryHandler(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }
        public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _categoryManagementService.CreateCategory(request);
            if (response)
                return new CreateCategoryResponse(response);
            else
                return new CreateCategoryResponse(false);
        }
    }
}
