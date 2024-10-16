using Microsoft.EntityFrameworkCore;
using SmallCMS.Application.Common.Models;

namespace SmallCMS.Application.Common.Extensions
{
    public static class PaginationExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
    => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    }
}
