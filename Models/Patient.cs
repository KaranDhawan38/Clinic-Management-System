using System.ComponentModel.DataAnnotations;

namespace ApteanClinic.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public User PatientUser { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string Address { get; set; }

        [Required]
        [Display(Name = "Emergency Contact Name")]
        public string EmergengyContactName { get; set; }

        [Required]
        [Display(Name = "Emergency Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 10, MinimumLength = 10)]
        public string EmergencyContactNumber { get; set; }


    }
}