using EventOrganizator.Application.Repositories;
using EventOrganizator.Domain.Entities;
using EventOrganizator.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly EventOrganizatorDbContext _eventOrganizatorDbContext;
        public EventRepository(EventOrganizatorDbContext eventOrganizatorDbContext) : base(eventOrganizatorDbContext)
        {
            _eventOrganizatorDbContext = eventOrganizatorDbContext;
        }
    }
}
