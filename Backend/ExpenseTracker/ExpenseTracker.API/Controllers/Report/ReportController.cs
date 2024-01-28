using ExpenseTracker.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    public class ReportController: ExpenseTrackerBaseController
    {
        public ReportController(IMediator mediator, ICustomLogger logger) : base(mediator, logger)
        {
            
        }

        [HttpGet("get-weekly-report")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetWeeklyReport([FromQuery] GetWeeklyReportRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(request, cancellationToken);
                if(result.Any())
                    return Ok(new ExpenseTrackerReturn<IEnumerable<GetWeeklyReportResponse>>()
                    {
                         Data = result,
                         Message = "Weekly Report Loaded",
                         Success = true
                    });
                else 
                    return Ok(new ExpenseTrackerReturn<IEnumerable<GetWeeklyReportResponse>>()
                    {
                        Data = result,
                        Message = "Error Loading Report",
                        Success = false
                    });
            }
            catch (Exception)
            {
                return StatusCode(500, new ExpenseTrackerReturn<IEnumerable<GetWeeklyReportResponse>>()
                {
                    Data = new List<GetWeeklyReportResponse>() { },
                    Message = "Error Loading Report",
                    Success = false
                });
            }
        }

        [HttpGet("get-category-expense-report")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetCategoryExpenseReport([FromQuery] GetCategoryExpenseReportRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(request, cancellationToken);
                if (result.Any())
                    return Ok(new ExpenseTrackerReturn<IEnumerable<GetCategoryExpenseReportResponse>>()
                    {
                        Data = result,
                        Message = "Category Expense Report Loaded",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<IEnumerable<GetCategoryExpenseReportResponse>>()
                    {
                        Data = result,
                        Message = "Error Loading Category Expense Report Report",
                        Success = false
                    });
            }
            catch (Exception)
            {
                return StatusCode(500, new ExpenseTrackerReturn<IEnumerable<GetCategoryExpenseReportResponse>>()
                {
                    Data = new List<GetCategoryExpenseReportResponse>() { },
                    Message = "Error Loading Category Expense Report Report",
                    Success = false
                });
            }
        }

        [HttpGet("get-category-report")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetCategoryReport([FromQuery] GetCategoryReportRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(request, cancellationToken);
                if (result.Any())
                    return Ok(new ExpenseTrackerReturn<IEnumerable<GetCategoryReportResponse>>()
                    {
                        Data = result,
                        Message = "Category Expense Report Loaded",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<IEnumerable<GetCategoryReportResponse>>()
                    {
                        Data = result,
                        Message = "Error Loading Category Expense Report Report",
                        Success = false
                    });
            }
            catch (Exception)
            {
                return StatusCode(500, new ExpenseTrackerReturn<IEnumerable<GetCategoryReportResponse>>()
                {
                    Data = new List<GetCategoryReportResponse>() { },
                    Message = "Error Loading Category Expense Report Report",
                    Success = false
                });
            }
        }
    }
}
