using FirstAttempt.Core.DTOs;
using FirstAttempt.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace FirstAttemtp.API.Middlewares
{
    //Extencion classı yazabilmek için class static olmak zorunda
    public static class UseCustomExceptionHandler
    {
        public static void UserCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                //run sonlandırıcı middleware
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFuture = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFuture.Error switch
                    {
                        ClientSideException => 400,
                        _ => 500
                        //Default 500
                    };
                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFuture.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });
            });
        }
    }
}
