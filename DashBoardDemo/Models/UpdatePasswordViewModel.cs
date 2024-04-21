using System.ComponentModel.DataAnnotations;

namespace DashBoardDemo.Models
{
    public class UpdatePasswordViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a new password")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm the new password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
