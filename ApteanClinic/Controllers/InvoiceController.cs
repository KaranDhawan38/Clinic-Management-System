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
    [AuthenticateUser]
    [OutputCache(Duration = 0)]
    public class InvoiceController : Controller
    {
        private ApteanClinicContext db = new ApteanClinicContext();
        private PatientBusinessLayer patientBusinessLayer = new PatientBusinessLayer();
        private List<InvoiceViewModel> invoiceListViewModel = new List<InvoiceViewModel>();
        Invoice invoice = new Invoice();
        MedicinesQuantity medicine;
        InvoiceViewModel invoicelist;
        private int Total = 0;
        private List<MedicinesQuantity> medicinelist = new List<MedicinesQuantity>();
        private int AppointmentId = 0;
        [AuthorizeUser(Roles = "Admin,Nurse,Doctor")]
        public ActionResult AddMedicines(int id)
        {
            AppointmentId = id;
            TempData["id"] = AppointmentId;
            medicine = new MedicinesQuantity();
            medicine = patientBusinessLayer.CheckIdInList(AppointmentId);

            if (medicine == null)
            {
                return View(db.Medicines.ToList());
            }

            else
            {
                return RedirectToAction("InvoiceList", "Invoice", new { AppointmentId = AppointmentId });
            }


        }

        [HttpPost]
        [AuthorizeUser(Roles = "Admin,Nurse,Doctor")]
        public ActionResult Save(FormCollection collection)
        {
            TempData["id"] = TempData["id"];
            AppointmentId = (int)TempData["id"];
            var check = collection["test"].Split(',');
            List<string> checkList = check.ToList();
            List<int> quant = collection["quantity"].Split(',').Select(int.Parse).ToList();
            List<int> Ids = collection["medicineId"].Split(',').Select(int.Parse).ToList();

            float sum = 0;
            for (int i = 0; i < Ids.Count; i++)
            {
                medicine = new MedicinesQuantity();
                if (quant[i] != 0)
                {

                    int MedicineID = Ids[i];
                    medicine.Medicine_Id = MedicineID;
                    medicine.MedicineRate = patientBusinessLayer.GetMedicineCost(MedicineID);
                    medicine.Appointment_Id = AppointmentId;
                    medicine.quantity = quant[i];
                    sum = sum + medicine.MedicineRate * quant[i];

                    medicinelist.Add(medicine);

                }

            }
            int fees = patientBusinessLayer.GetDoctorFeesByAppointmentId(AppointmentId);
            Total = Convert.ToInt32(sum + fees);
            ViewData["fees"] = fees;
            TempData["total"] = sum + fees;
            CreateInvoice();
            patientBusinessLayer.SaveData(medicinelist);
            return RedirectToAction("InvoiceList", "Invoice", new { AppointmentId = AppointmentId });



        }




        // GET: Invoice/Create
        [AuthorizeUser(Roles = "Admin,Nurse,Doctor")]
        public ActionResult CreateInvoice()
        {

            invoice.Appointment_Id = Convert.ToInt32(TempData["id"]);
            invoice.Total = (float)TempData["total"];
            Create(invoice);
            return RedirectToAction("Index", "Invoice");
        }

        // POST: Invoice/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Roles = "Admin,Nurse,Doctor")]
        public void Create(Invoice invoice)
        {

            if (ModelState.IsValid)
            {
                patientBusinessLayer.createInvoice(invoice);
                
            }

          
        }




        /// GET: Invoice/Delete/5
         [AuthorizeUser(Roles = "Admin")]
        public ActionResult Delete(int InvoiceId=0)
        {
            if (InvoiceId == 0)
            {
                return Redirect("/Error/Index?error=400");
            }
            else
            {

                invoice = patientBusinessLayer.GetInvoiceByInvoiceId(InvoiceId);
            }
            return View(invoice);
        }

        // POST: Invoice/Delete/5
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int InvoiceId = 0)
        {
          
            patientBusinessLayer.DeleteInvoice(InvoiceId);
            return RedirectToAction("ManageInvoices",Session["PatientId"]);
        }
        public List<InvoiceViewModel> FetchInvoicesByAppointmentId(List<Appointment> appointments)
        {
            foreach (var appointment in appointments)
            {
                Invoice invoice = patientBusinessLayer.GetInvoice(appointment.Id);
                if (invoice != null)
                {
                    invoicelist = new InvoiceViewModel();
                    invoicelist.InvoiceId = invoice.Id;
                    invoicelist.PatientName = patientBusinessLayer.GetPatientNameByAppointmentId(appointment.Id);
                    invoicelist.AppointmentDate = Convert.ToDateTime(patientBusinessLayer.GetAppointmentDate(appointment.Id));
                    invoicelist.Total = patientBusinessLayer.GetTotalBill(appointment.Id);
                    invoiceListViewModel.Add(invoicelist);
                }


            }
            return invoiceListViewModel;
        }

        public ActionResult ManageInvoices(int id = 0)
        {
            int InvoiceCounter = patientBusinessLayer.GetNumberOfInvoices();
            invoiceListViewModel = new List<InvoiceViewModel>();
            if (InvoiceCounter != 0)
            {
                if (id != 0)
                {
                    int PatientId = (int)id;
                    List<Appointment> appointments = patientBusinessLayer.GetPatientAppointments(PatientId);
                    invoiceListViewModel= FetchInvoicesByAppointmentId(appointments);
                  
                    return View(invoiceListViewModel);
                }
                else
                {
                    List<Appointment> appointments = patientBusinessLayer.GetAppointments();

                    invoiceListViewModel = FetchInvoicesByAppointmentId(appointments);
                    return View(invoiceListViewModel);
                }
            }
            return View(invoiceListViewModel);


        }

        public ActionResult GetAppointmentDetailsByInvoiceId(int InvoiceId)
        {
            int AppointmentId = patientBusinessLayer.GetAppointmentDetailsByInvoiceId(InvoiceId);
            return RedirectToAction("InvoiceList", "Invoice", new { AppointmentId = AppointmentId });

        }
        public ActionResult InvoiceList(int AppointmentId)
        {

            ViewData["fees"] = patientBusinessLayer.GetDoctorFeesByAppointmentId(AppointmentId);
            invoiceListViewModel = new List<InvoiceViewModel>();
            List<MedicinesQuantity> medicinelist = patientBusinessLayer.GetPatientMedicines(AppointmentId);

            foreach (var medlist in medicinelist)
            {

                invoicelist = new InvoiceViewModel();
                invoicelist.MedicineName = patientBusinessLayer.GetMedicineName(medlist.Medicine_Id);
                invoicelist.Rate = Convert.ToInt32(medlist.MedicineRate);
                invoicelist.Quantity = medlist.quantity;
                invoicelist.PatientName = patientBusinessLayer.GetPatientNameByAppointmentId(medlist.Appointment_Id);
                invoicelist.AppointmentDate = Convert.ToDateTime(patientBusinessLayer.GetAppointmentDate(medlist.Appointment_Id));
                invoicelist.Phone_number = patientBusinessLayer.GetPhoneNumber(medlist.Appointment_Id);
                invoicelist.Price = Convert.ToInt32(medlist.MedicineRate) * medlist.quantity;
                invoiceListViewModel.Add(invoicelist);


            }

            ViewData["TotalBill"] = patientBusinessLayer.GetTotalBill(AppointmentId);
            return View(invoiceListViewModel);
        }





    }
}
