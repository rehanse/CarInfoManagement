using Newtonsoft.Json;
using System.Net;
using System;
using Microsoft.AspNetCore.Diagnostics;

namespace CarInfoManagement.Middleware
{
    public class ExceptionMiddleware
    {
        /// <summary>
        /// // middleware for handling exception and generating consistent error reponses
        /// </summary>
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// Method to handle exception and send consistent error reponses
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns>return the Exception Responses</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            //Set the Http Status code for the response and write the exception as JSON to the response stream
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
    //Exetension method used to add the middleware to the HTTP request pipeline
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
