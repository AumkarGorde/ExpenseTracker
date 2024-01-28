using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetExpensesRequestHandler : IRequestHandler<GetExpensesRequest, GetExpensePaginatedResponse>
    {
        private readonly IExpenseManagementService _expenseManagementService;
        public GetExpensesRequestHandler(IExpenseManagementService expenseManagementService)
        {
            _expenseManagementService = expenseManagementService;
        }
        public async Task<GetExpensePaginatedResponse> Handle(GetExpensesRequest request, CancellationToken cancellationToken)
        {
            return await _expenseManagementService.GetExpense(request);
        }
    }
}
