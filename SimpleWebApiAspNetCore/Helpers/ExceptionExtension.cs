using Microsoft.AspNetCore.Diagnostics;

namespace SimpleWebApiAspNetCore.Helpers;

public static class ExceptionExtension
{
    public static void ExceptionHandling(this IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (errorFeature != null)
                {
                    var logger = loggerFactory.CreateLogger("ExceptionHandlingMiddleware");
                    logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                }

                await context.Response.WriteAsync("There was an error");
            });
        });
    }
}
