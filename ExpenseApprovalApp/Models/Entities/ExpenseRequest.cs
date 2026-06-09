using ExpenseApprovalApp.Models.Enums;

namespace ExpenseApprovalApp.Models.Entities
{
    public class ExpenseRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public ExpenseStatus Status { get; set; }
        public DateTime Date { get; set; }
        public AppUser IssuedBy { get; set; }
        public string IssuedById { get; set; }
        public AppUser IssuedTo { get; set; }
        public string IssuedToId { get; set; }
        public List<ExpenseItem> Items { get; set; }

        public string? ManagerComment { get; set; }


    }
}
