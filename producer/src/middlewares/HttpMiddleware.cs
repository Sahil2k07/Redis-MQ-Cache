namespace producer.middlewares;

public class HttpMiddleware(ILogger<HttpMiddleware> logger, RequestDelegate next)
{
    private readonly ILogger<HttpMiddleware> _logger = logger;

    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new
            {
                Success = false,
                Message = "An Unexpected Server Error Occurred",
                Error = ex.Message,
            };

            await httpContext.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}

public static class HttpMiddlewareExtensions
{
    public static IApplicationBuilder UseHttpMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpMiddleware>();
    }
}
