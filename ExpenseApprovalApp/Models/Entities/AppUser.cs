using Microsoft.AspNetCore.Identity;

namespace ExpenseApprovalApp.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }
}
