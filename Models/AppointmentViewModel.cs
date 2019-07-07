using ApteanClinic.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ApteanClinic.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public Speciality Speciality { get; set; }
        public List<SelectListItem> Doctors { get; set; }
        public int DoctorId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Registred Date")]
        public DateTime AppointmentDate { get; set; }

        public string TimeSlot { get; set; }
        public int PatientId { get; set; }

        public AppointmentViewModel()
        {
            Doctors = new List<SelectListItem>();
        }
    }
}