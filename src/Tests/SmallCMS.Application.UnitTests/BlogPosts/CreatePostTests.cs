using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmallCMS.Application.BlogPosts.Commands.CreateBlogPost;
using SmallCMS.Domain.Entities;
using SmallCMS.Infrastructure.Data;
using System.Security.Claims;
using Xunit;

namespace SmallCMS.Application.UnitTests.BlogPosts
{
    public class CreatePostTests
    {
        private readonly Mock<ISmallCmsDbContext> _testDbContext;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly CreatePostCommandHandler _commandHandler;

        public CreatePostTests()
        {
            _testDbContext = new Mock<ISmallCmsDbContext>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));

            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            _commandHandler = new CreatePostCommandHandler(_testDbContext.Object, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task CreatePostHandle_ShouldThrowUnauthorizedAccessException_WhenUserIsNotValid()
        {
            // Arrange
            var command = new CreatePostCommand
            {
                Title = "Unauthorized Title",
                Body = "Unauthorized Body"
            };

            // mock not valid user
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext)null);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _commandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task CreatePostHandle_ShouldCreateBlogPost_WhenUserIsValid()
        {
            // Arrange
            var command = new CreatePostCommand
            {
                Title = "post 1 test",
                Body = "post 1 Test content"
            };

            var mockDbSet = new Mock<DbSet<BlogPost>>();
            _testDbContext.Setup(m => m.BlogPosts).Returns(mockDbSet.Object);
            _testDbContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _testDbContext.Verify(m => m.BlogPosts.Add(It.Is<BlogPost>(p =>
                p.Title == command.Title &&
                p.Body == command.Body &&
                p.UserId == "1"
            )), Times.Once);

            _testDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<int>(result);
        }
    }
}
