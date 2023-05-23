using EventOrganizator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Repositories
{
    public interface ICityRepository : IGenericRepository<City>
    {
        //Task<Category> UpdateUser(Category category);
        //Task<City> GetByName(string name, Expression<Func<City, bool>> filter = null);
        Task<City> Get(Expression<Func<City, bool>> filter = null);
    }
}
