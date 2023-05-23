using EventOrganizator.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.Event
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Address { get; set; }
        public EventStatus EventStatus { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }

        public string CreatedByAppUserId { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
    }
}
