using MediatR;
using SmallCMS.Infrastructure.Data;

namespace SmallCMS.Application.BlogPosts.Commands.UpdateBlogPost
{
    public record UpdatePostCommand : IRequest
    {
        public int Id { get; set; }
        public required string Title { get; init; }
        public required string Body { get; init; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly ISmallCmsDbContext _dbcontext;
        public UpdatePostCommandHandler(ISmallCmsDbContext context)
        {
            _dbcontext = context;
        }

        public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var blogPost = await _dbcontext.BlogPosts
            .FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new KeyNotFoundException("Blog post not found.");

            blogPost.Title = request.Title;
            blogPost.Body = request.Body;

            await _dbcontext.SaveChangesAsync(cancellationToken);
        }
    }
}
