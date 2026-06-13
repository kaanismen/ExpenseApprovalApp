namespace ExpenseApprovalApp.Models.ViewModels
{
    public class UserListViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public int RequestCount { get; set; }
    }
}
