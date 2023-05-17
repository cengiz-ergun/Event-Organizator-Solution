using EventOrganizator.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EventOrganizator.API.Helpers
{
    public static class CustomHttpResponse
    {
        public static IActionResult Result(Response response)
        {
            switch (response.HttpStatusCode)
            {

                case System.Net.HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(new
                    {
                        errors = response.Errors
                    });

                case System.Net.HttpStatusCode.OK:
                    return new OkObjectResult(new
                    {
                        data = response.Data,
                        count = response.Data.Count
                    });
                case System.Net.HttpStatusCode.Created:
                    return new OkObjectResult(new //should be CreatedResult. this will be handled after user endpoint
                    {
                        data = response.Data,
                        count = response.Data.Count
                    });
                case System.Net.HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(new
                    {
                        message = response.Message
                    });
                case System.Net.HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(new
                    {
                        message = response.Message
                    });
                case System.Net.HttpStatusCode.NoContent:
                    return new NoContentResult();
                default:
                    break;                
            }
            throw new Exception();
        }
    }
}
