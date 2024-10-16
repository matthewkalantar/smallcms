using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallCMS.Application.BlogPosts.Commands.CreateBlogPost;
using SmallCMS.Application.BlogPosts.Commands.DeleteBlogPost;
using SmallCMS.Application.BlogPosts.Commands.UpdateBlogPost;
using SmallCMS.Application.BlogPosts.Queries.GetAllBlogPosts;
using SmallCMS.Application.BlogPosts.Queries.GetAllBlogPostsByUser;
using SmallCMS.Application.BlogPosts.Queries.GetBlogPostById;

namespace SmallCMS.API.Endpoints
{
    public static class BlogPostEndpoints
    {
        public static IEndpointRouteBuilder MapBlogPostEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/api/blogposts", async (IMediator mediator, [FromQuery] int? pageNumber, [FromQuery] int? pageSize,
                [FromQuery] string? searchTerm = null) =>
            {
                var query = new GetAllBlogPostsQuery
                {
                    PageNumber = pageNumber ?? 1,
                    PageSize = pageSize ?? 10,
                    SearchTerm = !string.IsNullOrEmpty(searchTerm) ? searchTerm : null
                };

                var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .WithName("GetAllBlogPosts");

            app.MapGet("/api/blogposts/{id:int}", async (int id, IMediator mediator) =>
            {
                var query = new GetBlogPostByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .WithName("GetBlogPostById");

            app.MapPost("/api/blogposts", async (CreatePostCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.Ok();
            })
            .WithName("CreateBlogPost");

            app.MapPut("/api/blogposts/{id:int}", [Authorize] async (int id, UpdatePostCommand command, IMediator mediator) =>
            {
                command.Id = id;
                await mediator.Send(command);
                return Results.NoContent();
            })
            .WithName("UpdateBlogPost");

            app.MapDelete("/api/blogposts/{id:int}", [Authorize] async (int id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteBlogPostCommand(id));
                return Results.NoContent();
            })
            .WithName("DeleteBlogPost");

            app.MapGet("/api/user/blogposts", async (int? pageNumber, int? pageSize, IMediator mediator) =>
            {
                var query = new GetAllBlogPostsByUserQuery
                {
                    PageNumber = pageNumber ?? 1,
                    PageSize = pageSize ?? 10
                };

                var result = await mediator.Send(query);
                return Results.Ok(result);
            })

            .WithName("GetAllBlogPostsByUser");
            return app;
        }
    }
}
