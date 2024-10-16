using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using SmallCMS.Application.Common.Extensions;
using SmallCMS.Application.Common.Models;
using SmallCMS.Infrastructure.Data;
using System.Security.Claims;

namespace SmallCMS.Application.BlogPosts.Queries.GetAllBlogPostsByUser
{
    public record GetAllBlogPostsByUserQuery : IRequest<PaginatedList<BlogPostDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class GetAllBlogPostsByUserQueryHandler : IRequestHandler<GetAllBlogPostsByUserQuery, PaginatedList<BlogPostDto>>
    {
        private readonly ISmallCmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllBlogPostsByUserQueryHandler(ISmallCmsDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PaginatedList<BlogPostDto>> Handle(GetAllBlogPostsByUserQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token.");

            return await _dbContext.BlogPosts
                     .Where(x => x.UserId == userIdClaim)
                     .OrderBy(post => post.CreatedDateUtc)
                     .ProjectTo<BlogPostDto>(_mapper.ConfigurationProvider)
                     .PaginatedListAsync(request.PageNumber, request.PageSize);

        }
    }
}
