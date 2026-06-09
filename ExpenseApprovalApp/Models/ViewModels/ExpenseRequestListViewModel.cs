using ExpenseApprovalApp.Models.Entities;
using ExpenseApprovalApp.Models.Enums;

namespace ExpenseApprovalApp.Models.ViewModels
{
    public class ExpenseRequestListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public ExpenseStatus Status { get; set; }
        public DateTime Time { get; set; }
        public List<ExpenseItemViewModel> Expenses { get; set; }
        public string? ManagerComment { get; set; }
    }
}
