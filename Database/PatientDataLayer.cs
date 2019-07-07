using ApteanClinic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace ApteanClinic.Database
{
    public class PatientDataLayer
    {
        public Patient AddPatient(Patient patient)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Patients.Add(patient);
                    context.SaveChanges();
                    return patient;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Patient> GetAllPatients(string role, int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    if (role == "Doctor")
                    {
                        Doctor doctor = context.Doctors.Include(d => d.DoctorUser).Where(d => d.DoctorUser.Id == id).FirstOrDefault();
                        List<int> PatientsId = context.Appointments.Where(s => s.DoctorId == doctor.Id).Select(s => s.PatientId).Distinct().ToList();
                        List<Patient> list = new List<Patient>();
                        foreach (int Id in PatientsId)
                        {
                            list.Add(context.Patients.Include(p => p.PatientUser).Where(a => a.Id == Id).FirstOrDefault());
                        }
                        return list;
                    }
                    else
                        return context.Patients.Include(p => p.PatientUser).ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Patient GetPatienById(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Patients.Include(p => p.PatientUser).Where(p => p.Id == id).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void UpdatePatientDetails(Patient patient)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    User user = patient.PatientUser;
                    context.Entry(user).State = EntityState.Modified;
                    context.Entry(patient).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void RemovePatient(Patient patient)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    //Remove appointments of patient
                    List <Appointment> appointments = context.Appointments.Where(a => a.PatientId == patient.Id).ToList();
                    foreach(var appointment in appointments)
                    {
                        //Remove medical history of patient
                        List<MedicalHistory> medicalHistories = context.MedicalHistories.Where(a => a.AppointmentId == appointment.Id).ToList();
                        foreach(var medicalHistory in medicalHistories)
                        {
                            context.Entry(medicalHistory).State = EntityState.Deleted;
                            context.MedicalHistories.Remove(medicalHistory);
                        }
                        //Remove Medicines quantity related to patient
                        List<MedicinesQuantity> medicines = context.Medicine_Quantity.Where(a => a.Appointment_Id == appointment.Id).ToList();
                        foreach (var medicine in medicines)
                        {
                            context.Entry(medicine).State = EntityState.Deleted;
                            context.Medicine_Quantity.Remove(medicine);
                        }
                        //Remove invoices of patient
                        List<Invoice> invoices = context.Invoices.Where(a => a.Appointment_Id == appointment.Id).ToList();
                        foreach (var invoice in invoices)
                        {
                            context.Entry(invoice).State = EntityState.Deleted;
                            context.Invoices.Remove(invoice);
                        }
                        context.Entry(appointment).State = EntityState.Deleted;
                        context.Appointments.Remove(appointment);
                    }
                    //Remove patient
                    User patientBasicDetials = patient.PatientUser;
                    context.Entry(patient).State = EntityState.Deleted;
                    context.Patients.Remove(patient);
                    context.SaveChanges();
                    UserDataLayer userData = new UserDataLayer();
                    userData.DeleteUser(patientBasicDetials);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Patient GetPatientByEmail(string email)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Patients.Include(p => p.PatientUser).Where(p => p.PatientUser.Email == email).FirstOrDefault();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    context.Dispose();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Patients.Where(p => p.Id == id).Select(p => p.PatientUser.Name).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public int GetTotalAppointments(DateTime date, int Time, int Doctor)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.Where(a => a.Date == date).Where(a => a.Time == Time).Where(a => a.DoctorId == Doctor).Count();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public Appointment FixAppointment(Appointment appointment)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Appointments.Add(appointment);
                    context.SaveChanges();
                    return appointment;
                }
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
                using (var context = new ApteanClinicContext())
                {
                    context.Appointments.Add(appointment);
                    context.Entry(appointment).State = EntityState.Modified;
                    context.SaveChanges();
                    return appointment;
                }
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
                using (var context = new ApteanClinicContext())
                {
                    if (patientId == -1)
                        return context.Appointments.ToList();
                    return context.Appointments.Where(a => a.PatientId == patientId).ToList();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Medicine_Quantity.Where(m => m.Appointment_Id == AppointmentId).ToList();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Medicines.Where(a => a.Id == MedicineId).Select(a => a.Name).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public string GetAppointmentDate(int AppointmentId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    string date = context.Appointments.Where(a => a.Id == AppointmentId).Select(a => a.Date).FirstOrDefault().ToString();
                    return date;

                }
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
                using (var context = new ApteanClinicContext())
                {
                    int PatientId = GetPatientIdByAppointmentId(AppointmentId);
                    return context.Patients.Where(a => a.Id == PatientId).Select(a => a.PatientUser.Contact).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public List<MedicalHistory> GetPatientMedicalHistory(int patientId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.MedicalHistories.Where(m => m.PatientId == patientId).ToList();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    foreach (var item in medicine)
                    {
                        context.Medicine_Quantity.Add(item);
                        context.SaveChanges();
                    }
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.Where(a => a.Id == Appointment_Id).Select(a => a.PatientId).FirstOrDefault();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    int PatientId = GetPatientIdByAppointmentId(Appointment_Id);
                    return context.Patients.Where(p => p.Id == PatientId).Select(p => p.PatientUser.Name).FirstOrDefault();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    int DoctorId = context.Appointments.Where(a => a.Id == Appointment_Id).Select(a => a.DoctorId).FirstOrDefault();
                    int fees = (int)context.Doctors.Where(d => d.Id == DoctorId).Select(d => d.Fees).FirstOrDefault();
                    return fees;
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Medicine_Quantity.Where(m => m.Appointment_Id == Appointment_Id).FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public int GetTotalBill(int Appointment_Id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    int totalBill = (int)context.Invoices.Where(m => m.Appointment_Id == Appointment_Id).Select(m => m.Total).FirstOrDefault();
                    return totalBill;

                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public int GetMedicineCost(int MedicineId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return Convert.ToInt32(context.Medicines.Include(m => m.Cost).Where(m => m.Id == MedicineId).Select(m => m.Cost).FirstOrDefault());
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Invoices.Where(m => m.Appointment_Id == AppointmentId).FirstOrDefault();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Invoices.Where(m => m.Id == InvoiceId).FirstOrDefault();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Invoices.Where(m => m.Id == InvoiceId).Select(m => m.Appointment_Id).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public List<Appointment> GetAppointments()
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.ToList();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Invoices.Count();
                }
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

                using (var context = new ApteanClinicContext())
                {
                    context.Invoices.Add(invoice);
                    context.SaveChanges();
                }

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
                using (var context = new ApteanClinicContext())
                {
                    int AppointmentId = GetAppointmentDetailsByInvoiceId(InvoiceId);
                    var Medicines=context.Medicine_Quantity.Where(a => a.Appointment_Id == AppointmentId).ToList();
                    context.Medicine_Quantity.RemoveRange(Medicines);
                    Invoice invoice=context.Invoices.Find(InvoiceId);
                    context.Entry(invoice).State = EntityState.Deleted;
                    context.Invoices.Remove(invoice);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
       
    }
}