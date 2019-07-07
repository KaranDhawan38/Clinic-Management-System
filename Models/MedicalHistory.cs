using System;
using System.ComponentModel.DataAnnotations;

namespace ApteanClinic.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Dignosis { get; set; }
        [DataType(DataType.MultilineText)]
        public string Medicine { get; set; }
        [DataType(DataType.MultilineText)]
        public string ClinicRemark { get; set; }
    }
    public class MedicalHistoryViewModel
    {
        [Display(Name = "Appointment Id")]
        public int AppointmentId { get; set; }

        [Display(Name = "Patient")]
        public string PatientName { get; set; }
        [Display(Name ="Doctor")]
        public string DoctorName { get; set; }
        public string Date { get; set; }
        public string Dignosis { get; set; }

        public string Medicine { get; set; }
        [Display(Name ="Clinic Remarks")]
        public string ClinicRemark { get; set; }

    }
}