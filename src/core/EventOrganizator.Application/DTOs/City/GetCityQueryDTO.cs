using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.City
{
    public class GetCityQueryDTO
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}
