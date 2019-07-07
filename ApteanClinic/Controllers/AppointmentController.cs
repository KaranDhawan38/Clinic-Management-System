using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApteanClinic.BusinessLayer;
using ApteanClinic.Filters;
using ApteanClinic.Models;
using Models.Enum;
using Logger;
namespace ApteanClinic.Controllers
{
    [AuthenticateUser]
    [OutputCache(Duration =0)]
    public class AppointmentController : Controller
    {
        private Appointment appointment;
        private AppointmentBusinessLayer appointmentBusinessLayer;
        private DoctorBusinessLayer doctorBusinessLayer;
        private PatientBusinessLayer patientBusinessLayer;
        private AppointmentListViewModel appointmentListViewModel;
        private static int patientId;


        public AppointmentController()
        {
            appointment = new Appointment();
            appointmentListViewModel = new AppointmentListViewModel();
            appointmentBusinessLayer = new AppointmentBusinessLayer();
            patientBusinessLayer = new PatientBusinessLayer();
            doctorBusinessLayer = new DoctorBusinessLayer();
        }

        // GET: Default
        [AuthorizeUser(Roles = "Nurse, Patient")]
       // [CheckId]
        public ActionResult Index(int? id)
        {
            try
            {
                patientId = (int)id;
                return View("Index");
                throw new Exception();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error400");
            }
        }


        [HttpPost]
        public ActionResult Index(AppointmentViewModel appointmentViewModel)
        {
            try
            {
                appointmentBusinessLayer = new AppointmentBusinessLayer();
                Dictionary<string, string> errorMsg = appointmentBusinessLayer.ValidateAppointment(appointmentViewModel);
                if (errorMsg != null)
                {
                    ModelState.AddModelError(errorMsg.ElementAt(0).Key, errorMsg.ElementAt(0).Value);
                    return View("Index");
                }
                appointmentViewModel.PatientId = patientId;
                Dictionary<string, string> errMsg = appointmentBusinessLayer.FixAppointment(appointmentViewModel);

                if (errMsg != null)
                {
                    ModelState.AddModelError(errMsg.ElementAt(0).Key, errMsg.ElementAt(0).Value);
                }
                else
                {
                    Logging.loggInfo($"Appointment is added for patient id {appointmentViewModel.PatientId} with Doctor ID {appointmentViewModel.DoctorId } on date {appointmentViewModel.AppointmentDate } at time {appointmentViewModel.TimeSlot }");
                    if ((int)Session["Role"] == 3)
                        return Redirect("/Patients/Details/" + patientId);
                    else
                        return Redirect("/Appointment/AppointmentList");
                }
                return new HttpNotFoundResult();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error");
            }
        }
        [AuthorizeUser(Roles = "Doctor, Patient")]
        [CheckId]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            appointment = appointmentBusinessLayer.GetAppointmentById(id);
            appointmentListViewModel.Id = appointment.Id;
            appointmentListViewModel.PatientName = patientBusinessLayer.GetPatientNameById(appointment.PatientId);
            appointmentListViewModel.DoctorName = doctorBusinessLayer.GetDoctorNameById(appointment.DoctorId);
            appointmentListViewModel.Date = appointment.Date.ToString().Split(' ')[0];
            appointmentListViewModel.Time = TimeSlots.Timings[appointment.Time];
            appointmentListViewModel.Status = appointment.Status;
            return View(appointmentListViewModel);
        }

        public ActionResult Edit(AppointmentListViewModel appointmentListViewModel)
        {
            int isAppointmentEdited = appointmentBusinessLayer.EditAppointment(appointmentListViewModel, (int)Session["Role"]);

            if (isAppointmentEdited != 0)
            {
                if ((int)Session["Role"] == 1)
                {
                    Logging.loggInfo($"Appointment  status id updated for appointment id { appointmentListViewModel.Id} having  patient id {appointmentListViewModel.PatientId} by Doctor as {appointmentListViewModel.Status}");
                    return Redirect("/Doctors/Details/" + isAppointmentEdited.ToString());
                }
                else if ((int)Session["Role"] == 3)
                {
                    Logging.loggInfo($"Appointment  status id updated for appointment id { appointmentListViewModel.Id} having  patient id {appointmentListViewModel.PatientId} by Patient as {appointmentListViewModel.Status}");
                    return RedirectToAction("/Appointmentlist/" + (int)Session["userid"]);
                }
                return new HttpNotFoundResult();
            }          
            else return new HttpNotFoundResult();
        }

        public ActionResult ShowDoctorData(string specVal)
        {
            var docList = doctorBusinessLayer.GetDoctorsBySpeciality(specVal).ToList();
            return Json(docList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowDoctorTime(string doctor, string date)
        {
            var DoctorTimeSlotList = doctorBusinessLayer.GetDoctorAvailableTimeSlots(doctor, date);
            return Json(DoctorTimeSlotList, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(Roles ="Nurse,Patient,Doctor")]
        [CheckId]
        public ActionResult AppointmentList(int? id)
        {
            return View(appointmentBusinessLayer.GetAppointmentListView(id));
        }


        public void ChangeAppointmentStatus(int appointmentId)
        {
            Boolean isStatausChanged = appointmentBusinessLayer.ChangeAppointmentStatus(appointmentId);
        }
    }
}