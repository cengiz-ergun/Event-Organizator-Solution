using EventOrganizator.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.UoW
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IEventRepository EventRepository { get; }

        void Commit();
    }
}
