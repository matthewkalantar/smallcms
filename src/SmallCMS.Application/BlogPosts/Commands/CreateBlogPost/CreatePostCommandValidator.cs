using FluentValidation;

namespace SmallCMS.Application.BlogPosts.Commands.CreateBlogPost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(v => v.Title)
               .MaximumLength(120)
               .NotEmpty();
        }
    }
}
