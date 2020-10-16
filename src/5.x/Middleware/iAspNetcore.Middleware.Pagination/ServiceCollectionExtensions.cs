using iAspNetcore.Middleware.Pagination;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddiAspNetcorePagination(this IServiceCollection services)
        {
            services.TryAddTransient<IBuildPaginationLinks, PaginationLinkBuilder>();

            return services;
        }
    }
}
