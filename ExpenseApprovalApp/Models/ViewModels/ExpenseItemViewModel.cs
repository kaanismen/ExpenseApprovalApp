using ExpenseApprovalApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpenseApprovalApp.Models.ViewModels
{
    public class ExpenseItemViewModel
    {
        [Required(ErrorMessage = "The expense request must have a title.")]
        [StringLength(50, ErrorMessage = "Title must not exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Title must not exceed 100 characters.")]
        public string? Description { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
        public ExpenseCategory Category { get; set; }
    }
}
