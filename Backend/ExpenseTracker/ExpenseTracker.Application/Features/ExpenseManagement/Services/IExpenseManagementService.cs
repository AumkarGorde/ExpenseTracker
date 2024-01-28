using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface IExpenseManagementService
    {
        Task<bool> CreateExpense(CreateExpenseCommand request);
        Task<GetExpensePaginatedResponse> GetExpense(GetExpensesRequest request);
        Task<UpdateExpenseCommandResponse> UpdateExpense(UpdateExpenseCommand request);
        Task<DeleteExpenseCommandResponse> DeleteExpense(DeleteExpenseCommand request);
    }
}
