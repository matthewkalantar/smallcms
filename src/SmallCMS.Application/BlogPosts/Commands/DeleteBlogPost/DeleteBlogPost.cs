using MediatR;
using SmallCMS.Infrastructure.Data;

namespace SmallCMS.Application.BlogPosts.Commands.DeleteBlogPost
{
    public record DeleteBlogPostCommand(int Id) : IRequest;
    public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand>
    {
        private readonly ISmallCmsDbContext _dbcontext;
        public DeleteBlogPostCommandHandler(ISmallCmsDbContext context)
        {
            _dbcontext = context;
        }

        public async Task Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPost = await _dbcontext.BlogPosts
            .FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new KeyNotFoundException("Blog post not found.");

            _dbcontext.BlogPosts.Remove(blogPost);

            await _dbcontext.SaveChangesAsync(cancellationToken);
        }
    }
}
