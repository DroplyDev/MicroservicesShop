#region

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Contracts.Responses;
using ProductService.Domain.Exceptions;
using Serilog;

#endregion

namespace ProductService.Infrastructure.Middlewares;

[SuppressMessage("ReSharper", "ContextualLoggerProblem")]
[SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
public sealed class ExceptionHandlingMiddleware
{
    private const string ContentType = "application/json";
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(ILogger logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {

            _logger.Warning(ex, ex.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = ContentType;
            await context.Response.WriteAsJsonAsync(new BadRequestResponse(ex.Errors));
        }
        catch (ApiException ex)
        {
            _logger.Write(ex.GetLevel(), ex, ex.Description);
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.Fatal(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, ApiException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        context.Response.ContentType = ContentType;
        return context.Response.WriteAsJsonAsync(new ApiExceptionResponse(exception.Description,
            context.Response.StatusCode));
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = ContentType;
        return context.Response.WriteAsJsonAsync(new ApiExceptionResponse(exception.Message,
            context.Response.StatusCode));
    }
}
