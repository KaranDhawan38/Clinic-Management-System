using System;
using System.ComponentModel.DataAnnotations;

namespace ApteanClinic.Models
{
    public class ForgotPassword
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 10, MinimumLength = 10)]
        public String MobileNumber { get; set; }

        [Required]
        [Range(100000, 999999, ErrorMessage = "Invalid OTP")]
        public int OTP { get; set; }

    }
}