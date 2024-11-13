namespace mini_blog.ViewModels
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Category { get; set; }
        public IFormFile? Image { get; set; }
    }
}
