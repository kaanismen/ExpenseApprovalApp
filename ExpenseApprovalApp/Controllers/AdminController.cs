using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseApprovalApp.Data;
using ExpenseApprovalApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using ExpenseApprovalApp.Models.ViewModels;

namespace ExpenseApprovalApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public AdminController (UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserListViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();

                int requestCount = role == "Employee"
                    ? _dbContext.ExpenseRequests.Count(r => r.IssuedById == user.Id)
                    : role == "Manager"
                        ? _dbContext.ExpenseRequests.Count(r => r.IssuedToId == user.Id)
                        : 0;

                model.Add(new UserListViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Department = user.Department,
                    IsActive = user.IsActive,
                    Role = role,
                    RequestCount = requestCount
                });
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ToggleActive(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Users");
        }


    }
}
