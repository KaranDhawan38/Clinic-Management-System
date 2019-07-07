using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ApteanClinic.Models
{
    public class DoctorDetailsViewModel
    {
        public Doctor Doctor { set; get; }
        public List<AppointmentListViewModel> AppointmentList;
        public List<SelectListItem> Timings { set; get; }
        public DoctorDetailsViewModel()
        {
            AppointmentList = new List<AppointmentListViewModel>();
            //Doctor = new Doctor();
        }
    }
}
