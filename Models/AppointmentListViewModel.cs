using ApteanClinic.Models;
using ApteanClinic.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ApteanClinic.Models
{
    public class AppointmentListViewModel
    {
        [Display(Name = "Index")]
        public int Id { get; set; }

        public int PatientId { get; set; }
        [Display(Name = "Name")]
        public string PatientName { get; set; }
        [Display(Name = "Doctor")]
        public string DoctorName { get; set; }
        [Display(Name = "Date")]
        public string Date { get; set; }
        [Display(Name = "Time Slot")]
        public string Time { get; set; }
        [Display(Name = "Status")]
        public AppointmentStatus Status { get; set; }

        public List<SelectListItem> StatusList;
        public AppointmentListViewModel()
        {
            StatusList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = AppointmentStatus.Pending.ToString(),
                    Text = AppointmentStatus.Pending.ToString(),
                    Selected = true
                },
                 new SelectListItem()
                {
                    Value = AppointmentStatus.Approved.ToString(),
                    Text = AppointmentStatus.Approved.ToString(),
                    Selected = false
                }
            };
        }
        public bool isCancelled { get; set; }
        public bool showMedicineButton = false;
    }
}
