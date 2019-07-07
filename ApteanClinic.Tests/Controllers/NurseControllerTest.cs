using System;
using ApteanClinic.BusinessLayer;
using ApteanClinic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApteanClinic.Tests.Controllers
{
    [TestClass]
    public class NurseControllerTest
    {
        [TestMethod]
        public void TestAddNurse()
        {
            Nurse nurse = new Nurse();
            nurse.NurseUser = new User();
            nurse.NurseUser.Name = "Test";
            nurse.NurseUser.Password = nurse.NurseUser.ConfirmPassword = "Test1234";
            nurse.NurseUser.Email = "testnurse@gmail.com";
            nurse.NurseUser.BloodGroup = Models.Enum.BloodGroup.B;
            nurse.NurseUser.Gender = Models.Enum.Gender.Female;
            nurse.NurseUser.Contact = "7232392398";
            nurse.Availability = true;
            NurseBusinessLayer businessLayer = new NurseBusinessLayer();
            businessLayer.AddNurse(nurse);
            Assert.AreEqual("Test", businessLayer.GetNurseNameById(nurse.Id));
        }

        [TestMethod]
        public void TestEditDetails()
        {
            NurseBusinessLayer businessLayer = new NurseBusinessLayer();
            Nurse nurse = businessLayer.GetNurseByEmail("testnurse@gmail.com");
            nurse.NurseUser.Name = "TestEdit";
            businessLayer.UpdateNurseDetails(nurse);
            Assert.AreEqual("TestEdit", businessLayer.GetNurseNameById(nurse.Id));
        }

        [TestMethod]
        public void TestDelete()
        {
            NurseBusinessLayer businessLayer = new NurseBusinessLayer();
            Nurse testNurse = businessLayer.GetNurseByEmail("testnurse@gmail.com");
            businessLayer.DeleteNurse(testNurse);
            Assert.IsTrue(businessLayer.GetNurseByEmail("testnurse@gmail.com") == null);
        }
    }
}
