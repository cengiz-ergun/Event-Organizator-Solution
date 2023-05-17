using EventOrganizator.Application.Repositories.Category;
using EventOrganizator.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Repositories.Category
{
    public class CategoryReadRepository : ReadRepository<EventOrganizator.Domain.Entities.Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(EventOrganizatorDbContext context) : base(context)
        {
        }
    }
}
