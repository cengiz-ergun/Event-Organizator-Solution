using EventOrganizator.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EventOrganizator.API.Helpers
{
    public static class CustomHttpResponse
    {
        public static IActionResult Result(Response response)
        {
            if (response.Errors.Count > 0)
            {
                return new UnprocessableEntityObjectResult(new
                {
                    errors = response.Errors
                });
            }
            else if (response.Data.Count > 0)
            {
                return new OkObjectResult(new
                {
                    payload = response.Data,
                    count   = response.Data.Count
                });              

            }
            else
            {
                return new NoContentResult();
            }
        }
    }
}
