using EventOrganizator.API.Helpers;
using EventOrganizator.Application.Constants;
using EventOrganizator.Application.Features.Commands.AppUser;
using EventOrganizator.Application.Features.Commands.Category;
using EventOrganizator.Application.Features.Queries.Category;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoryController : ControllerBase
    {
        readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Administrator,Member")]
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] GetAllCategoriesQueryRequest getAllCategoriesQueryRequest)
        {
            GetAllCategoriesQueryResponse allCategoriesQueryResponse = await _mediator.Send(getAllCategoriesQueryRequest);
            return CustomHttpResponse.Result(allCategoriesQueryResponse);
        }

        [Authorize(Roles = "Administrator,Member")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] GetCategoryByIdQueryRequest getCategoryByIdQueryRequest)
        {
            GetCategoryByIdQueryResponse categoryByIdQueryResponse = await _mediator.Send(getCategoryByIdQueryRequest);
            return CustomHttpResponse.Result(categoryByIdQueryResponse);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateCommandRequest categoryCreateCommandRequest)
        {
            CategoryCreateCommandResponse signupUserCommandResponse = await _mediator.Send(categoryCreateCommandRequest);
            return CustomHttpResponse.Result(signupUserCommandResponse);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategoryCommandRequest deleteCategoryCommandRequest)
        {
            DeleteCategoryCommandResponse deleteCategoryCommandResponse = await _mediator.Send(deleteCategoryCommandRequest);
            return CustomHttpResponse.Result(deleteCategoryCommandResponse);
        }
    }
}
