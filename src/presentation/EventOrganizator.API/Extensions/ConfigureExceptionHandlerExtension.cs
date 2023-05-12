using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using EventOrganizator.Application.Exceptions;

namespace EventOrganizator.API.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var message = "Error";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        switch (contextFeature.Error)
                        {
                            //case UserHasNotOneRoleException:
                            //    break;
                            //case NotFoundException:
                            //    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                            //    message = contextFeature.Error.Message;
                            //    break;
                            default:
                                break;
                        }
                        var result = contextFeature.Error;

                        logger.LogError(contextFeature.Error.Message);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            //StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error",                       
                            //Title = "Hata alındı!"
                        })); ;
                    }
                });
            });
        }
    }
}

