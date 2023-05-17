using AutoMapper;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.DTOs.AppUser;
using EventOrganizator.Application.DTOs.Category;
using EventOrganizator.Application.Features.Commands.AppUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Features.Commands.Category
{
    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommandRequest, CategoryCreateCommandResponse>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryCreateCommandHandler(ICategoryService categoryService,
                                            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<CategoryCreateCommandResponse> Handle(CategoryCreateCommandRequest request, CancellationToken cancellationToken)
        {
            CreateCategory createCategory = _mapper.Map<CreateCategory>(request);
            Response response = await _categoryService.CreateCategoryAsync(createCategory);
            return _mapper.Map<CategoryCreateCommandResponse>(response);
        }
    }
}
