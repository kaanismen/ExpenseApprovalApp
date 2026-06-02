using ExpenseApprovalApp.Data;
using ExpenseApprovalApp.Models.Entities;
using ExpenseApprovalApp.Models.Enums;
using ExpenseApprovalApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace ExpenseApprovalApp.Controllers
{
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
                var request = new ExpenseRequest
                {
                    Title = model.Title,
                    IssuedById = user.Id,
                    Description = model.Description,
                    Status = ExpenseStatus.Pending,
                    Date = DateTime.Now,
                    Amount = 0
                };
                _dbContext.ExpenseRequests.Add(request);
                await _dbContext.SaveChangesAsync();

                foreach (var item in model.Items)
                {
                    var expenseItem = new ExpenseItem
                    {
                        Description = item.Description,
                        Name = item.Name,
                        Amount = item.Amount,
                        Category = item.Category,
                        ExpenseRequestId = request.Id
                    };
                    _dbContext.ExpenseItems.Add(expenseItem);
                    request.Amount += item.Amount;

                }
                _dbContext.ExpenseRequests.Update(request);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else { return View(model); }
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User);
            }
        }
    }
}