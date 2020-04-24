using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.API.Middleware
{
   public class ErrorHandlingMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly ILogger<ErrorHandlingMiddleware> _logger;
      public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
      {
         _logger = logger;
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
            await HandleExceptionAsync(context, ex, _logger);
         }
      }

      private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
      {
         object errors = null;

         switch (ex)
         {
            case RestException rest:
               logger.LogError(ex, GlobalConstants.REST_ERROR);
               errors = rest.Errors;
               context.Response.StatusCode = (int)rest.Code;
               break;
            case ValidationException validation:
               logger.LogError(ex, GlobalConstants.VALIDATION_ERROR);
               errors = validation.Errors;
               context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
               break;
            case Exception e:
               logger.LogError(ex, GlobalConstants.SERVER_ERROR);
               errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
               context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
               break;
         };

         context.Response.ContentType = GlobalConstants.APPLICATION_TYPE_JSON;

         if (errors != null)
         {
            var result = JsonSerializer.Serialize(new { errors });

            await context.Response.WriteAsync(result);
         }
      }
   }
}