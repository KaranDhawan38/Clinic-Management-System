using ApteanClinic.Database;
using ApteanClinic.Models;
using ApteanClinic.Models.Enum;
using BusinessLayer;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApteanClinic.BusinessLayer
{
    public class DoctorBusinessLayer
    {
        private DoctorDataLayer doctorDataLayer;
        private DoctorDetailsViewModel doctorDetailsViewModel;
        private List<AppointmentListViewModel> appointmentListViewModelList;
        private AppointmentListViewModel appointmentListViewModel;
        private PatientBusinessLayer patientBusinessLayer;
        private PatientDataLayer patientDataLayer;

        //private List<SelectListItem> availableTimings;
        public DoctorBusinessLayer()
        {
            doctorDataLayer = new DoctorDataLayer();
        }

        public void AddDoctor(Doctor doctor,List<SelectListItem> Timings)
        {
            try
            {
                doctor.ShiftTime = new List<DoctorTime>();
                foreach (SelectListItem time in Timings)
                {
                    DoctorTime doctorTime = new DoctorTime();
                    if (time.Selected)
                    {
                        doctorTime.TimeId = int.Parse(time.Value);
                        doctorTime.Doctor = doctor;
                        doctor.ShiftTime.Add(doctorTime);
                    }
                }
                doctor.DoctorUser.Role = Role.Doctor;
                doctor.DoctorUser.Password = doctor.DoctorUser.ConfirmPassword = Encryption.Encrypt(doctor.DoctorUser.Password);
                doctorDataLayer.AddDoctor(doctor);
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Doctor GetDoctorByEmail(string email)
        {
            try
            {
                return doctorDataLayer.GetDoctorByEmail(email);
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public string GetDoctorNameById(int id)
        {
            try
            {
                return doctorDataLayer.GetDoctorNameById(id);
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Doctor> GetDoctorsBySpeciality(string spec)
        {
            try
            {
                Speciality speciality = (Speciality)Enum.Parse(typeof(Speciality), spec);
                return doctorDataLayer.GetDoctorsBySpeciality(speciality);
            }catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Doctor GetDoctorById(int id)
        {
            try
            {
                var doctor = doctorDataLayer.GetDoctorById(id);
                return doctor;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<string> GetDoctorAvailableTimeSlots(string doctorId, string dateTime)
        {
            try { 
                int docId = int.Parse(doctorId);
                var totalSlots = doctorDataLayer.GetDoctorTimeSlot(docId);
                List<string> availableSlots = new List<string>();
                var appointments = doctorDataLayer.GetDocotorBookedSlots(docId,Convert.ToDateTime(dateTime));
                List<DoctorTime> availableTimeSlots = new List<DoctorTime>();
                foreach (var slot in totalSlots)
                {
                    if(appointments.IndexOf(slot.TimeId) == -1)
                    {
                        availableSlots.Add(TimeSlots.Timings[slot.TimeId]);
                    }
                }
                return availableSlots;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public DoctorDetailsViewModel GetDoctorDetials(int doctorId)
        {
            try {
            doctorDetailsViewModel = new DoctorDetailsViewModel();
            doctorDetailsViewModel.Doctor = GetDoctorById(doctorId);
            doctorDetailsViewModel.AppointmentList = AppointmentList(doctorId);
            return doctorDetailsViewModel;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Appointment> GetDoctorAppointments(int doctorId)
        {
            try { 
            doctorDataLayer = new DoctorDataLayer();
            return doctorDataLayer.GetDoctorAppointments(doctorId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<AppointmentListViewModel> AppointmentList(int doctorId)
        {
            try { 
            patientBusinessLayer = new PatientBusinessLayer();
            appointmentListViewModel = new AppointmentListViewModel();
            List<Appointment> appointmentList = GetDoctorAppointments(doctorId);
            appointmentListViewModelList = new List<AppointmentListViewModel>();
            foreach (var appointment in appointmentList)
            {
                appointmentListViewModel = new AppointmentListViewModel();
                appointmentListViewModel.Id = appointment.Id;
                appointmentListViewModel.PatientId = appointment.PatientId;
                appointmentListViewModel.PatientName = patientBusinessLayer.GetPatientNameById(appointment.PatientId);
                appointmentListViewModel.DoctorName = GetDoctorNameById(appointment.DoctorId);
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
                appointmentListViewModelList.Add(appointmentListViewModel);
            }
            return appointmentListViewModelList;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }



        public List<DoctorAppointment> GetDoctorsUpcomingAppointments(int docId)
        {
            try
            {
                doctorDataLayer = new DoctorDataLayer();
                patientDataLayer = new PatientDataLayer();
                List<Appointment> appointments = doctorDataLayer.GetDoctorUpcomingAppointments(docId);
                List<DoctorAppointment> doctorAppointments = new List<DoctorAppointment>();
                DoctorAppointment doctorAppointment;
                foreach (var appointment in appointments)
                {
                    doctorAppointment = new DoctorAppointment();
                    doctorAppointment.Id = appointment.Id;
                    doctorAppointment.PatientId = appointment.PatientId;
                    doctorAppointment.PatientName = patientDataLayer.GetPatientNameById(appointment.PatientId);
                    doctorAppointment.Date = appointment.Date.ToString().Split(' ')[0];
                    doctorAppointment.TimeSlot = TimeSlots.Timings[appointment.Time];
                    doctorAppointment.Status = appointment.Status;
                    doctorAppointments.Add(doctorAppointment);
                }
                return doctorAppointments;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<DoctorAppointment> GetDoctorsAllAppointments(int docId)
        {
            try
            {
                doctorDataLayer = new DoctorDataLayer();
                patientDataLayer = new PatientDataLayer();
                List<Appointment> appointments = doctorDataLayer.GetDoctorAllAppointments(docId);
                List<DoctorAppointment> doctorAppointments = new List<DoctorAppointment>();
                DoctorAppointment doctorAppointment;
                foreach (var appointment in appointments)
                {
                    doctorAppointment = new DoctorAppointment();
                    doctorAppointment.Id = appointment.Id;
                    doctorAppointment.PatientId = appointment.PatientId;
                    doctorAppointment.PatientName = patientDataLayer.GetPatientNameById(appointment.PatientId);
                    doctorAppointment.Date = appointment.Date.ToString().Split(' ')[0];
                    doctorAppointment.TimeSlot = TimeSlots.Timings[appointment.Time];
                    doctorAppointment.Status = appointment.Status;
                    doctorAppointments.Add(doctorAppointment);
                }
                return doctorAppointments;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Doctor> GetAllDoctors()
        {
            try
            {
                return doctorDataLayer.GetAllDoctors();
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void DeleteDoctor(int id)
        {
            try { 
            doctorDataLayer.DeleteDoctor(id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void EditDoctorDetails(Doctor doctor)
        {
            try
            {
                doctorDataLayer.EditDoctorDetails(doctor);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
    }
}