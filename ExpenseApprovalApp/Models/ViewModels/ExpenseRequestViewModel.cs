namespace ExpenseApprovalApp.Models.ViewModels
{
    public class ExpenseRequestViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ExpenseItemViewModel> Items { get; set; }
    }
}
