using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        ICategoryManagementService _categoryManagementService;
        public DeleteCategoryCommandHandler(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }
        public Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return _categoryManagementService.DeleteCategory(request);
        }
    }
}
