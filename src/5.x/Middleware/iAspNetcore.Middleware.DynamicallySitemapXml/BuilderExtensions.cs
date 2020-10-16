using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace iAspNetcore.Middleware.Sitemap
{
   public static class BuilderExtensions
    {
        public static IApplicationBuilder UseSitemapMiddleware(this IApplicationBuilder app,
        string rootUrl = "http://localhost:5004")
        {
            return app.UseMiddleware<SitemapMiddleware>(new[] { rootUrl });
        }
    }
}
