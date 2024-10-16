using SmallCMS.API.DTOs;
using System.Net;
using System.Text.Json;

namespace SmallCMS.API.Middlewares
{
    /// <summary>
    /// Middleware to handle all unhandled exceptions globally.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="environment">The hosting environment.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Proceed to the next middleware/component in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            _logger.LogError(exception, "An unhandled exception has occurred.");

            // Set the response status code and content type
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";


            var response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Error = "An unexpected error occurred.",

                // Add more details in only development
                Details = _environment.IsDevelopment() ? exception.ToString() : null
            };

            // Serialize the response to JSON
            var jsonResponse = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
