using System;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApteanClinic.Tests.Controllers
{
    [TestClass]
    public class MedicalHistoryControllerTest
    {
        [TestMethod]
        public void TestGetAllMedicalHistories()
        {
            MedicalHistoriesBusinessLayer businessLayer = new MedicalHistoriesBusinessLayer();
            Assert.IsFalse(businessLayer.GetMedicalHistory(null).Count == 0);
        }
    }
}
