using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PokemonTranslator.Api.Models;
using PokemonTranslator.Core.Exceptions;
using PokemonTranslator.Infrastructure.ShakespeareClient.Exceptions;

namespace PokemonTranslator.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            switch (exception)
            {
                case PokemonNotFoundException:
                {
                    context.Response.StatusCode = 404;
                    return Task.CompletedTask;
                }
                case HttpRequestException httpRequestException:
                {
                    _logger.LogError(httpRequestException, httpRequestException.Message);
                    var errorsResponse = new ErrorsResponse
                    {
                        Errors = new List<Error>
                        {
                            new()
                            {
                                Code = "HttpRequestErrorKey",
                                Message = httpRequestException.Message
                            }
                        }
                    };
                    context.Response.StatusCode = 400;
                    return context.Response.WriteAsJsonAsync(errorsResponse);
                }
                case ShakespeareClientException shakespeareClientException:
                {
                    _logger.LogError(shakespeareClientException, shakespeareClientException.Message);
                    var errorsResponse = new ErrorsResponse
                    {
                        Errors = new List<Error>
                        {
                            new()
                            {
                                Code = shakespeareClientException.Code,
                                Message = shakespeareClientException.Message
                            }
                        }
                    };
                    return context.Response.WriteAsJsonAsync(errorsResponse);
                }
                default:
                {
                    _logger.LogError(exception, exception.Message);
                    var defaultErrorResponse = new ErrorsResponse
                    {
                        Errors = new List<Error>
                        {
                            new()
                            {
                                Code = "InternalServerErrorKey",
                                Message = "Internal server error"
                            }
                        }
                    };
                    context.Response.StatusCode = 500;
                    return context.Response.WriteAsJsonAsync(defaultErrorResponse);
                }
            }
        }
    }
}