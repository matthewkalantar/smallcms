using Microsoft.EntityFrameworkCore;
using Moq;
using SmallCMS.Application.BlogPosts.Commands.DeleteBlogPost;
using SmallCMS.Domain.Entities;
using SmallCMS.Infrastructure.Data;
using Xunit;

namespace SmallCMS.Application.UnitTests.BlogPosts
{
    public class DeleteBlogPostTests
    {
        private readonly Mock<ISmallCmsDbContext> _testDbContext;
        private readonly DeleteBlogPostCommandHandler _commandHandler;

        public DeleteBlogPostTests()
        {
            _testDbContext = new Mock<ISmallCmsDbContext>();
            _commandHandler = new DeleteBlogPostCommandHandler(_testDbContext.Object);
        }

        [Fact]
        public async Task DeleteBlogPostHandle_ShouldRemoveBlogPost_WhenBlogPostExists()
        {
            // Arrange
            var blogPostId = 1;
            var blogPost = new BlogPost { Id = blogPostId, Title = "test title1", Body = "Test Content 1",UserId = "1" };
            var mockDbSet = new Mock<DbSet<BlogPost>>();

            _testDbContext.Setup(m => m.BlogPosts.FindAsync(new object[] { blogPostId }, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(blogPost);

            // Act
            await _commandHandler.Handle(new DeleteBlogPostCommand(blogPostId), CancellationToken.None);

            // Assert
            _testDbContext.Verify(m => m.BlogPosts.Remove(It.Is<BlogPost>(p => p.Id == blogPostId)), Times.Once);
            _testDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteBlogPostHandle_ShouldThrowKeyNotFoundException_WhenPostNotExist()
        {
            // Arrange
            var blogPostId = 99;

            _testDbContext.Setup(m => m.BlogPosts.FindAsync(new object[] { blogPostId }, It.IsAny<CancellationToken>()))
                          .ReturnsAsync((BlogPost)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _commandHandler.Handle(new DeleteBlogPostCommand(blogPostId), CancellationToken.None));
        }
    }
}
