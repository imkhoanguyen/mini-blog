using System.ComponentModel.DataAnnotations;

namespace mini_blog.ViewModels
{
    public class LoginVM
    {
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
