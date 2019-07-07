using ApteanClinic.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApteanClinic.Models
{
    public class DoctorAppointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Date { get; set; }
        public string TimeSlot { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
