using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class UpdateExpenseHandler : IRequestHandler<UpdateExpenseCommand, UpdateExpenseCommandResponse>
    {
        private readonly IExpenseManagementService _expenseManagementService;
        public UpdateExpenseHandler(IExpenseManagementService expenseManagementService)
        {
            _expenseManagementService = expenseManagementService; 
        }
        public async Task<UpdateExpenseCommandResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            return await _expenseManagementService.UpdateExpense(request);
        }
    }
}
