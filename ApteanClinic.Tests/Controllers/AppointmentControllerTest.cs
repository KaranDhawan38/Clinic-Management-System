using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ApteanClinic.BusinessLayer;
using ApteanClinic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enum;

namespace ApteanClinic.Tests.Controllers
{
    [TestClass]
    public class AppointmentControllerTest
    {
        [TestMethod]
        public void TestValidateAppointment()
        {
            AppointmentViewModel appointment = new AppointmentViewModel();
            appointment.DoctorId = 1;
            appointment.AppointmentDate = DateTime.Today;
            appointment.TimeSlot = TimeSlots.Timings[1];
            AppointmentBusinessLayer businessLayer = new AppointmentBusinessLayer();
            Assert.IsNull(businessLayer.ValidateAppointment(appointment));
        }
    }
}
