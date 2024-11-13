
namespace mini_blog.ViewModels
{
    public class BlogCreateVM
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Category { get; set; }
        public required IFormFile Image { get; set; }
    }
}
