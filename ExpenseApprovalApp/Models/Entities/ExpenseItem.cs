using ExpenseApprovalApp.Models.Enums;

namespace ExpenseApprovalApp.Models.Entities
{
    public class ExpenseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public ExpenseRequest ExpenseRequest { get; set; }
        public int ExpenseRequestId { get; set; }
        public ExpenseCategory Category { get; set; }
        

    }
}
