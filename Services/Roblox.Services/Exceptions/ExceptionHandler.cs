using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Roblox.Services.Exceptions.Services;
using Roblox.Services.Models;

namespace Roblox.Services.Exceptions
{
    public static class ExceptionHandler
    {
        public static async Task OnError(Exception error, HttpContext context)
        {
            int statusCode = 500;
            List<ErrorResponseEntry> errors = new();
            if (error is IHttpException exception)
            {
                statusCode = exception.statusCode;
                errors.Add(new ()
                {
                    code = exception.errorCode,
#if DEBUG
                    message = error.Message + "\n" + error.StackTrace,
#endif
                });
            }

            if (errors.Count == 0)
            {
                errors.Add(new()
                {
                    code = ErrorCode.InternalServerError,
                    message = "Internal server error"
                });
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new ErrorResponse()
            {
                errors = errors,
            });
        }
    }
}