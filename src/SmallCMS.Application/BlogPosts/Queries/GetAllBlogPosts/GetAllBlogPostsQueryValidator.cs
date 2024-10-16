using FluentValidation;

namespace SmallCMS.Application.BlogPosts.Queries.GetAllBlogPosts
{
    public class GetAllBlogPostsQueryValidator : AbstractValidator<GetAllBlogPostsQuery>
    {
        public GetAllBlogPostsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber should be greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize should be greater than or equal to 1.");
        }
    }
}
