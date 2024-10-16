using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmallCMS.Application.BlogPosts.Queries.GetBlogPostById;
using SmallCMS.Application.Common.Models;
using SmallCMS.Domain.Entities;
using SmallCMS.Infrastructure.Data;
using Xunit;

namespace SmallCMS.Application.UnitTests.BlogPosts
{
    public class GetBlogPostByIdTests
    {
        private readonly ISmallCmsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBlogPostByIdTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogPost, BlogPostDto>();
            });
            _mapper = config.CreateMapper();

            var options = new DbContextOptionsBuilder<SmallCmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"SmallCmsTestDb_{Guid.NewGuid()}")
                .Options;

            var dbContext = new SmallCmsDbContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            _dbContext = dbContext;

            SeedData(dbContext);
        }

        private static void SeedData(SmallCmsDbContext context)
        {

            context.BlogPosts.RemoveRange(context.BlogPosts);
            context.SaveChanges();


            var blogPosts = new List<BlogPost>
            {
                 new() { Id = 1, Title = "Test Post 1", Body = "Body 1", CreatedDateUtc = System.DateTime.UtcNow.AddDays(-1), UserId = "1" },
                 new() { Id = 2, Title = "Test Post 2", Body = "Body 2", CreatedDateUtc = System.DateTime.UtcNow, UserId = "2" }
            };

            context.BlogPosts.AddRange(blogPosts);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetBlogPostByIdHandle_ShouldReturnBlogPostDto_WhenBlogPostExists()
        {
            // Arrange
            var query = new GetBlogPostByIdQuery
            {
                Id = 1
            };
            var handler = new GetBlogPostByIdHandler(_dbContext, _mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Post 1", result.Title);
            Assert.Equal("Body 1", result.Body);
        }

        [Fact]
        public async Task GetBlogPostByIdHandle_ShouldReturnNull_WhenBlogPostNotExist()
        {
            // Arrange
            var query = new GetBlogPostByIdQuery
            {
                Id = 50000
            };
            var handler = new GetBlogPostByIdHandler(_dbContext, _mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
