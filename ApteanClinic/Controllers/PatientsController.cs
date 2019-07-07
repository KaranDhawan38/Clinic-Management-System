using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApteanClinic.BusinessLayer;
using ApteanClinic.Database;
using ApteanClinic.Filters;
using ApteanClinic.Models;
using Logger;

namespace ApteanClinic.Controllers
{
    [OutputCache(Duration = 0)]
    [AuthenticateUser]
    public class PatientsController : Controller
    {
        private PatientBusinessLayer patientBusinessLayer = new PatientBusinessLayer();
        // GET: Patients

        [AuthorizeUser(Roles = "Admin,Doctor,Nurse")]
        public ActionResult Index()
        {
            var patients = patientBusinessLayer.GetAllPatients(Session["Role"].ToString(),int.Parse(Session["UserId"].ToString()));
            Logging.loggInfo("Showing list of all patients to user");
            return View(patients.ToList());
        }

        [CheckId]
        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Patient patient = patientBusinessLayer.GetPatientById((int)id);
            if (patient == null)
            {
                Logging.loggError($"No Patient found having Patient id = {id}");
                return Redirect("/Error/Index");
            }
            Logging.loggInfo($"Showing results of patient having patient id = {id} and Name = {patient.PatientUser.Name}");
            return View(patient);
        }

        [AuthorizeUser(Roles ="Admin,Nurse")]
        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }
            if(!UserDataLayer.CheckEmail(patient.PatientUser.Email))
            {
                ModelState.AddModelError("PatientUser.Email", "Email already exists");
                return View(patient);
            }
            patient.PatientUser.Role = Models.Enum.Role.Patient;
            patientBusinessLayer.AddPatient(patient);
            
            Logging.loggInfo($"Patient addedd with User id = {patient.PatientUser.Id} and Name = {patient.PatientUser.Name}");
            return RedirectToAction("Index");
        }

        // GET: Patients/Edit/5
        [AuthorizeUser(Roles ="Admin,Patient")]
        [CheckId]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Patient patient = patientBusinessLayer.GetPatientById((int)id);
            if (patient == null)
            {
                Logging.loggError($"No Patient found having Patient id = {id}");
                return Redirect("/Error/Index");
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patientBusinessLayer.UpdatePatientDetials(patient);
                Logging.loggInfo($"Patient Edited with id = {patient.Id}");
                return RedirectToAction("Details/" + patient.Id);
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        [AuthorizeUser(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Patient patient = patientBusinessLayer.GetPatientById((int)id);
            if (patient == null)
            {
                Logging.loggError($"No Patient found having Patient id = {id}");
                return Redirect("/Error/Index");
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = patientBusinessLayer.GetPatientById((int)id);
            patientBusinessLayer.RemovePatient(patient);
            Logging.loggInfo($"Patient delted having patient id = {id}");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                patientBusinessLayer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
