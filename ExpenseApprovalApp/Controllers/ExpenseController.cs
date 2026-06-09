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
    [Authorize(Roles = "Employee")]
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
                
                var user = await _userManager.GetUserAsync(User);
                var managers = await _userManager.GetUsersInRoleAsync("Manager");
                var manager = managers.FirstOrDefault(m => m.Department == user.Department);
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
        public async Task<IActionResult> Edit(int id)
        {
            var request = await _dbContext.ExpenseRequests
                .Include(e => e.Items)
                .FirstOrDefaultAsync(e => e.Id == id);

            ViewBag.RequestId = id;

            var model = new ExpenseRequestViewModel
            {
                Title = request.Title,
                Description = request.Description,
                Items = request.Items.Select(i => new ExpenseItemViewModel
                {
                    Name = i.Name,
                    Description = i.Description,
                    Amount = i.Amount,
                    Category = i.Category
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ExpenseRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = await _dbContext.ExpenseRequests
                    .Include(e => e.Items)
                    .FirstOrDefaultAsync(e => e.Id == id);

                request.Title = model.Title;
                request.Date = DateTime.Now;
                request.Description = model.Description;
                request.Amount = model.Items.Sum(i => i.Amount);
                request.Status = ExpenseStatus.Pending;
                _dbContext.ExpenseItems.RemoveRange(request.Items);
                request.Items = model.Items.Select(i => new ExpenseItem
                {
                    Name = i.Name,
                    Description = i.Description,
                    Amount = i.Amount,
                    Category = i.Category
                }).ToList();
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
                ManagerComment = e.ManagerComment,
                Expenses = e.Items.Select(i => new ExpenseItemViewModel
                {
                    Name = i.Name,
                    Description = i.Description,
                    Amount = i.Amount,
                    Category = i.Category
                }).ToList()
            }).ToList();

            return View(model);
        }
    }
}