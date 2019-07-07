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
    public class NurseController : Controller
    {
        private NurseBusinessLayer nurseBusinessLayer = new NurseBusinessLayer();

        [AuthorizeUser(Roles = "Admin")]
        // GET: Nurse
        public ActionResult Index()
        {
            Logging.loggInfo("Showing list of all nurses to admin");
            var nurses = nurseBusinessLayer.GetAllNurses();
            return View(nurses);
        }

        // GET: Nurse/Details/5
        [AuthorizeUser(Roles ="Admin,Nurse")]
        [CheckId]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Nurse nurse = nurseBusinessLayer.GetNurseById((int)id);
            if (nurse == null)
            {
                Logging.loggError($"No nurse found having nurse id = {id}");
                return Redirect("/Error/Index");
            }
            Logging.loggInfo($"Showing detials of nurse having id = {id} and Name = {nurse.NurseUser.Name}");
            return View(nurse);
        }

        [AuthorizeUser(Roles = "Admin")]
        // GET: Nurse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nurse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nurse nurse)
        {
            if (!ModelState.IsValid)
            {
                return View(nurse);
            }
            if (!UserDataLayer.CheckEmail(nurse.NurseUser.Email))
            {
                ModelState.AddModelError("NurseUser.Email", "Email already exists");
                return View(nurse);
            }
            nurse.NurseUser.Role = Models.Enum.Role.Nurse;
            nurseBusinessLayer.AddNurse(nurse);

            Logging.loggInfo($"Nurse addedd with id = {nurse.NurseUser.Id} and Name = {nurse.NurseUser.Name}");
            return RedirectToAction("Index");
        }

        // GET: Nurse/Edit/5
        [AuthorizeUser(Roles ="Admin,Nurse")]
        [CheckId]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Nurse nurse = nurseBusinessLayer.GetNurseById((int)id);
            if (nurse == null)
            {
                Logging.loggError($"No nurse found having nurse id = {id}");
                return Redirect("/Error/Index");
            }
            return View(nurse);
        }

        // POST: Nurse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                nurseBusinessLayer.UpdateNurseDetails(nurse);
                Logging.loggInfo($"Nurse Edited having id = {nurse.Id} and name = {nurse.NurseUser.Name}");
                return RedirectToAction("Details/" + nurse.Id);
            }
            return View(nurse);
        }

        // GET: Nurse/Delete/5
        [AuthorizeUser(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Nurse nurse = nurseBusinessLayer.GetNurseById((int)id);
            if (nurse == null)
            {
                Logging.loggError($"No nurse found having nurse id = {id}");
                return Redirect("/Error/Index");
            }
            return View(nurse);
        }

        // POST: Nurse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nurse nurse = nurseBusinessLayer.GetNurseById((int)id);
            nurseBusinessLayer.DeleteNurse(nurse);

            Logging.loggInfo($"Nurse Deleted having id = {nurse.Id} and name = {nurse.NurseUser.Name}");

         //   log.Info($"Nurse Edited having id = {id}");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                nurseBusinessLayer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
