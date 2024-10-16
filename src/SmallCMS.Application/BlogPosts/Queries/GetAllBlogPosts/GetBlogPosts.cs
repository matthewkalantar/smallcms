using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmallCMS.Application.Common.Extensions;
using SmallCMS.Application.Common.Models;
using SmallCMS.Infrastructure.Data;

namespace SmallCMS.Application.BlogPosts.Queries.GetAllBlogPosts
{
    public record GetAllBlogPostsQuery : IRequest<PaginatedList<BlogPostDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
    public class GetAllBlogPostsQueryHandler : IRequestHandler<GetAllBlogPostsQuery, PaginatedList<BlogPostDto>>
    {
        private readonly ISmallCmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllBlogPostsQueryHandler(ISmallCmsDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BlogPostDto>> Handle(GetAllBlogPostsQuery request, CancellationToken cancellationToken)
        {
            var postsQuery = _dbContext.BlogPosts
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                postsQuery = postsQuery.Where(post => post.Title.Contains(request.SearchTerm) || post.Body.Contains(request.SearchTerm));
            }
            var q= await postsQuery
                .OrderBy(post => post.CreatedDateUtc)
                .ProjectTo<BlogPostDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return q;
            return await postsQuery
                .OrderBy(post => post.CreatedDateUtc)
                .ProjectTo<BlogPostDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

        }
    }
}
