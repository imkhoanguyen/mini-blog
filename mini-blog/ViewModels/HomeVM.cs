using mini_blog.Entities;
using mini_blog.Helpers;

namespace mini_blog.ViewModels
{
    public class HomeVM
    {
        public PaginatedList<Blog> BlogList { get; set; }
        public List<string> CategoryList { get; set; } = [];
    }
}
