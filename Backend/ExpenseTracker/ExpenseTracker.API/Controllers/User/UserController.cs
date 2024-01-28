using ExpenseTracker.Application;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ExpenseTracker.API.Controllers.User
{
    public class UserController : ExpenseTrackerBaseController
    {
        private const string CLASS_NAME = "UserController";
        private readonly IValidator<RegisterUserCommand> _validator;
        public UserController(IMediator mediator, ICustomLogger logger, IValidator<RegisterUserCommand> validator) : base(mediator, logger)
        {
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool response = false;
            _logger.LogInfo(CLASS_NAME, "Register", "Registration Endpoint start");
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var a = validationResult.Errors;
                    _logger.LogInfo(CLASS_NAME, "Register", "Server Side Validation Failed");
                    return BadRequest(new ExpenseTrackerReturn<List<ValidationFailure>>()
                    {
                        Data = validationResult.Errors,
                        Success = false,
                        Message = "Server side Validation Failed"
                    }); ;
                }

                response = await _mediator.Send(request, cancellationToken);
                _logger.LogInfo(CLASS_NAME, "Register", "Registration Endpoint end");
                return Ok(new ExpenseTrackerReturn<bool>()
                {
                    Data = response,
                    Success = true,
                    Message = "Registration SuccessFull"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(CLASS_NAME, "Register", "Registration Endpoint Error", ex);
                return StatusCode(500, new ExpenseTrackerReturn<bool>()
                {
                    Data = response,
                    Success = false,
                    Message = "Registration Unsuccessfull"
                });
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(request, cancellationToken);
                if (String.IsNullOrEmpty(response.Token))
                    return Unauthorized(new ExpenseTrackerReturn<LoginUserCommandResponse>()
                    {
                        Data = response,
                        Success = false,
                        Message = "Login Failed"
                    });
                else
                    return Ok(new ExpenseTrackerReturn<LoginUserCommandResponse>()
                    {
                        Data = response,
                        Success = true,
                        Message = "Login Success"
                    });
            }
            catch (Exception)
            {
                return Unauthorized(new ExpenseTrackerReturn<LoginUserCommandResponse>()
                {
                    Data = new LoginUserCommandResponse()
                    {
                        FirstName = string.Empty,
                        Token = string.Empty
                    ,
                        UserId = string.Empty,
                        UserName = string.Empty
                    },
                    Success = false,
                    Message = "Login Failed"
                });
            }
        }

        [HttpGet("get-user-details")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetUserDetails([FromQuery] GetUserDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(request, cancellationToken);
                if (result.BudgetLimit != 0)
                    return Ok(new ExpenseTrackerReturn<GetUserDetailsResponse>()
                    {
                        Data = result,
                        Message = "User Details Retrived",
                        Success = true,
                    });
                else
                    return Ok(new ExpenseTrackerReturn<GetUserDetailsResponse>()
                    {
                        Data = result,
                        Message = "Failed to Retrive User Details",
                        Success = false,
                    });
            }
            catch (Exception)
            {
                return StatusCode(500, new ExpenseTrackerReturn<GetUserDetailsResponse>()
                {
                    Data = new GetUserDetailsResponse(),
                    Message = "Failed to Retrive User Details",
                    Success = false,
                });
            }
        }

        [HttpPut("update-user-details/{userId}")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> UpdateUserDetails(Guid userId, UpdateUserDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.UserId = userId;
                var result = await _mediator.Send(request, cancellationToken);
                if (result.IsUpdated)
                    return Ok(new ExpenseTrackerReturn<UpdateUserDetailsResponse>()
                    {
                        Data = result,
                        Message = "Updated User Details",
                        Success = true,
                    });
                else
                    return Ok(new ExpenseTrackerReturn<UpdateUserDetailsResponse>()
                    {
                        Data = result,
                        Message = "Update Failed",
                        Success = false,
                    });
            }
            catch (Exception)
            {
                return Ok(new ExpenseTrackerReturn<UpdateUserDetailsResponse>()
                {
                    Data = new UpdateUserDetailsResponse() { IsUpdated = false},
                    Message = "Update Failed",
                    Success = false,
                });
            }
        }
    }
}
