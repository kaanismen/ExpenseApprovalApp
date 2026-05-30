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

            var admin = await userManager.FindByEmailAsync("admin@expense.com");
            if (admin == null)
            {
                admin = new AppUser 
                {
                    FirstName = "Admin", 
                    LastName = "User", 
                    UserName = "admin@expense.com", 
                    Email = "admin@expense.com",
                    Department = "IT"
                };
                var result = await userManager.CreateAsync(admin, "Admin123!");
                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                await userManager.AddToRoleAsync(admin, "Admin");
            }


            var manager = await userManager.FindByEmailAsync("manager@expense.com");
            if (manager == null)
            {
                manager = new AppUser
                {
                    FirstName = "Manager",
                    LastName = "User",
                    UserName = "manager@expense.com",
                    Email = "manager@expense.com",
                    Department = "Finance"
                };
                var result = await userManager.CreateAsync(manager, "Manager123!");
                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                await userManager.AddToRoleAsync(manager, "Manager");
            }


            var employee = await userManager.FindByEmailAsync("employee@expense.com");
            if (employee == null)
            {
                employee = new AppUser
                {
                    FirstName = "Employee",
                    LastName = "User",
                    UserName = "employee@expense.com",
                    Email = "employee@expense.com",
                    Department = "Finance"
                };
                var result = await userManager.CreateAsync(employee, "Employee123!");
                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                await userManager.AddToRoleAsync(employee, "Employee");
            }        
        }

        internal static void InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
