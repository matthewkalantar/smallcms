using Microsoft.EntityFrameworkCore;
using SmallCMS.Domain.Entities;

namespace SmallCMS.Infrastructure.Data
{
    public interface ISmallCmsDbContext
    {
        DbSet<BlogPost> BlogPosts { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
