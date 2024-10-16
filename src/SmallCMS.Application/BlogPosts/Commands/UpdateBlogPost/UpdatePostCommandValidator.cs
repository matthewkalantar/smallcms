using FluentValidation;

namespace SmallCMS.Application.BlogPosts.Commands.UpdateBlogPost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(v => v.Title)
               .MaximumLength(120)
               .NotEmpty();
        }
    }
}
