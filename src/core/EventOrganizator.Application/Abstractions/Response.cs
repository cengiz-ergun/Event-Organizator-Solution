using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Abstractions
{
    public abstract class Response
    {
        public List<string> Errors { get; set; } = new();
        public List<object> Data { get; set; } = new();
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}
