using MediatR;
using Microsoft.AspNetCore.Http;
using SmallCMS.Domain.Entities;
using SmallCMS.Infrastructure.Data;
using System.Security.Claims;
namespace SmallCMS.Application.BlogPosts.Commands.CreateBlogPost
{
    public record CreatePostCommand : IRequest<int>
    {
        public required string Title { get; init; }
        public required string Body { get; init; }
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly ISmallCmsDbContext _dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreatePostCommandHandler(ISmallCmsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dbcontext = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token.");

            var blogpost = new BlogPost
            {
                Body = request.Body,
                Title = request.Title,
                UserId = userIdClaim
            };

            _dbcontext.BlogPosts.Add(blogpost);

            await _dbcontext.SaveChangesAsync(cancellationToken);

            return blogpost.Id;
        }
    }
}
