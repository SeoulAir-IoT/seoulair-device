﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SeoulAir.Device.Domain.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Api.Configuration
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var problemDetails = GenerateProblemDetails(ex);
            var jsonSetting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var result = JsonConvert.SerializeObject(problemDetails, jsonSetting);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)problemDetails.Status;

            return context.Response.WriteAsync(result);
        }

        private static ProblemDetails GenerateProblemDetails(Exception ex)
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
                    break;
                default:
                    code = HttpStatusCode.NotImplemented;
                    type = NotImplementedUri;
                    title = NotImplementedTitle;
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
