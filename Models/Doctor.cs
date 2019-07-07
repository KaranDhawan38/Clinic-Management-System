using ApteanClinic.Models.Enum;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ApteanClinic.Models
{
    public class Doctor  
    {
        public int Id { get; set; }
        public User DoctorUser { get; set; }
        public Speciality Speciality { get; set; }
        public Double Fees { get; set; }

        public List<DoctorTime> ShiftTime { set; get; }
    }


}