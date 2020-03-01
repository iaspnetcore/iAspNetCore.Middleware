using Microsoft.AspNetCore.Builder;


namespace iAspNetcore.Middleware.LogResponse
{

    public static class LogRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseiAspNetcoreLogResponse(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogResponseMiddleware>();
        }
    }
}
