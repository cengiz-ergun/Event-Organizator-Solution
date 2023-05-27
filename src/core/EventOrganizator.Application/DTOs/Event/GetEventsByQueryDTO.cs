using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.Event
{
    public class GetEventsByQueryDTO
    {
        public int Page { get; set; } 
        public int Size { get; set; } 
    }
}
