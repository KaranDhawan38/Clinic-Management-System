using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApteanClinic.Models
{
    public class DashboardModel
    {
        public int totalPatients { get; set; }
        public int totalAppointments { get; set; }
        public int totalAppointmentsToday { get; set; }
        public int totalDoctors { get; set; }
        public int totalUsers { get; set; }

    }
}
