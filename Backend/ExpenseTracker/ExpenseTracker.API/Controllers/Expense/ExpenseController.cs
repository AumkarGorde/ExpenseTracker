using ExpenseTracker.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    public class ExpenseController : ExpenseTrackerBaseController
    {
        public ExpenseController(IMediator mediator, ICustomLogger logger) : base(mediator, logger)
        {

        }
        [HttpPost("create-expense")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> CreateExpense(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(request, cancellationToken);
                if (response.IsExpenseCreated)
                    return Ok(new ExpenseTrackerReturn<CreateExpenseResponse>()
                    {
                        Data = response,
                        Message = "Expense created successfully",
                        Success = true,
                    });
                else
                    return Ok(new ExpenseTrackerReturn<CreateExpenseResponse>()
                    {
                        Data = response,
                        Message = "Expense Creation Failed",
                        Success = true,
                    });
            }
            catch (Exception)
            {
                CreateExpenseResponse responseFail = new CreateExpenseResponse(false);
                return StatusCode(400, new ExpenseTrackerReturn<CreateExpenseResponse>()
                {
                    Data = responseFail,
                    Message = "Expense Creation Failed",
                    Success = false,
                });
            }
        }

        [HttpGet("get-expenses")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetExpenses([FromQuery] GetExpensesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(request, cancellationToken);
                if (response.Expenses.Any())
                    return Ok(new ExpenseTrackerReturn<GetExpensePaginatedResponse>()
                    {
                        Data = response,
                        Message = "Expenses retrived successfully",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<GetExpensePaginatedResponse>()
                    {
                        Data = response,
                        Message = "No Expenses Found",
                        Success = true
                    });
            }
            catch (Exception)
            {
                var responseFail = new GetExpensePaginatedResponse()
                {
                    Page = 1,
                    PageSize = 1,
                    Expenses = new List<GetExpensesResponse>() { }
                };
                return StatusCode(400, new ExpenseTrackerReturn<GetExpensePaginatedResponse>()
                {
                    Data = responseFail,
                    Message = "No Expenses Found",
                    Success = true
                });
            }
        }

        [Authorize(Roles = "USER")]
        [HttpPut("{expenseId}")]
        public async Task<ActionResult> UpdateExpense(Guid expenseId, [FromBody] UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.ExpenseId = expenseId;
                var response = await _mediator.Send(request, cancellationToken);
                if (response.isUpdatedComplete)
                    return Ok(new ExpenseTrackerReturn<UpdateExpenseCommandResponse>()
                    {
                        Data = response,
                        Message = "Updated the Expense",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<UpdateExpenseCommandResponse>()
                    {
                        Data = response,
                        Message = "Update Expense Failed",
                        Success = false
                    });
            }
            catch (Exception)
            {
                var err = new UpdateExpenseCommandResponse(false);
                return Ok(new ExpenseTrackerReturn<UpdateExpenseCommandResponse>()
                {
                    Data = err,
                    Message = "Update Expense Failed",
                    Success = false
                });
            }
        }
        
        [Authorize(Roles = "USER")]
        [HttpDelete("{expenseId}")]
        public async Task<ActionResult> DeleteExpense(Guid expenseId, CancellationToken cancellationToken)
        {
            try
            {
                var request = new DeleteExpenseCommand(expenseId);
                var response = await _mediator.Send(request, cancellationToken);
                if (response.isDeleted)
                    return Ok(new ExpenseTrackerReturn<DeleteExpenseCommandResponse>()
                    {
                        Data = response,
                        Message = "Deleted the Expense",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<DeleteExpenseCommandResponse>()
                    {
                        Data = response,
                        Message = "Delete failed",
                        Success = false
                    });
            }
            catch (Exception)
            {
                return Ok(new ExpenseTrackerReturn<DeleteExpenseCommandResponse>()
                {
                    Data = new DeleteExpenseCommandResponse(false),
                    Message = "Delete failed",
                    Success = false
                });
            }
        }
    }
}
