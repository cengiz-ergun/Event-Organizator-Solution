using EventOrganizator.Application.Repositories;
using EventOrganizator.Application.UoW;
using EventOrganizator.Persistence.Contexts;
using EventOrganizator.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICityRepository CityRepository { get; private set; }

        private EventOrganizatorDbContext _eventOrganizatorDbContext;

        public UnitOfWork(EventOrganizatorDbContext eventOrganizatorDbContext)
        {
            _eventOrganizatorDbContext = eventOrganizatorDbContext;

            CityRepository = new CityRepository(_eventOrganizatorDbContext);
        }


        //
        public void Commit()
        {
            _eventOrganizatorDbContext.SaveChanges();
        }


    }
}
