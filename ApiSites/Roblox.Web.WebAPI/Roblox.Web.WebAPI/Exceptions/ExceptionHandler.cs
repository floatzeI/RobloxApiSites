using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Roblox.Web.WebAPI.Exceptions;

namespace Roblox.Web.WebAPI
{
    public static class WebApiExceptionHandler
    {
        public static async Task OnError(Exception error, HttpContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errors = new List<ErrorEntry>();
            if (error is IWebApiException exception)
            {
                statusCode = exception.statusCode;
                errors.Add(new ()
                {
                    code = exception.code,
                    message = exception.message,
                    userFacingMessage = exception.message,
                });
            }
            else
            {
                errors.Add(new ()
                {
                    code = 0,
#if DEBUG
                    message = error.Message + "\n" + error.StackTrace,
#else
                    message = "InternalServerError"
#endif
                    userFacingMessage = "Something went wrong",
                });
            }
            
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(new ErrorResponse()
            {
                errors = errors,
            });
        }
    }
}