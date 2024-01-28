using Asp.Versioning;
using ExpenseTracker.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExpenseTrackerBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly ICustomLogger _logger;
        public ExpenseTrackerBaseController(IMediator mediator,ICustomLogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}
