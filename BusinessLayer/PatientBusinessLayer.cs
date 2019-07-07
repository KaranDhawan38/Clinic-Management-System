
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using ApteanClinic.Database;
using ApteanClinic.Models;
using ApteanClinic.Models.Enum;
using BusinessLayer;
using Models.Enum;

namespace ApteanClinic.BusinessLayer
{
    public class PatientBusinessLayer
    {
        private PatientDataLayer patientDataLayer;

        public PatientBusinessLayer()
        {
            patientDataLayer = new PatientDataLayer();
        }

        public List<Patient> GetAllPatients(string role,int id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetAllPatients(role, id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Patient GetPatientById(int id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetPatienById(id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void AddPatient(Patient patient)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                patient.PatientUser.Password = patient.PatientUser.ConfirmPassword = Encryption.Encrypt(patient.PatientUser.Password);
                patientDataLayer.AddPatient(patient);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void UpdatePatientDetials(Patient patient)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                patientDataLayer.UpdatePatientDetails(patient);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Patient GetPatientByEmail(string email)
        {
            return patientDataLayer.GetPatientByEmail(email);
        }

        public void RemovePatient(Patient patient)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                patientDataLayer.RemovePatient(patient);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void Dispose()
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                patientDataLayer.Dispose();
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public string GetPatientNameById(int id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetPatientNameById(id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Appointment UpdateAppointment(Appointment appointment)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.UpdateAppointment(appointment);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Appointment> GetPatientAppointments(int patientId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetPatientAppointments(patientId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public List<MedicinesQuantity> GetPatientMedicines(int AppointmentId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetPatientMedicines(AppointmentId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public string GetMedicineName(int MedicineId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetMedicineName(MedicineId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public string GetPhoneNumber(int AppointmentId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetPhoneNumber(AppointmentId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void SaveData(List<MedicinesQuantity> medicine)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                patientDataLayer.SaveData(medicine);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public string GetPatientNameByAppointmentId(int Appointment_Id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetPatientNameByAppointmentId(Appointment_Id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public string GetAppointmentDate(int Appointment_Id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetAppointmentDate(Appointment_Id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public int GetPatientIdByAppointmentId(int Appointment_Id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetPatientIdByAppointmentId(Appointment_Id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public int GetDoctorFeesByAppointmentId(int Appointment_Id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetDoctorFeesByAppointmentId(Appointment_Id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public MedicinesQuantity CheckIdInList(int Appointment_Id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.CheckIdInList(Appointment_Id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public int GetTotalBill(int AppointmentId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetTotalBill(AppointmentId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public int GetMedicineCost(int Medicine_Id)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetMedicineCost(Medicine_Id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
       
        public Invoice GetInvoice(int AppointmentId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetInvoice(AppointmentId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public int GetAppointmentDetailsByInvoiceId(int InvoiceId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetAppointmentDetailsByInvoiceId(InvoiceId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
       public List<Appointment>  GetAppointments()
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                return patientDataLayer.GetAppointments();
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public int GetNumberOfInvoices()
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
            return patientDataLayer.GetNumberOfInvoices();
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void createInvoice(Invoice invoice)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                patientDataLayer.createInvoice(invoice);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void DeleteInvoice(int InvoiceId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
                patientDataLayer.DeleteInvoice(InvoiceId);
                
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
     
        
             public Invoice GetInvoiceByInvoiceId(int InvoiceId)
        {
            try
            {
                patientDataLayer = new PatientDataLayer();
               return patientDataLayer.GetInvoiceByInvoiceId(InvoiceId);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }

        }
    }
}