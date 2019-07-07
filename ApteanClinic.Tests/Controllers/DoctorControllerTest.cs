using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ApteanClinic.BusinessLayer;
using ApteanClinic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApteanClinic.Tests.Controllers
{
    [TestClass]
    public class DoctorControllerTest
    {
        [TestMethod]
        public void TestAddDoctor()
        {
            Doctor doctor = new Doctor();
            doctor.DoctorUser = new User();
            doctor.DoctorUser.Name = "Test";
            doctor.DoctorUser.Password = doctor.DoctorUser.ConfirmPassword = "Test1234";
            doctor.DoctorUser.Email = "test@gmail.com";
            doctor.DoctorUser.BloodGroup = Models.Enum.BloodGroup.AB;
            doctor.DoctorUser.Gender = Models.Enum.Gender.Female;
            doctor.Speciality = Models.Enum.Speciality.ENT;
            doctor.Fees = 600;
            doctor.DoctorUser.Contact = "7232392398";
            List<SelectListItem> timings = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Selected = true }
            };
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            businessLayer.AddDoctor(doctor, timings);
            Assert.AreEqual("Test", businessLayer.GetDoctorNameById(doctor.Id));
        }

        [TestMethod]
        public void TestAvailableTimeSlots()
        {
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            Doctor doctor = businessLayer.GetDoctorByEmail("test@gmail.com");
            Assert.AreEqual(1, businessLayer.GetDoctorAvailableTimeSlots(doctor.Id.ToString(), DateTime.Today.ToString()).Count);
        }

        [TestMethod]
        public void TestDoctorAppointments()
        {
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            Doctor doctor = businessLayer.GetDoctorByEmail("test@gmail.com");
            Assert.IsFalse(businessLayer.GetDoctorAppointments(doctor.Id).Count != 0);
        }
        
        [TestMethod]
        public void TestGetAllDoctors()
        {
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            Assert.IsTrue(businessLayer.GetAllDoctors().Count > 0);
        }

        [TestMethod]
        public void TestGetDoctorBySpeciality()
        {
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            Assert.IsNotNull(businessLayer.GetDoctorsBySpeciality("ENT"));
        }

        [TestMethod]
        public void TestGetDoctorDetials()
        {
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            Doctor doctor = businessLayer.GetDoctorByEmail("test@gmail.com");
            Assert.AreEqual(600, businessLayer.GetDoctorDetials(doctor.Id).Doctor.Fees);
        }

        [TestMethod]
        public void TestDoctorAppointmentList()
        {
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            Doctor doctor = businessLayer.GetDoctorByEmail("test@gmail.com");
            Assert.IsTrue(businessLayer.GetDoctorsAllAppointments(doctor.Id).Count == 0);
            Assert.IsTrue(businessLayer.AppointmentList(doctor.Id).Count == 0);
            Assert.IsTrue(businessLayer.GetDoctorsUpcomingAppointments(doctor.Id).Count == 0);
        }

        [TestMethod]
        public void TestEditDetails()
        {
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            Doctor doctor = businessLayer.GetDoctorByEmail("test@gmail.com");
            doctor.DoctorUser.Name = "TestEdit";
            businessLayer.EditDoctorDetails(doctor);
            Assert.AreEqual("TestEdit", businessLayer.GetDoctorNameById(doctor.Id));
        }

        [TestMethod]
        public void TestDelete()
        {
            DoctorBusinessLayer businessLayer = new DoctorBusinessLayer();
            Doctor testDoctor = businessLayer.GetDoctorByEmail("test@gmail.com");
            businessLayer.DeleteDoctor(testDoctor.Id);
            Assert.IsTrue(businessLayer.GetDoctorById(testDoctor.Id) == null);
        }
    }
}
