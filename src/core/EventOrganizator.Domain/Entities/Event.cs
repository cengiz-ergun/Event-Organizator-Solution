using EventOrganizator.Domain.Entities.Common;
using EventOrganizator.Domain.Entities.Identity;
using EventOrganizator.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Domain.Entities
{
    public class Event: BaseEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
        public string Address { get; set; }
        public int NumberOfPeople { get; set; }
        public EventStatus EventStatus { get; set; }

        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public string CreatedByAppUserId { get; set; }
        [ForeignKey(nameof(CreatedByAppUserId))]
        public AppUser CreatedByAppUser { get; set; }
    }
}
