using InvoiceApp.Helpers.Exceptions;
using System.Web;

namespace InvoiceApp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (AppException e)
            {
                HandleException(e, httpContext);
            }
            catch (Exception e)
            {
                HandleException(new AppException(), httpContext);
            }
        }


        private void HandleException(AppException exception, HttpContext httpContext)
        {
            httpContext.Response.Redirect($"/Home/AppError?ErrorCode={exception.ErrorCode}&ErrorDetails={HttpUtility.UrlPathEncode(exception.ErrorDetails)}&Message={HttpUtility.UrlPathEncode(exception.Message)}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
