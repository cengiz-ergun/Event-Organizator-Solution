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
    public class GetAllCategoriesQueryHandler: IRequestHandler<GetAllCategoriesQueryRequest, GetAllCategoriesQueryResponse>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<GetAllCategoriesQueryResponse> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            Response response = await _categoryService.GetAllCategoriesAsync(request.Page, request.Size);
            return _mapper.Map<GetAllCategoriesQueryResponse>(response);
        }
    }
}
