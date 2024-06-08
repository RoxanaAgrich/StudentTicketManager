using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
namespace Infrastrucrure.Middleware
{
    internal sealed class ExceptionHandlingMiddleware:IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
            => _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e) 
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new
        {
            title = GetTitile(exception),
            status = statusCode,
            detail = exception.Message,
            error = GetError(exception)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status404NotFound,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError,
        };
    }

    private static string GetTitile(Exception exception)
        => exception switch
        {
            ValidationException applicationException => applicationException.Message,
            _=>"Server Error",
        };
    
    private static IReadOnlyCollection<Application.Exceptions.ValidationError> GetError (Exception errors)
    {
        IReadOnlyCollection<Application.Exceptions.ValidationError> errors = null;
        if (errors is Application.Exceptions.ValidationException validationException)
        {
            errors = validationException.Errors;
        }
        return errors;
    }
 
}
