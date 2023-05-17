using AutoMapper;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Features.Queries.Category
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public DeleteCategoryCommandHandler(ICategoryService categoryService,
                                           IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            Response response = await _categoryService.DeleteCategoryByIdAsync(int.Parse(request.Id));
            return _mapper.Map<DeleteCategoryCommandResponse>(response);
        }
    }
}
