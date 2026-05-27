using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ExpenseApprovalApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ExpenseRequest> ExpenseRequests { get; set; }
        public DbSet<ExpenseItem> ExpenseItems { get; set; }
    }
}
    