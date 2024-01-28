using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    internal class CreateExpenseHandler : IRequestHandler<CreateExpenseCommand, CreateExpenseResponse>
    {
        private readonly IExpenseManagementService _expenseManagementService;
        public CreateExpenseHandler(IExpenseManagementService expenseManagementService)
        {
            _expenseManagementService = expenseManagementService;
        }

        public async Task<CreateExpenseResponse> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var response = await _expenseManagementService.CreateExpense(request);
            return new CreateExpenseResponse(response);
        }
    }
}
