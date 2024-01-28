using ExpenseTracker.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    public class DashboardController : ExpenseTrackerBaseController
    {
        public DashboardController(IMediator mediator, ICustomLogger logger) : base(mediator, logger)
        {

        }

        // Summary
        // budget Overview
        // Where have you spent in this month, category wise distribution
        // Saving Goals 
        // Recent 10 transactions
        [Authorize(Roles = "USER")]
        [HttpGet("dashboard-summary")]
        public async Task<ActionResult> GetSummary([FromQuery] GetSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(request, cancellationToken);
                if (response.Balance >= 0)
                    return Ok(new ExpenseTrackerReturn<GetSummaryResponse>()
                    {
                        Data = response,
                        Message = "Summary calculated successfully",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<GetSummaryResponse>()
                    {
                        Data = response,
                        Message = "Failed to Calculate Summary",
                        Success = false
                    });
            }
            catch (Exception)
            {
                GetSummaryResponse response = new GetSummaryResponse()
                {
                    RemainingBudget = 0,
                    TotalExpense = 0
                };
                return Ok(new ExpenseTrackerReturn<GetSummaryResponse>()
                {
                    Data = response,
                    Message = "Failed to Calculate Summary",
                    Success = false
                });
            }
        }
        [Authorize(Roles = "USER")]
        [HttpPost("dashboard-budget")]
        public async Task<ActionResult> ConfigureBudget(CreateBudgetLimitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(request, cancellationToken);
                if (response.IsBudgetCreated)
                    return StatusCode(201, new ExpenseTrackerReturn<CreateBudgetLimitResponse>()
                    {
                        Data = response,
                        Message = "Budget Limit created",
                        Success = true,
                    });
                else
                    return StatusCode(400, new ExpenseTrackerReturn<CreateBudgetLimitResponse>()
                    {
                        Data = response,
                        Message = "Failed to create Budget Limit",
                        Success = false,
                    });
            }
            catch (Exception)
            {
                CreateBudgetLimitResponse response = new CreateBudgetLimitResponse(false);
                return StatusCode(400, new ExpenseTrackerReturn<CreateBudgetLimitResponse>()
                {
                    Data = response,
                    Message = "Failed to create Budget Limit",
                    Success = false,
                });
            }
        }
        [Authorize(Roles = "USER")]
        [HttpGet("dashboard-budget-overview")]
        public async Task<ActionResult> GetBudgetOverview([FromQuery] GetBudgetOverviewQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(query, cancellationToken);
                if (response.TotalSpent != 0 && response.BudegetLimit != 0)
                    return Ok(new ExpenseTrackerReturn<GetBudgetOverviewResponse>()
                    {
                        Data = response,
                        Message = "Budget overview calculated successfully",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<GetBudgetOverviewResponse>()
                    {
                        Data = response,
                        Message = "Budget overview issue",
                        Success = true
                    });
            }
            catch (Exception)
            {
                GetBudgetOverviewResponse response = new GetBudgetOverviewResponse(0, 0);
                return StatusCode(400, new ExpenseTrackerReturn<GetBudgetOverviewResponse>()
                {
                    Data = response,
                    Message = "Failed to create Budget overview",
                    Success = false,
                });
            }
        }
        [Authorize(Roles = "USER")]
        [HttpGet("dashboard-top-spents")]
        public async Task<ActionResult> GetTopSpents([FromQuery] GetTopSpentsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(query, cancellationToken);
                if (response.Count > 0)
                    return Ok(new ExpenseTrackerReturn<List<GetTopSpentsResponse>>()
                    {
                        Data = response,
                        Message = "Top Spents Retrived",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<List<GetTopSpentsResponse>>()
                    {
                        Data = response,
                        Message = "Top Spents Retival Failed",
                        Success = false
                    });
            }
            catch (Exception)
            {
                var errorResponse = new List<GetTopSpentsResponse>() { };
                return Ok(new ExpenseTrackerReturn<List<GetTopSpentsResponse>>()
                {
                    Data = errorResponse,
                    Message = "Top Spents Retival Failed",
                    Success = false
                });
            }
        }

        [Authorize(Roles = "USER")]
        [HttpGet("dashboard-financial-goals")]
        public async Task<ActionResult> GetFinancialGoals([FromQuery] GetFinancialGoalsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(query, cancellationToken);
                if (response.SavingsGoal is not 0)
                    return Ok(new ExpenseTrackerReturn<GetFinancialGoalsResponse>()
                    {
                        Data = response,
                        Message = "Financial Goals Retrived",
                        Success = true
                    });
                else 
                    return Ok(new ExpenseTrackerReturn<GetFinancialGoalsResponse>()
                    {
                        Data = response,
                        Message = "Financial Goals Retrival Failed",
                        Success = false
                    });


            }
            catch (Exception)
            {
                var err = new GetFinancialGoalsResponse(0);
                return Ok(new ExpenseTrackerReturn<GetFinancialGoalsResponse>()
                {
                    Data = err,
                    Message = "Financial Goals Retrival Failed",
                    Success = false
                });
            }
        }

        [Authorize(Roles = "USER")]
        [HttpGet("dashboard-recent-transactions")]
        public async Task<ActionResult> GetRecentTransactions([FromQuery] GetRecentTransactionsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(query, cancellationToken);
                if (response.Count() > 0)
                    return Ok(new ExpenseTrackerReturn<IEnumerable<GetRecentTransactionsResponse>>
                    {
                        Data = response,
                        Message = "Transactions Received",
                        Success = true
                    });
                else
                    return Ok(new ExpenseTrackerReturn<IEnumerable<GetRecentTransactionsResponse>>
                    {
                        Data = response,
                        Message = "Transactions Receive Failure",
                        Success = false
                    });
            }
            catch (Exception)
            {
                var err = new List<GetRecentTransactionsResponse>();
                return Ok(new ExpenseTrackerReturn<IEnumerable<GetRecentTransactionsResponse>>
                {
                    Data = err,
                    Message = "Transactions Receive Failure",
                    Success = false
                });
            }
        }
    }
}
