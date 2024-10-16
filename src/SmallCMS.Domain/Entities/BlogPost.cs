using Microsoft.AspNetCore.Identity;

namespace SmallCMS.Domain.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
        public required string UserId { get; set; }
        public virtual IdentityUser? User { get; set; }

    }
}
