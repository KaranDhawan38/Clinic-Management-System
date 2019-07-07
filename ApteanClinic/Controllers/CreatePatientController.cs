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
using ApteanClinic.Models;

namespace ApteanClinic.Controllers
{
    public class CreatePatientController : Controller
    {
        private ApteanClinicContext db = new ApteanClinicContext();

        // GET: CreatePatient
        public ActionResult Index()
        {
            return View(db.CreatePatientViewModels.ToList());
        }

        // GET: CreatePatient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreatePatientViewModel createPatientViewModel = db.CreatePatientViewModels.Find(id);
            if (createPatientViewModel == null)
            {
                return HttpNotFound();
            }
            return View(createPatientViewModel);
        }

        // GET: CreatePatient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreatePatient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Gender,BloodGroup,Contact,Password")] CreatePatientViewModel createPatientViewModel)
        {
            if (ModelState.IsValid)
            {
                PatientBusinessLayer createPatient = new PatientBusinessLayer();
                createPatient.CreatePatient(createPatientViewModel);
                return RedirectToAction("Index");
            }

            return View(createPatientViewModel);
        }

        // GET: CreatePatient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreatePatientViewModel createPatientViewModel = db.CreatePatientViewModels.Find(id);
            if (createPatientViewModel == null)
            {
                return HttpNotFound();
            }
            return View(createPatientViewModel);
        }

        // POST: CreatePatient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,BloodGroup,Contact,Password")] CreatePatientViewModel createPatientViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(createPatientViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(createPatientViewModel);
        }

        // GET: CreatePatient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreatePatientViewModel createPatientViewModel = db.CreatePatientViewModels.Find(id);
            if (createPatientViewModel == null)
            {
                return HttpNotFound();
            }
            return View(createPatientViewModel);
        }

        // POST: CreatePatient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreatePatientViewModel createPatientViewModel = db.CreatePatientViewModels.Find(id);
            db.CreatePatientViewModels.Remove(createPatientViewModel);
            db.SaveChanges();
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
    }
}
