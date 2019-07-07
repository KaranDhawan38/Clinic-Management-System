using ApteanClinic.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApteanClinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int NurseId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public int Time { get; set; }
        public AppointmentStatus Status { get; set; }

    }
}