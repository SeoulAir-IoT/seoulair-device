using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SeoulAir.Device.Client.Enums;
using SeoulAir.Device.Domain.Enums;
using SeoulAir.Device.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeoulAir.Device.Api.Configuration;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var problemDetails = GenerateProblemDetails(ex);
        var jsonSetting = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var result = JsonSerializer.Serialize(problemDetails, jsonSetting);

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.StatusCode;

        _logger.LogError(ex, ex.Message);
        return context.Response.WriteAsync(result);
    }

    private (int StatusCode, int ErrorCode, string Title) GenerateProblemDetails(Exception ex)
    {
        return ex switch
        {
            SeoulAirException seoulAirException => 
                (seoulAirException.StatusCode,
                (int)seoulAirException.ErrorCode,
                seoulAirException.Title),
            _ => ((int)HttpStatusCode.InternalServerError,
                  (int)SeoulAirDeviceErrorCodes.NotKnownError,
                  "Something went wrong!"),
        };
    }
}
