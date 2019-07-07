using ApteanClinic.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApteanClinic.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 80, MinimumLength = 2)]
        public String Name { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Blood Group")]
        public BloodGroup BloodGroup { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 10, MinimumLength = 10)]
        public string Contact { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 16, MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$",ErrorMessage = "Passwords must contain at least 8 characters, including uppercase letters, lowercase letters, numbers and special characters.")]
        public String Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public String ConfirmPassword { get; set; }
        public Role Role { get; set; }
    }
}