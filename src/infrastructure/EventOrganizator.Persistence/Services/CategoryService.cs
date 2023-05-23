using AutoMapper;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.DTOs.AppUser;
using EventOrganizator.Application.DTOs.Category;
using EventOrganizator.Application.DTOs.City;
using EventOrganizator.Application.Repositories.Category;
using EventOrganizator.Domain.Entities;
using EventOrganizator.Persistence.Repositories.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryReadRepository categoryReadRepository,
                               ICategoryWriteRepository categoryWriteRepository,
                               IMapper mapper)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _mapper = mapper;
        }
        public async Task<Response> CreateCategoryAsync(CreateCategory createCategory)
        {
            var query = _categoryReadRepository.GetWhere(C => C.Name.ToLower() == createCategory.Name.ToLower());
            Response response = new Response();
            if (query.Any() && query.FirstOrDefault().IsDeleted == false)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"There is already a category named {createCategory.Name}.");
            }
            else if(query.Any() && query.FirstOrDefault().IsDeleted == true)
            {
                Category category = query.FirstOrDefault();
                category.IsDeleted = false;
                category.Name = createCategory.Name;
                _categoryWriteRepository.Update(category);
                response.HttpStatusCode = System.Net.HttpStatusCode.Created;
                response.Data.Add(_mapper.Map<SingleCategory>(category));
            }
            else
            {
                Category category = _mapper.Map<Category>(createCategory);

                bool result = await _categoryWriteRepository.AddAsync(category);
                
                if (result)
                {
                    await _categoryWriteRepository.SaveAsync();
                    response.HttpStatusCode = System.Net.HttpStatusCode.Created;
                    response.Data.Add(_mapper.Map<SingleCategory>(category));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            return response;
        }

        public async Task<Response> DeleteCategoryByIdAsync(int id)
        {
            Category category = await _categoryReadRepository.GetByIdAsync(id);
            Response response = new Response();
            if (category == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There is not a category with id {id}");
            }
            else
            {
                var result = category.Events.Select(e => e.EventStatus != Domain.Enum.EventStatus.Canceled || e.EventStatus != Domain.Enum.EventStatus.Ended);
                if (result.Count() > 0)
                {
                    response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    response.Errors.Add($"Sorry. This category has one or more event which is at Pending or Active status.");
                }
                else
                {
                    await _categoryWriteRepository.RemoveAsync(id);
                    await _categoryWriteRepository.SaveAsync();
                    response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
                }
            }
            return response;
        }

        public async Task<Response> GetAllCategoriesAsync(int page, int size)
        {
            var query = _categoryReadRepository.Table;

            var data = query.Skip(page * size).Take(size);
            /*.Take((page * size)..size);*/

            return new Response()
            {
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                Data = await data.Select(c => _mapper.Map<SingleCategory>(c)).ToListAsync<object>()
            };
        }

        public async Task<Response> GetCategoryByIdAsync(int id)
        {
            Category category = await _categoryReadRepository.GetByIdAsync(id);
            Response response = new Response();
            if (category == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There is not a category with id {id}");
            }
            else
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                response.Data.Add(_mapper.Map<SingleCategory>(category));
            }
            return response;
        }
    }
}
