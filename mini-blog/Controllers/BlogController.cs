using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini_blog.DataAccess;
using mini_blog.Entities;
using mini_blog.Helpers;
using mini_blog.ViewModels;

namespace mini_blog.Controllers
{
    [Authorize]
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
                                     .Where(b => b.UserId == currentUser.Id)
                                     .AsNoTracking();

            var paginatedBlogs = await PaginatedList<Blog>.CreateAsync(blogsQuery, pageNumber, pageSize);

            return View(paginatedBlogs);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);


            var currentUser = await _userManager.GetUserAsync(User);

            string imagePath = string.Empty;
            if (model.Image != null)
            {
                var fileName = Path.GetFileName(model.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;
            }

            var newBlog = new Blog
            {
                Title = model.Title,
                Content = model.Content,
                Category = model.Category,
                ImgUrl = imagePath,
                UserId = currentUser.Id
            };

            _context.Blogs.Add(newBlog);
            await _context.SaveChangesAsync();
            TempData["success"] = "The blog has been created successfully.";
            return RedirectToAction("Index", "Blog");
        }


        public IActionResult Edit(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            var blogEditVM = new BlogEditVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Category = blog.Category,
            };
            return View(blogEditVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var currentUser = await _userManager.GetUserAsync(User);

            var blogToUpdate = await _context.Blogs.FindAsync(model.Id);
            if (blogToUpdate == null) return NotFound();

            string imagePath = blogToUpdate.ImgUrl;

            if (model.Image != null)
            {
                // Nếu có ảnh mới, xóa ảnh cũ
                if (!string.IsNullOrEmpty(blogToUpdate.ImgUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blogToUpdate.ImgUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Lưu ảnh mới
                var fileName = Path.GetFileName(model.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;
            }

            blogToUpdate.Title = model.Title;
            blogToUpdate.Content = model.Content;
            blogToUpdate.Category = model.Category;
            blogToUpdate.ImgUrl = imagePath;
            blogToUpdate.UserId = currentUser.Id;
            blogToUpdate.Updated = DateTime.Now;
            blogToUpdate.Status = Enum.BlogStatus.Wait;

            await _context.SaveChangesAsync();
            TempData["success"] = "The blog has been edited successfully.";
            return RedirectToAction("Index", "Blog");
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

            
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);

               if(_context.SaveChanges() > 0)
                {
                    return Json(new { success = true, message = "The blog has been deleted successfully." });
                }
            }
            return Json(new { success = false, message = "Blog not found!!!" });
        }
    }
}
