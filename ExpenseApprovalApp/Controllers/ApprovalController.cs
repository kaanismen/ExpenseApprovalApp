using ExpenseApprovalApp.Data;
using ExpenseApprovalApp.Models.Entities;
using ExpenseApprovalApp.Models.Enums;
using ExpenseApprovalApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalApp.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ApprovalController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public ApprovalController(UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var expenses = _dbContext.ExpenseRequests.Where(e => e.IssuedToId == user.Id && e.Status == ExpenseStatus.Pending).Include(e => e.Items);
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

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);
            var expenses = _dbContext.ExpenseRequests.Where(e => e.IssuedToId == user.Id && e.Status != ExpenseStatus.Pending).Include(e => e.Items);
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

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if ( user == null ) return Forbid();
            var request = await _dbContext.ExpenseRequests.FindAsync(id);
            if (request == null) return NotFound();
            if (request.IssuedToId != user.Id) return Forbid();
            request.Status = ExpenseStatus.Approved;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id, string managerComment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();
            var request = await _dbContext.ExpenseRequests.FindAsync(id);
            if (request == null) return NotFound();
            if (request.IssuedToId != user.Id) return Forbid();
            request.Status = ExpenseStatus.Rejected;
            request.ManagerComment = managerComment;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RevisionRequested(int id, string managerComment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();
            var request = await _dbContext.ExpenseRequests.FindAsync(id);
            if (request == null) return NotFound();
            if (request.IssuedToId != user.Id) return Forbid();
            request.Status = ExpenseStatus.RevisionRequested;
            request.ManagerComment = managerComment;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
