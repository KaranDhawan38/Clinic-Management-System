using System.ComponentModel.DataAnnotations;

namespace ApteanClinic.Models
{
    public class Invoice
    {
       [Key]
        [Display(Name = "Invoice Id")]
        public int Id { get; set; }
        public int Appointment_Id{ get; set; }
        public float Total { get; set; }
       
    }
}
