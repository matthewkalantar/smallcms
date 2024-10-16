using AutoMapper;
using SmallCMS.Domain.Entities;

namespace SmallCMS.Application.Common.Models
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public DateTime CreatedDateUtc { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<BlogPost, BlogPostDto>();
            }
        }
    }

}
