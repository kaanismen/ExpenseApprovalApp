using System.ComponentModel.DataAnnotations;

namespace ExpenseApprovalApp.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
