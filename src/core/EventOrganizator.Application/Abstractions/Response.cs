using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Abstractions
{
    public abstract class Response
    {
        public List<string> Errors { get; set; } = new();
        public List<object> Data { get; set; } = new();
    }
}
