using System.ComponentModel.DataAnnotations;

namespace ExpenseApprovalApp.Models.ViewModels
{
    public class ExpenseRequestViewModel
    {
        [Required(ErrorMessage = "The expense request must have a title.")]
        [StringLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
        public string Title { get; set; }
        [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
        public string? Description { get; set; }
        public List<ExpenseItemViewModel> Items { get; set; }
    }
}
