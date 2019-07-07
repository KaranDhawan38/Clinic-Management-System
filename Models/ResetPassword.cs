using System.ComponentModel.DataAnnotations;

namespace ApteanClinic.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Old password field is required")]
        [DataType(DataType.Password)]
        [Display(Name = " Old Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "password field is required")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 16, MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Passwords must contain at least 8 characters, including uppercase letters, lowercase letters, numbers and special characters.")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password field is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Passwords do not match")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}