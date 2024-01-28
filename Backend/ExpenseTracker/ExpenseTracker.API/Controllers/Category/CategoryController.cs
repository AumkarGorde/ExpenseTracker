using ExpenseTracker.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    public class CategoryController : ExpenseTrackerBaseController
    {
        private const string CLASS_NAME = "CategoryController";
        public CategoryController(IMediator mediator, ICustomLogger logger) : base(mediator, logger)
        {

        }

        [HttpPost("create-category")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> CreateCategory(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo(CLASS_NAME, "CreateCategory", "CreateCategory Endpoint start");
            try
            {
                CreateCategoryResponse response = await _mediator.Send(request, cancellationToken);
                if(response.IsCrategoryCreated)
                    return StatusCode(201, new ExpenseTrackerReturn<CreateCategoryResponse>()
                    {
                        Data = response,
                        Message = "Category created",
                        Success = true,
                    });
                else
                    return StatusCode(400, new ExpenseTrackerReturn<CreateCategoryResponse>()
                    {
                        Data = response,
                        Message = "Failed to create category",
                        Success = false,
                    });
            }
            catch (Exception ex)
            {
                CreateCategoryResponse responseFail = new CreateCategoryResponse(false);
                _logger.LogError(CLASS_NAME, "Register", "CreateCategory Endpoint Error", ex);
                return StatusCode(400, new ExpenseTrackerReturn<CreateCategoryResponse>()
                {
                    Data = responseFail,
                    Message = "Failed to create category",
                    Success = false,
                });
            }
        }

        [HttpGet("get-categories")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetCategories([FromQuery] GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo(CLASS_NAME, "GetCategories", "GetCategories Endpoint start");
            try
            {
                var response = await _mediator.Send(request, cancellationToken);
                if(response.Categories.Any())
                    return Ok(new ExpenseTrackerReturn<GetCategoriesPaginatedResponse>()
                    {
                        Data = response,
                        Message = "Data retrived successfully",
                        Success = true,
                    });
                else
                    return Ok(new ExpenseTrackerReturn<GetCategoriesPaginatedResponse>()
                    {
                        Data = response,
                        Message = "No Data found",
                        Success = true,
                    });
            }
            catch (Exception ex)
            {
                var responseFail = new GetCategoriesPaginatedResponse() 
                {
                    TotalItems = 0,
                    Page = 1,
                    PageSize = 1,
                    Categories = new List<GetCategoriesResponse>() { }
                };
                _logger.LogError(CLASS_NAME, "Register", "CreateCategory Endpoint Error", ex);
                return StatusCode(400, new ExpenseTrackerReturn<GetCategoriesPaginatedResponse>()
                {
                    Data = responseFail,
                    Message = "Failed to retrive categories",
                    Success = false,
                });
            }
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.CategoryId = categoryId;
                var response = await _mediator.Send(request);
                if (response)
                    return Ok(new ExpenseTrackerReturn<bool>()
                    {
                        Data = response,
                        Message = "Updated category successfully",
                        Success = true,
                    });
                else return Ok(new ExpenseTrackerReturn<bool>()
                {
                    Data = response,
                    Message = "Update Failed",
                    Success = false,
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ExpenseTrackerReturn<bool>()
                {
                    Data = false,
                    Message = "Update Failed",
                    Success = false,
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            try
            {
                DeleteCategoryCommand delete = new DeleteCategoryCommand(id);
                var response = await _mediator.Send(delete);
                if (response)
                    return Ok(new ExpenseTrackerReturn<bool>()
                    {
                        Data = response,
                        Message = "Deleted category successfully",
                        Success = true,
                    });
                else return Ok(new ExpenseTrackerReturn<bool>()
                {
                    Data = response,
                    Message = "Delete Failed",
                    Success = false,
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ExpenseTrackerReturn<bool>()
                {
                    Data = false,
                    Message = "Delete Failed",
                    Success = false,
                });
            }
        }
    }
}
