using System.Text.Json;
using CompanyWebsite.Application.Exceptions;
using Shared;

namespace CompanyWebsite.Web.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, exception.Message);

        (int code, Error[]? errors) = exception switch
        {
            BadRequestException => (StatusCodes.Status500InternalServerError,
                JsonSerializer.Deserialize<Error[]>(exception.Message)),

            NotFoundException => (StatusCodes.Status404NotFound,
                JsonSerializer.Deserialize<Error[]>(exception.Message)),

            _ => (StatusCodes.Status500InternalServerError, [Error.Failure(null, "Something went wrong")])
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;
        
        await context.Response.WriteAsJsonAsync(errors);
    }
}

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app) =>
        app.UseMiddleware<ExceptionMiddleware>();
}