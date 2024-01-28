using AutoMapper;
using ExpenseTracker.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class ExpenseManagementService : BaseService, IExpenseManagementService
    {
        private readonly IExpenseBudgetUnitOfWork _expensesBudgetUOW;
        public ExpenseManagementService(ICustomLogger logger, IMapper mapper,
            IExpenseBudgetUnitOfWork expensesBudgetUOW) : base(logger, mapper)
        {
            _expensesBudgetUOW = expensesBudgetUOW;
        }

        private async Task UpdateBudgetBalanceOnCreate(Expenses expenses)
        {
            var budget = await _expensesBudgetUOW.BudgetRepository.GetBudgetByUserId(expenses.UserId);
                if (expenses.ExpenseType == ExpenseType.Expenditure)
                    budget.Balance -= expenses.Amount;
                else
                    budget.Balance += expenses.Amount;
            _expensesBudgetUOW.BudgetRepository.Update(budget);
            await _expensesBudgetUOW.SaveChanges();
        }

        public async Task<bool> CreateExpense(CreateExpenseCommand request)
        {
            try
            {
                var expenses = new Expenses()
                {
                    Description = request.Description,
                    Amount = request.Amount,
                    Date = request.Date.Add(DateTime.Now.TimeOfDay),
                    ExpenseType = request.ExpenseType,
                    CategoryId = request.CategoryId,
                    UserId = request.UserId,
                };
                await _expensesBudgetUOW.ExpensesRepository.AddAsync(expenses);
                await _expensesBudgetUOW.SaveChanges();
                await UpdateBudgetBalanceOnCreate(expenses);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<DeleteExpenseCommandResponse> DeleteExpense(DeleteExpenseCommand request)
        {
            try
            {
                var expense = await _expensesBudgetUOW.ExpensesRepository.GetByIdAsync(request.ExpenseId);
                long exisitingAmt = expense.Amount;
                if (expense is not null)
                {
                    _expensesBudgetUOW.ExpensesRepository.Delete(expense);
                    await _expensesBudgetUOW.SaveChanges();
                    await UpdateBudgetOnUpdate(expense, exisitingAmt, 0);
                    return new DeleteExpenseCommandResponse(true);
                }
                return new DeleteExpenseCommandResponse(false);
            }
            catch (Exception)
            {
                return new DeleteExpenseCommandResponse(false);
            }
        }

        public async Task<GetExpensePaginatedResponse> GetExpense(GetExpensesRequest request)
        {
            try
            {
                return  await _expensesBudgetUOW.ExpensesRepository.GetExpenseByUser(request.UserId, request.Page, request.PageSize);
            }
            catch (Exception ex)
            {
                return new GetExpensePaginatedResponse()
                {
                    Page = 1,
                    PageSize = 1,
                    TotalItems = 0,
                    Expenses = new List<GetExpensesResponse>() { }
                };
            }
        }

        public async Task<UpdateExpenseCommandResponse> UpdateExpense(UpdateExpenseCommand request)
        {
            try
            {
                request.Date = request.Date.Add(DateTime.Now.TimeOfDay);
                var expenses = await _expensesBudgetUOW.ExpensesRepository.GetByIdAsync(request.ExpenseId);
                long exisitingAmt = expenses.Amount;
                if(expenses is  null)
                    return new UpdateExpenseCommandResponse(false);
                _mapper.Map(request, expenses);
                _expensesBudgetUOW.ExpensesRepository.Update(expenses);
                await _expensesBudgetUOW.SaveChanges();
                await UpdateBudgetOnUpdate(expenses, exisitingAmt, expenses.Amount);
                return new UpdateExpenseCommandResponse(true);
            }
            catch (Exception)
            {
                return new UpdateExpenseCommandResponse(false);
            }
        }

        private async Task UpdateBudgetOnUpdate(Expenses expenses, long oldValue, long newValue)
        {
            var budget = await _expensesBudgetUOW.BudgetRepository.GetBudgetByUserId(expenses.UserId);
            if(expenses.ExpenseType == ExpenseType.Income)
            {
                long initialAmount = budget.Balance - oldValue;
                budget.Balance = initialAmount + newValue;
            }
            else
            {
                long initialAmount = budget.Balance + oldValue;
                budget.Balance = initialAmount - newValue;
            }
            _expensesBudgetUOW.BudgetRepository.Update(budget);
            await _expensesBudgetUOW.SaveChanges();
        }
    }
}
