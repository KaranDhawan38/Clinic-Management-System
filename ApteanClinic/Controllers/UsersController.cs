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

    public class UsersController : Controller
    {
        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["UserId"] == null)
                return View();
            else
                return Redirect("/HomePage/DashBoard");
        }

        // POST: Users/Create
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
            PatientBusinessLayer patientUser = new PatientBusinessLayer();
            if (!UserDataLayer.CheckEmail(patient.PatientUser.Email))
            {
                ModelState.AddModelError("PatientUser.Email", "Email already exists");
                Logging.loggError($"{patient.PatientUser.Email} is already exist as email");
                return View(patient);
            }
            patient.PatientUser.Role = Models.Enum.Role.Patient; 
            patientUser.AddPatient(patient);

            Logging.loggInfo($"Patient addedd with id = {patient.PatientUser.Id}");
            return Redirect("/Login/Index");
        }

    }
}
