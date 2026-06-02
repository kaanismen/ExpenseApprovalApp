using ExpenseApprovalApp.Data;
using ExpenseApprovalApp.Models.Entities;
using ExpenseApprovalApp.Models.Enums;
using ExpenseApprovalApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace ExpenseApprovalApp.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public ExpenseController(UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ExpenseRequestViewModel
            {
                Items = new List<ExpenseItemViewModel>()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var manager = await _userManager.FindByEmailAsync("manager@expense.com");
                var user = await _userManager.GetUserAsync(User);
                var request = new ExpenseRequest
                {
                    Title = model.Title,
                    IssuedById = user.Id,
                    Description = model.Description,
                    Status = ExpenseStatus.Pending,
                    Date = DateTime.Now,
                    IssuedToId = manager.Id,
                    Amount = model.Items.Sum(i => i.Amount),
                    Items = model.Items.Select(i => new ExpenseItem
                    {
                        Name = i.Name,
                        Description = i.Description,
                        Amount = i.Amount,
                        Category = i.Category
                    }).ToList()
                    
                };
                _dbContext.ExpenseRequests.Add(request);
                await _dbContext.SaveChangesAsync();

               

                return RedirectToAction("Index");
            }
            else { return View(model); }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var expenses = _dbContext.ExpenseRequests.Where(e => e.IssuedById == user.Id).Include(e => e.Items);
            var model = expenses.Select(e => new ExpenseRequestListViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                Status = e.Status,
                Time = e.Date,
                Expenses = e.Items.Select(i => new ExpenseItemViewModel
                {
                    Description = i.Description,
                    Amount = i.Amount,
                    Category = i.Category
                }).ToList()
            }).ToList();

            return View(model);
        }
    }
}