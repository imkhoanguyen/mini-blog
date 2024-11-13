using System.ComponentModel.DataAnnotations;

namespace mini_blog.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Username")]
        public required string UserName { get; set; }
        [Display(Name = "Full Name")]
        public required string FullName { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Display(Name = "Comfirm password")]
        [Compare("Password", ErrorMessage = "The password and comfirm password does not match")]
        [DataType(DataType.Password)]
        public required string ComfirmPassword { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
