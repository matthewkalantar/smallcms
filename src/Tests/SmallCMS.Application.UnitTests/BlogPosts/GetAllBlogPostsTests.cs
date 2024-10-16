using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmallCMS.Application.BlogPosts.Queries.GetAllBlogPosts;
using SmallCMS.Application.Common.Models;
using SmallCMS.Domain.Entities;
using SmallCMS.Infrastructure.Data;
using Xunit;

namespace SmallCMS.Application.UnitTests.BlogPosts
{
    public class GetAllBlogPostsTests
    {
        private readonly ISmallCmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllBlogPostsTests()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogPost, BlogPostDto>();
            });
            _mapper = config.CreateMapper();


            var options = new DbContextOptionsBuilder<SmallCmsDbContext>()
                .UseInMemoryDatabase(databaseName: "SmallCmsTestDb")
                .Options;

            var dbContext = new SmallCmsDbContext(options);
            _dbContext = dbContext;

            SeedData(dbContext);
        }

        private static void SeedData(SmallCmsDbContext context)
        {
            var blogPosts = new List<BlogPost>
            {
                  new() { Id = 1, Title = "Post 1", Body = "Body 1", CreatedDateUtc = System.DateTime.UtcNow.AddDays(-1), UserId = "1" },
                  new() { Id = 2, Title = "Post 2", Body = "Body 2", CreatedDateUtc = System.DateTime.UtcNow, UserId = "2" }
            };

            context.BlogPosts.AddRange(blogPosts);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetAllBlogPostsHandle_ShouldReturnPaginatedList_WhenPostsExist()
        {
            // Arrange
            var query = new GetAllBlogPostsQuery
            {
                PageNumber = 1,
                PageSize = 10
            };

            var queryHandler = new GetAllBlogPostsQueryHandler(_dbContext, _mapper);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count);
            Assert.Equal("Post 1", result.Items.ElementAt(0).Title);
            Assert.Equal("Post 2", result.Items.ElementAt(1).Title);
        }
    }
}
