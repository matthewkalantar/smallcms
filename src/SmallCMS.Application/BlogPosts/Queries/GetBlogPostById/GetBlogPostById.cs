using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmallCMS.Application.Common.Models;
using SmallCMS.Infrastructure.Data;

namespace SmallCMS.Application.BlogPosts.Queries.GetBlogPostById
{
    public class GetBlogPostByIdQuery : IRequest<BlogPostDto?>
    {
        public int Id { get; set; }
    }
    public class GetBlogPostByIdHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto?>
    {
        private readonly ISmallCmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBlogPostByIdHandler(ISmallCmsDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<BlogPostDto?> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.BlogPosts
                              .AsNoTracking()
                              .Where(bp => bp.Id == request.Id)
                              .ProjectTo<BlogPostDto>(_mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync(cancellationToken);

            return post;
        }
    }

}
