using EventOrganizator.Application.Repositories.Category;
using EventOrganizator.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
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
        public async Task<EventOrganizator.Domain.Entities.Category> GetByIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.Include("Events").FirstOrDefaultAsync(data => data.Id == id);
        }
    }
}
