using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using mini_blog.Enum;

namespace mini_blog.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; }
        public required string Category { get; set; }
        public BlogStatus Status { get; set; } = BlogStatus.Wait;
        public required string ImgUrl { get; set; }
        public required string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
