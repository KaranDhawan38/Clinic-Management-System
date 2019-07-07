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
using log4net;
using Models.Enum;
using Logger;

namespace ApteanClinic.Controllers
{
    [OutputCache(Duration = 0)]
    [AuthenticateUser]
    public class DoctorsController : Controller
    {
        private ApteanClinicContext db = new ApteanClinicContext();
        public Doctor Doctor;
        private DoctorBusinessLayer doctorBusinessLayer;
        private UserBusinessLayer userBusinessLayer;

        private AppointmentViewModel appointmentViewModel;
        private PatientBusinessLayer patientBusinessLayer;
        private DoctorDetailsViewModel doctorDetailsViewModel;

        public DoctorsController()
        {
            userBusinessLayer = new UserBusinessLayer();
            appointmentViewModel = new AppointmentViewModel();
            doctorBusinessLayer = new DoctorBusinessLayer();
            patientBusinessLayer = new PatientBusinessLayer();
        }

        [AuthorizeUser(Roles = "Admin,Nurse")]
        // GET: Doctors
        public ActionResult Index()
        {
            var doctors = doctorBusinessLayer.GetAllDoctors();
            return View(doctors.ToList());
        }

        [HttpPost]
        public ActionResult GetPartial(string searchTerm = null)
        {

            var doctors = doctorBusinessLayer.GetAllDoctors();
            return View("Index", doctors);

        }
        [AuthorizeUser(Roles = "Admin,Nurse,Doctor")]
        [CheckId]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            doctorDetailsViewModel = new DoctorDetailsViewModel();
            doctorDetailsViewModel = doctorBusinessLayer.GetDoctorDetials((int)id);
            return View(doctorDetailsViewModel);
        }

        [AuthorizeUser(Roles = "Admin")]
        // GET: Doctors/Create
        public ActionResult Create()
        { 
            DoctorDetailsViewModel doctorDetailsViewModel = new DoctorDetailsViewModel();
            doctorDetailsViewModel.Doctor = new Doctor();
            doctorDetailsViewModel.Doctor.DoctorUser = new User();
            doctorDetailsViewModel.Doctor.ShiftTime = new List<DoctorTime>();
            doctorDetailsViewModel.Timings = new List<SelectListItem>();
            for (int i = 0; i < TimeSlots.Timings.Count; i++)
            {
                doctorDetailsViewModel.Timings.Add(
                    new SelectListItem
                    {
                        Text = TimeSlots.Timings[i],
                        Value = i.ToString(),
                        Selected = false
                    });
            }
            return View(doctorDetailsViewModel);
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorDetailsViewModel doctorDetailsViewModel)
        {
            doctorBusinessLayer = new DoctorBusinessLayer();
            userBusinessLayer = new UserBusinessLayer();
            var user = userBusinessLayer.GetUserByEmail(doctorDetailsViewModel.Doctor.DoctorUser.Email);
            if (user != null)
            {
                ModelState.AddModelError("DoctorUser.Email", "Email already exists");
                return View(doctorDetailsViewModel);
            }
            Doctor doctor = new Doctor();
            doctor.ShiftTime = new List<DoctorTime>();
            doctor = doctorDetailsViewModel.Doctor;
            doctorBusinessLayer.AddDoctor(doctor,doctorDetailsViewModel.Timings);

            Logging.loggInfo($"Doctor addedd with Userid = {doctor.DoctorUser.Id} and Name = {doctor.DoctorUser.Name} ");
            return RedirectToAction("Index");
        }

        // GET: Doctors/Edit/5
        [AuthorizeUser(Roles = "Admin,Doctor")]
        [CheckId]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Doctor doctor = doctorBusinessLayer.GetDoctorById((int)id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Doctor doctor)
        {
           // doctor.ShiftTime = doctorBusinessLayer.GetDoctorShiftTimeByDoctorId(doctor.Id);
            if (ModelState.IsValid)
            {
                doctorBusinessLayer.EditDoctorDetails(doctor);
                ViewBag.UserId = new SelectList(db.Users, "Id", "Name", doctor.DoctorUser.Id);
                return RedirectToAction("Details/" + doctor.Id);
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        [AuthorizeUser(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }

            Doctor doctor = doctorBusinessLayer.GetDoctorById((int)id);

            if (doctor == null)
            {
                Logging.loggError($"No Doctor found having Doctor id = {id}");
                return HttpNotFound();
            }
            
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Logging.loggInfo($"Doctor deleted haiving doctor Id = {id}");
            doctorBusinessLayer.DeleteDoctor(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [AuthorizeUser(Roles ="Admin,Doctor,Nurse")]
        
        public ActionResult GetDoctorAppointments(int docId, int appointments)
        {
            doctorBusinessLayer = new DoctorBusinessLayer();
            if (appointments == 0)
            {
                List<DoctorAppointment> doctorAppointments = doctorBusinessLayer.GetDoctorsUpcomingAppointments(docId);
                return Json(doctorAppointments, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<DoctorAppointment> doctorAppointments = doctorBusinessLayer.GetDoctorsAllAppointments(docId);
                return Json(doctorAppointments, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
