using EventOrganizator.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<Response> CreateCategoryAsync(CreateCategory createCategory);
        Task<Response> GetAllCategoriesAsync(int page, int size);
        Task<Response> GetCategoryByIdAsync(int id);
        Task<Response> DeleteCategoryByIdAsync(int id);
        //Task<(bool, CompletedCategoryDTO)> CompleteCategoryAsync(string id);
    }
}
