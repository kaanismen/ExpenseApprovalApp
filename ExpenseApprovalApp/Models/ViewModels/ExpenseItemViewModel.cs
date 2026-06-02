using ExpenseApprovalApp.Models.Enums;

namespace ExpenseApprovalApp.Models.ViewModels
{
    public class ExpenseItemViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public ExpenseCategory Category { get; set; }
    }
}
