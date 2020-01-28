using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Elmah.Io.AspNetCore;

namespace MinhaAPICore.Extensoes
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            await exception.ShipAsync(context);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;           
        }

        //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //    return Task.CompletedTask;
        //}
    }
}
