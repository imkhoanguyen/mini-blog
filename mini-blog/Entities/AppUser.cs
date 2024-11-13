using Microsoft.AspNetCore.Identity;

namespace mini_blog.Entities
{
    public class AppUser : IdentityUser
    {
        public required string FullName { get; set; }
        public List<Blog> Blogs { get; set; } = [];
    }
}
