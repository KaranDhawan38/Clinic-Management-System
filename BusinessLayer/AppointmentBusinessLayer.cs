using ApteanClinic.Database;
using ApteanClinic.Models;
using ApteanClinic.Models.Enum;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ExceptionHandler = ApteanClinic.Database.ExceptionHandler;

namespace ApteanClinic.BusinessLayer
{
    public class AppointmentBusinessLayer
    {
        private AppointmentDataLayer appointmentDataLayer;
        private Appointment appointment;
        private NurseBusinessLayer nurseBusinessLayer;
        private DoctorBusinessLayer doctorBusinessLayer;
        private PatientDataLayer patientDataLayer;
        private AppointmentBusinessLayer appointmentBusinessLayer;
        private PatientBusinessLayer patientBusinessLayer;
        private AppointmentListViewModel appointmentListViewModel;
        private List<AppointmentListViewModel> appointmentListViewModelList;

        public AppointmentBusinessLayer()
        {
            patientBusinessLayer = new PatientBusinessLayer();
            doctorBusinessLayer = new DoctorBusinessLayer();
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                appointmentDataLayer = new AppointmentDataLayer();
                return appointmentDataLayer.GetAppointmentsById(appointmentId);
            }catch(Exception e)
            {
                Database.ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Dictionary<string,string> ValidateAppointment(AppointmentViewModel appointmentViewModel)
        {
            try
            {
                Dictionary<string, string> errorMsg = null;
                if (appointmentViewModel.DoctorId == 0)
                {
                    errorMsg = new Dictionary<string, string>();
                    errorMsg.Add("DoctorId", "Doctor not available");
                    return errorMsg;
                }
                if (appointmentViewModel.AppointmentDate.ToString().Split(' ')[0] == "1/1/0001")
                {
                    errorMsg = new Dictionary<string, string>();
                    errorMsg.Add("AppointmentDate", "Appointment Date is required");
                    return errorMsg;
                }
                if (appointmentViewModel.TimeSlot == null)
                {
                    errorMsg = new Dictionary<string, string>();
                    errorMsg.Add("TimeSlot", "Please select a time slot or any other date.");
                    return errorMsg;
                }
                return errorMsg;
            }
            catch (Exception e)
            {
                Database.ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Dictionary<string, string> FixAppointment(AppointmentViewModel appointmentViewModel)
        {
            try
            {
                appointment = new Appointment();
                nurseBusinessLayer = new NurseBusinessLayer();
                doctorBusinessLayer = new DoctorBusinessLayer();
                patientDataLayer = new PatientDataLayer();

                appointmentViewModel.TimeSlot = TimeSlots.Timings.IndexOf(doctorBusinessLayer.GetDoctorAvailableTimeSlots(appointmentViewModel.DoctorId.ToString(), appointmentViewModel.AppointmentDate.ToString())[int.Parse(appointmentViewModel.TimeSlot)]).ToString();
                int isAlreadyBooked = patientDataLayer.GetTotalAppointments(Convert.ToDateTime(appointmentViewModel.AppointmentDate), int.Parse(appointmentViewModel.TimeSlot), appointmentViewModel.DoctorId);
                if (isAlreadyBooked > 0)
                {
                    return new Dictionary<string, string>() { { "", "Time slot already booked." } };
                }

                int NurseId = nurseBusinessLayer.GetAvailableNurse(Convert.ToDateTime(appointmentViewModel.AppointmentDate), TimeSlots.Timings.IndexOf(appointmentViewModel.TimeSlot));
                if (NurseId == -1)
                {
                    return new Dictionary<string, string>() { { "", "Appointment not booked, Please contact the clinic." } };
                }

                appointment.NurseId = NurseId;
                appointment.DoctorId = appointmentViewModel.DoctorId;
                appointment.PatientId = appointmentViewModel.PatientId;
                appointment.Date = Convert.ToDateTime(appointmentViewModel.AppointmentDate);
                appointment.Time = int.Parse(appointmentViewModel.TimeSlot);
                appointment.Status = AppointmentStatus.Pending;
                appointment = patientDataLayer.FixAppointment(appointment);
                return null;
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw;
            }
        }

        public int EditAppointment(AppointmentListViewModel appointmentListViewModel,int Role)
        {
            try
            {
                appointmentBusinessLayer = new AppointmentBusinessLayer();
                patientBusinessLayer = new PatientBusinessLayer();
                appointment = appointmentBusinessLayer.GetAppointmentById(appointmentListViewModel.Id);
                if (Role == 1)
                {
                    appointment.Status = appointmentListViewModel.Status;
                    appointment = patientBusinessLayer.UpdateAppointment(appointment);
                }
                else if (Role == 3)
                {
                    appointment.Status = (appointmentListViewModel.isCancelled) ? AppointmentStatus.Cancelled : AppointmentStatus.Pending;
                    appointment = patientBusinessLayer.UpdateAppointment(appointment);
                }
                return appointment.DoctorId;
            }catch(Exception e)
            {
                Database.ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<AppointmentListViewModel> GetAppointmentListView(int? id)
        {
            try
            {
                if (id == null)
                {
                    id = -1;
                }
                List<Appointment> appointmentList = patientBusinessLayer.GetPatientAppointments((int)id);
                appointmentListViewModelList = new List<AppointmentListViewModel>();
                appointmentDataLayer = new AppointmentDataLayer();
                foreach (var appointment in appointmentList)
                {
                    MedicinesQuantity invoice = appointmentDataLayer.GetInvoiceByAppointmentId(appointment.Id);
                    appointmentListViewModel = new AppointmentListViewModel();
                    appointmentListViewModel.Id = appointment.Id;
                    appointmentListViewModel.PatientName = patientBusinessLayer.GetPatientNameById(appointment.PatientId);
                    appointmentListViewModel.DoctorName = doctorBusinessLayer.GetDoctorNameById(appointment.DoctorId);
                    appointmentListViewModel.Date = appointment.Date.ToString().Split(' ')[0];
                    if (appointment.Time == -1)
                    {
                        appointmentListViewModel.Time = "---";
                    }
                    else
                    {
                        appointmentListViewModel.Time = TimeSlots.Timings[appointment.Time];
                    }
                    appointmentListViewModel.Status = appointment.Status;
                    if (invoice != null)
                    {
                        appointmentListViewModel.showMedicineButton = false;
                        //appointmentListViewModelList.Add(appointmentListViewModel);
                    }
                    else if (invoice == null)
                    {
                        appointmentListViewModel.showMedicineButton = true;
                    }
                    appointmentListViewModelList.Add(appointmentListViewModel);
                }
                return appointmentListViewModelList;
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Boolean ChangeAppointmentStatus(int appointmentId)
        {
            try
            {
                appointmentBusinessLayer = new AppointmentBusinessLayer();
                appointment = appointmentBusinessLayer.GetAppointmentById(appointmentId);
                patientBusinessLayer = new PatientBusinessLayer();
                appointment.Status = AppointmentStatus.Closed;
                appointment = patientBusinessLayer.UpdateAppointment(appointment);
                return true;
            }catch(Exception e)
            {
                Database.ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void CancelPastAppointments()
        {
            try
            {
                appointmentDataLayer = new AppointmentDataLayer();
                var previousDay = DateTime.Today.AddDays(-1);
                appointmentDataLayer.GetPastAppointments(previousDay);
                //for (int i=0;i<appointments.Count;i++)
                //{
                //    if(appointments[i].Status == AppointmentStatus.Pending || appointments[i].Status == AppointmentStatus.Approved)
                //    {
                //        appointments[i].Status = AppointmentStatus.Cancelled;
                //    }
                //}
                //appointmentDataLayer.UpdateAppointments(appointments);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
    }
}
