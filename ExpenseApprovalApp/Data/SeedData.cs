using ExpenseApprovalApp.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExpenseApprovalApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Manager", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
