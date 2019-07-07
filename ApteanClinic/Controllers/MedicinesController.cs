using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApteanClinic.Database;
using ApteanClinic.Filters;
using ApteanClinic.Models;
using BusinessLayer;
using Logger;

namespace ApteanClinic.Controllers
{
    [OutputCache(Duration = 0)]
    [AuthenticateUser]
    public class MedicinesController : Controller
    {
        private MedicineBusinessLayer medicineBusinessLayer = new MedicineBusinessLayer();

        // GET: Medicines
        [AuthorizeUser(Roles ="Admin")]
        public ActionResult Index()
        {
            var medicine = medicineBusinessLayer.GetAllMedicines();
            Logging.loggInfo("Showing list of all medicines");
            return View(medicine);
        }

        // GET: Medicines/Details/5
        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Medicine medicine = medicineBusinessLayer.GetMedicineById(id);
            if (medicine == null)
            {
                Logging.loggInfo($"No medicine found having id = {id}");
                return HttpNotFound();
               
            }
            return View(medicine);
        }

        // GET: Medicines/Create
        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
       
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                medicineBusinessLayer.AddMedicine(medicine);
                Logging.loggInfo($"{medicine.Name} is added in database as Medicine having id = {medicine.Id} and cost = {medicine.Cost}");
                return RedirectToAction("Index");
            }

            return View(medicine);
        }

        // GET: Medicines/Edit/5
        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Medicine medicine = medicineBusinessLayer.GetMedicineById(id);
            if (medicine == null)
            {
                Logging.loggError($"No medicine round having id = {medicine.Id}");
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                medicineBusinessLayer.UpdateMedicine(medicine);
                Logging.loggInfo($"{medicine.Name} Medicine modified having medicine id = {medicine.Id} and its current cost is {medicine.Cost}");
                return RedirectToAction("Index");
            }
            return View(medicine);
        }

        // GET: Medicines/Delete/5
        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index?error=400");
            }
            Medicine medicine = medicineBusinessLayer.GetMedicineById(id);
            if (medicine == null)
            {
                Logging.loggError($"No medicine found having id = {id}");
                return Redirect("/Error/Index");
            }
            return View(medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicine medicine = medicineBusinessLayer.GetMedicineById(id);
            medicineBusinessLayer.DeleteMedicineById(medicine);
            Logging.loggInfo($"Medicine Deleted having id = {medicine.Id}");
            return RedirectToAction("Index");
        }

    }
}
