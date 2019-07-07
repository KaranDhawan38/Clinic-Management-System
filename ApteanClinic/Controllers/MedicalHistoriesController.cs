using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApteanClinic.Database;
using ApteanClinic.Models;
using ApteanClinic.BusinessLayer;
using BusinessLayer;
using log4net;
using ApteanClinic.Filters;
using Logger;
namespace ApteanClinic.Controllers
{
    [AuthenticateUser]
    [OutputCache(Duration = 0)]
    public class MedicalHistoriesController : Controller
    {
        private static int patientId;
        private static int? appointmentId = null;
        private MedicalHistoriesBusinessLayer medicalHistoriesBusinessLayer;

        // GET: MedicalHistories
        [AuthorizeUser(Roles = "Doctor, Nurse, Admin")]
        public ActionResult Index()
        {
            medicalHistoriesBusinessLayer = new MedicalHistoriesBusinessLayer();
            Logging.loggInfo($"Showing list of all medical histories");
            return View(medicalHistoriesBusinessLayer.GetMedicalHistory(null));

        }

        [AuthorizeUser(Roles = "Admin,Nurse,Doctor,Patient")]
        [CheckId]
        // GET: MedicalHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }

            medicalHistoriesBusinessLayer = new MedicalHistoriesBusinessLayer();
            var history = medicalHistoriesBusinessLayer.GetMedicalHistory(id);
            if (history == null)
            {
                Logging.loggError($"No Patient found having nurse id = {id}");
                return Redirect("/Error/Index");
            }
            Logging.loggInfo($"Showing Medical history of Patient having id = {id}");
            return View(history);

        }

        [AuthorizeUser(Roles = "Doctor,Nurse")]
        // GET: MedicalHistories/Create
        public ActionResult Create(int id, int? aid)
        {

            patientId = id;
            if (aid != null)
            {
                appointmentId = aid;
            }
            return View();
        }

        // POST: MedicalHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalHistory medicalHistory)
        {
            if (ModelState.IsValid)
            {
                MedicalHistoriesBusinessLayer businessLayer = new MedicalHistoriesBusinessLayer();
                if (appointmentId != null)
                {
                    medicalHistory.AppointmentId = (int)appointmentId;
                    AppointmentBusinessLayer appointmentBusinessLayer = new AppointmentBusinessLayer();
                    appointmentBusinessLayer.ChangeAppointmentStatus(medicalHistory.AppointmentId);
                }
                medicalHistory.PatientId = patientId;
                businessLayer.AddHistory(medicalHistory);
                Logging.loggInfo($"Medcial history added  of the patient having patient id = {medicalHistory.PatientId}");
                return Redirect("/Patients/Details/" + patientId);
            }
            return View(medicalHistory);
        }

        protected override void Dispose(bool disposing)
        {
            medicalHistoriesBusinessLayer = new MedicalHistoriesBusinessLayer();
            if (disposing)
            {
                medicalHistoriesBusinessLayer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
