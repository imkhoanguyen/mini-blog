using Microsoft.AspNetCore.Identity;
using mini_blog.Entities;

namespace mini_blog.DataAccess.Seed
{
    public class UserSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any()) return;


            var users = new List<AppUser>()
            {
                new AppUser
                {
                    FullName = "Admin",
                    UserName = "admin",
                    Email = "itk21sgu@gmail.com",
                    PhoneNumber = "0123456789",
                },
                new AppUser
                {
                    FullName = "Customer",
                    UserName = "customer",
                    Email = "khoasgu01@gmail.com",
                    PhoneNumber = "0987654321",
                },
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Admin_123");
            }

            var adminUser = await userManager.FindByNameAsync("Admin");

            await userManager.AddToRoleAsync(adminUser, "Admin");

            var customerUser = await userManager.FindByNameAsync("Customer");

            await userManager.AddToRoleAsync(customerUser, "Customer");
        }
    }
}
