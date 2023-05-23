using EventOrganizator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Repositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
    }
}
