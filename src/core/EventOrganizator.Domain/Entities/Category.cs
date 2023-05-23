using EventOrganizator.Domain.Entities.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
