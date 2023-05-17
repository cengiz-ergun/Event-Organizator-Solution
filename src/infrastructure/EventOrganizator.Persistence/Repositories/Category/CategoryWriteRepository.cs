using EventOrganizator.Application.Repositories.Category;
using EventOrganizator.Domain.Entities;
using EventOrganizator.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Repositories.Category
{
    public class CategoryWriteRepository : WriteRepository<EventOrganizator.Domain.Entities.Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(EventOrganizatorDbContext context) : base(context)
        {
        }
    }
}
