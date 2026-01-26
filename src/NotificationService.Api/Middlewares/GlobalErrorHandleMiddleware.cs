using NotificationService.Api.Responses;
using System.Net;

namespace NotificationService.Api.Middlewares
{
    public class GlobalErrorHandleMiddleware
    {

        private readonly ILogger<GlobalErrorHandleMiddleware> _logger;
        private readonly RequestDelegate _next;



        public GlobalErrorHandleMiddleware(RequestDelegate next,
                                           ILogger<GlobalErrorHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {


            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {


                await HandleException(context, ex);

            }
        }


        public async Task HandleException(HttpContext context, Exception exception)
        {


            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            _logger.LogError(exception, "An unhandled exception occurred while processing the request.");

            var response=ApiResponseFactory.Failure("An unexpected error occurred. Please try again later.");



            await context.Response.WriteAsJsonAsync(response);
        }

    }
}
