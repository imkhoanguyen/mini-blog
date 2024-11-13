using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini_blog.DataAccess;
using mini_blog.Entities;
using mini_blog.Helpers;
using mini_blog.Models;
using mini_blog.ViewModels;
using System.Diagnostics;

namespace mini_blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContext _context;

        public HomeController(BlogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, string category = "")
        {
            int pageSize = 10;

            ViewData["CurrentFilter"] = category;

            var blogsQuery = _context.Blogs
                                     .Include(b => b.AppUser)
                                     .Where(b=> b.Status == Enum.BlogStatus.Active)
                                     .OrderByDescending(b=> b.Id)
                                     .AsNoTracking();

            var categoryList = _context.Blogs.Where(b => b.Status == Enum.BlogStatus.Active)
                .Select(b=>b.Category).Distinct().ToList();

            if(!string.IsNullOrEmpty(category))
            {
                blogsQuery = blogsQuery.Where(b => b.Category == category);
            }

            var paginatedBlogs = await PaginatedList<Blog>.CreateAsync(blogsQuery, pageNumber, pageSize);

            var vm = new HomeVM
            {
                BlogList = paginatedBlogs,
                CategoryList = categoryList
            };

            return View(vm);

        }

        public IActionResult Detail(int id)
        {
            var blog = _context.Blogs
                .Include(b => b.AppUser)
                .FirstOrDefault(b => b.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
