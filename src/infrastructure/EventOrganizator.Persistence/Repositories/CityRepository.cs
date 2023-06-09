﻿using EventOrganizator.Application.Repositories;
using EventOrganizator.Domain.Entities;
using EventOrganizator.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly EventOrganizatorDbContext _eventOrganizatorDbContext;
        public CityRepository(EventOrganizatorDbContext eventOrganizatorDbContext) : base(eventOrganizatorDbContext)
        {
            _eventOrganizatorDbContext = eventOrganizatorDbContext;
        }
        public async Task<City> Get(Expression<Func<City, bool>> filter = null)
        {
            return await _eventOrganizatorDbContext.Set<City>().Include("Events").AsNoTracking().FirstOrDefaultAsync(filter);
        }
        //public Task<City> GetByName(string name, Expression<Func<City, bool>> filter = null)
        //{
        //    return _eventOrganizerDbContext.Set<City>().AsNoTracking().Where(filter).FirstOrDefaultAsync(c => c.Name == name.ToLower());
        //}
    }
}
