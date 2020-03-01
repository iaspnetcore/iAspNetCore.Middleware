

using System;
using System.Collections.Generic;

namespace iAspNetcore.Middleware.Pagination
{
    public interface IBuildPaginationLinks
    {
        List<PaginationLink> BuildPaginationLinks(
            PaginationSettings paginationSettings,
            Func<int, string> generateUrl,
            string firstPageText,
            string firstPageTitle,
            string previousPageText,
            string previousPageTitle,
            string nextPageText,
            string nextPageTitle,
            string lastPageText,
            string lastPageTitle,
            string spacerText = "...");

    }
}
