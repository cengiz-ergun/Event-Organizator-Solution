using EventOrganizator.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizator.API.Helpers
{
    public static class CustomHttpResponse
    {
        public class SuccesfulResponse
        {
            public string Message { get; set; } = "Your request handled succesfully.";
        }

        public class BadResponse
        {
            public string Message { get; set; } = "Some error(s) while handling your request.";
            public List<string> Errors { get; set; } = new();
        }

        public static ObjectResult Result(Response response)
        {
            if (response.Errors.Count > 0) 
            {
                return new BadRequestObjectResult(new BadResponse()
                {
                    Errors = response.Errors
                });
            }
            return new OkObjectResult(new SuccesfulResponse());
        }
    }
}
