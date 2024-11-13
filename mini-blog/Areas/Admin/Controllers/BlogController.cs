using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini_blog.DataAccess;
using mini_blog.Entities;
using mini_blog.Enum;
using mini_blog.Helpers;

namespace mini_blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly BlogContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(BlogContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 5;

            var currentUser = await _userManager.GetUserAsync(User);

            var blogsQuery = _context.Blogs
                                     .Include(b => b.AppUser)
                                     .Where(b => b.Status == BlogStatus.Wait)
                                     .AsNoTracking();

            var paginatedBlogs = await PaginatedList<Blog>.CreateAsync(blogsQuery, pageNumber, pageSize);

            return View(paginatedBlogs);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptBlog(int id) 
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            blog.Status = BlogStatus.Active;

            if (await _context.SaveChangesAsync() > 0)
            {
                return Json(new { success = true, message = "Blog was accepted" });
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> RejectBlog(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            blog.Status = BlogStatus.Reject;

            if (await _context.SaveChangesAsync() > 0)
            {
                return Json(new { success = true, message = "Blog was rejected" });
            }

            return BadRequest();
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
    }
}
