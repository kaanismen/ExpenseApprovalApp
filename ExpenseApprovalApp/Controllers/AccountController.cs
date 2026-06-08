using ExpenseApprovalApp.Models.Entities;
using ExpenseApprovalApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApprovalApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Employee"))
                    {
                        return RedirectToAction("Index", "Expense");
                    } else if (roles.Contains("Manager"))
                    {
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email veya şifre");
                    return View(model);
                }

            } 
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
