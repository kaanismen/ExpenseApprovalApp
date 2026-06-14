using ExpenseApprovalApp.Models.Enums;

namespace ExpenseApprovalApp.Models.ViewModels
{
    public class AdminRequestListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public ExpenseStatus Status { get; set; }
        public DateTime Time { get; set; }
        public string EmployeeName { get; set; }
        public string ManagerName { get; set; }
        public string Department { get; set; }
    }
}
