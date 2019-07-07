using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApteanClinic.Models
{
    public class InvoiceViewModel
    {
        public int id { get; set; }
        public int InvoiceId { get; set; }
        [Display(Name = "Name")]
        public string PatientName { get; set; }
        [Display(Name = "Medicine")]
        public string MedicineName { get; set; }
        public int Total { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Date")]
        public DateTime AppointmentDate { get; set; }
        [Display(Name = "Phone")]
        public string Phone_number { get; set; }
        public int Rate { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}
