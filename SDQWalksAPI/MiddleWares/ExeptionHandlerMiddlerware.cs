using System.Net;

namespace SDQWalksAPI.MiddleWares
{
    public class ExeptionHandlerMiddlerware
    {
        private readonly ILogger<ExeptionHandlerMiddlerware> logger;
        private readonly RequestDelegate next;

        public ExeptionHandlerMiddlerware(ILogger<ExeptionHandlerMiddlerware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                logger.LogError(ex, $"{errorId} : {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong we are looking to fix this"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
