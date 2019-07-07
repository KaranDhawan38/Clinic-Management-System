using System;
using ApteanClinic.BusinessLayer;
using ApteanClinic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApteanClinic.Tests.Controllers
{
    [TestClass]
    public class PatientControllerTest
    {
        [TestMethod]
        public void TestAddPatient()
        {
            Patient patient = new Patient();
            patient.PatientUser = new User();
            patient.PatientUser.Name = "Test";
            patient.PatientUser.Password = patient.PatientUser.ConfirmPassword = "Test1234";
            patient.PatientUser.Email = "testpatient@gmail.com";
            patient.PatientUser.BloodGroup = Models.Enum.BloodGroup.B;
            patient.PatientUser.Gender = Models.Enum.Gender.Male;
            patient.PatientUser.Contact = "7232392398";
            patient.Height = 6;
            patient.Weight = 73;
            patient.Address = "#123 Panchkula";
            patient.EmergencyContactNumber = "8124393000";
            patient.EmergengyContactName = "TestFather";
            PatientBusinessLayer businessLayer = new PatientBusinessLayer();
            businessLayer.AddPatient(patient);
            Assert.AreEqual("Test", businessLayer.GetPatientNameById(patient.Id));
        }

        [TestMethod]
        public void TestEditDetails()
        {
            PatientBusinessLayer businessLayer = new PatientBusinessLayer();
            Patient patient = businessLayer.GetPatientByEmail("testpatient@gmail.com");
            patient.PatientUser.Name = "TestEdit";
            patient.EmergengyContactName = "TestMother";
            businessLayer.UpdatePatientDetials(patient);
            Assert.AreEqual("TestEdit", businessLayer.GetPatientNameById(patient.Id));
            Assert.AreEqual("TestMother", businessLayer.GetPatientById(patient.Id).EmergengyContactName);
        }

        [TestMethod]
        public void TestDelete()
        {
            PatientBusinessLayer businessLayer = new PatientBusinessLayer();
            Patient testPatient = businessLayer.GetPatientByEmail("testpatient@gmail.com");
            businessLayer.RemovePatient(testPatient);
            Assert.IsTrue(businessLayer.GetPatientByEmail("testpatient@gmail.com") == null);
        }
    }
}
