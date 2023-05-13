using EventOrganizator.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizator.API.Helpers
{
    public static class CustomHttpResponse
    {
        public static ObjectResult Result(Response response)
        {
            if (response.Errors.Count > 0) 
            {
                return new BadRequestObjectResult(new
                {
                    Message = "Some error(s) while handling your request.",
                    Errors = response.Errors
                });
            }
            else if(response.Data.Count > 0)
            {
                return new OkObjectResult(new
                {
                    Message = "Your request handled succesfully.",
                    Data = response.Data
                });
            }
            return new OkObjectResult(new
            {
                Message = "Your request handled succesfully."
            });
        }
    }
}
