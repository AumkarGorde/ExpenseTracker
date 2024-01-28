using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, DeleteExpenseCommandResponse>
    {
        private readonly IExpenseManagementService _expenseManagementService;
        public DeleteExpenseCommandHandler(IExpenseManagementService expenseManagementService)
        {
            _expenseManagementService = expenseManagementService;
        }
        public async Task<DeleteExpenseCommandResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            return await _expenseManagementService.DeleteExpense(request);
        }
    }
}
