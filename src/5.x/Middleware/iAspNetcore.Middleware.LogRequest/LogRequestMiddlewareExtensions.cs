using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Builder;




namespace iAspNetcore.Middleware.LogRequest
{

    public static class LogRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseiAspNetcoreLogRequest(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestMiddleware>();
        }
    }
}