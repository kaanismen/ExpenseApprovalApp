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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExpenseRequest>()
                .HasOne(e => e.IssuedBy)
                .WithMany()
                .HasForeignKey(e => e.IssuedById)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ExpenseRequest>()
                .HasOne(e => e.IssuedTo)
                .WithMany()
                .HasForeignKey(e => e.IssuedToId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
    