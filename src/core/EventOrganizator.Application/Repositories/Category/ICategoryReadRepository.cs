using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventOrganizator.Domain.Entities;

namespace EventOrganizator.Application.Repositories.Category
{
    public interface ICategoryReadRepository : IReadRepository<EventOrganizator.Domain.Entities.Category>
    {
        Task<EventOrganizator.Domain.Entities.Category> GetByIdAsync(int id, bool tracking = true);
    }
}
