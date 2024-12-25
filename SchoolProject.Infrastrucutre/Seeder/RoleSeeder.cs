using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> _roleManger)
        {
            var rolesCount = await _roleManger.Roles.CountAsync();
            if (rolesCount <= 0)
            {
                await _roleManger.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                await _roleManger.CreateAsync(new IdentityRole()
                {
                    Name = "User"
                });

            }
        }

    }
}
