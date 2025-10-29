using Microsoft.AspNetCore.Identity;

namespace TooliRent.WebAPI.IdentitySeed
{
    public class IdentityDataSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            // initializing custom roles
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    Console.WriteLine($"Role '{roleName}' created");
                }
            }

            await CreateDefaultAdminUser(userManager);
        }

        private static async Task CreateDefaultAdminUser(UserManager<IdentityUser> userManager)
        {
            var adminEmail = "admin@toolirent.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new IdentityUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                    Console.WriteLine($"Default admin created: {adminEmail} / Admin123!");
                }
            }
        }
    }
}
