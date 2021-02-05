using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeoulAir.Device.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Api.Configuration
{
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
            context.Response.StatusCode = (int)problemDetails.Status;

            return context.Response.WriteAsync(result);
        }

        private ProblemDetails GenerateProblemDetails(Exception ex)
        {
            string type;
            string title;
            HttpStatusCode code;

            switch (ex)
            {
                case ConfigurationException configurationException:
                case FileDoesNotExistException fileDoesNotExistException:
                case InvalidColumnTypeException invalidColumnTypeMessage:
                case InvalidDateTimeFormatException invalidDateTimeFormatException:
                case InvalidFileFormatException invalidFileFormatException:
                case InvalidStationCodeFormatException invalidStationCodeFormatException:
                    code = HttpStatusCode.InternalServerError;
                    type = InternalServerErrorUri;
                    title = InternalServerErrorTitle;
                    _logger.LogError(ex.ToString());
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    type = InternalServerErrorUri;
                    title = InternalServerErrorTitle;
                    _logger.LogError(ex.ToString());
                    break;
            }

            return new ProblemDetails()
            {
                Type = type,
                Title = title,
                Detail = ex.Message,
                Status = (int)code
            };
        }
    }
}
