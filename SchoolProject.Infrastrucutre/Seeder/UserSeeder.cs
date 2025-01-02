using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var user = new User
                {
                    UserName = "admin@dotnet.com",
                    Email = "admin@dotnet.com",
                    FullName = "Project Admin",
                    Country = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                await _userManager.CreateAsync(user, "user123");

                await _userManager.AddToRoleAsync(user, "Admin");

            }
        }
    }
}
