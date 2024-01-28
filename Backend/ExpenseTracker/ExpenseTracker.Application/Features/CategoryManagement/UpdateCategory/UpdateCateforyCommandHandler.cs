using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    internal class UpdateCateforyCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryManagementService _categoryManagementService;
        public UpdateCateforyCommandHandler(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }
        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryManagementService.UpdateCategory(request);
        }
    }
}
