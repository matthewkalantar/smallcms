using Moq;
using SmallCMS.Application.BlogPosts.Commands.UpdateBlogPost;
using SmallCMS.Domain.Entities;
using SmallCMS.Infrastructure.Data;
using Xunit;

namespace SmallCMS.Application.UnitTests.BlogPosts
{
    public class UpdatePostTests
    {
        private readonly Mock<ISmallCmsDbContext> _testDbContext;
        private readonly UpdatePostCommandHandler _commandHandler;

        public UpdatePostTests()
        {
            _testDbContext = new Mock<ISmallCmsDbContext>();
            _commandHandler = new UpdatePostCommandHandler(_testDbContext.Object);
        }

        [Fact]
        public async Task UpdatePostHandle_ShouldUpdateBlogPost_WhenBlogPostExists()
        {
            // Arrange
            var blogPostId = 1;
            var existingBlogPost = new BlogPost 
            {
                Id = blogPostId,
                Title = "Old Title",
                Body = "Old Content",
                UserId = "1"
                
            };

            _testDbContext.Setup(m => m.BlogPosts.FindAsync(new object[] { blogPostId }, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(existingBlogPost);

            var command = new UpdatePostCommand
            {
                Id = blogPostId,
                Title = "new Title",
                Body = "new Content"
            };

            // Act
            await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            _testDbContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(command.Title, existingBlogPost.Title);
            Assert.Equal(command.Body, existingBlogPost.Body);
        }

        [Fact]
        public async Task UpdatePostHandle_ShouldThrowKeyNotFoundException_WhenBlogPostDoesNotExist()
        {
            // Arrange
            var blogPostId = 50000; 

            _testDbContext.Setup(m => m.BlogPosts.FindAsync(new object[] { blogPostId }, It.IsAny<CancellationToken>()))
                          .ReturnsAsync((BlogPost)null);

            var command = new UpdatePostCommand
            {
                Id = blogPostId,
                Title = "Title",
                Body = "Body"
            };

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _commandHandler.Handle(command, CancellationToken.None));
        }
    }
}
