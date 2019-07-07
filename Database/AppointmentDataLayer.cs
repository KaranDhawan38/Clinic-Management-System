using ApteanClinic.Models;
using ApteanClinic.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApteanClinic.Database
{
    public class AppointmentDataLayer
    {
        public List<Appointment> GetAppointmentsById(int patientId, int appointmtnId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.Where(a => a.Id == appointmtnId).Where(a => a.PatientId == patientId).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Appointment GetAppointmentsById(int appointmtnId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.Where(a => a.Id == appointmtnId).FirstOrDefault();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public MedicinesQuantity GetInvoiceByAppointmentId(int appointmentId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Medicine_Quantity.Where(d => d.Appointment_Id == appointmentId).FirstOrDefault();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void GetPastAppointments(DateTime date)
        {
            try
            {
                using(var context = new ApteanClinicContext())
                {
                    var appointments = context.Appointments.Where(a => a.Date < date).ToList();
                    appointments.Where(a => a.Status == AppointmentStatus.Pending || a.Status == AppointmentStatus.Approved).ToList().ForEach(a => a.Status = AppointmentStatus.Cancelled);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void UpdateAppointments(List<Appointment> appointments)
        {
            try
            {
                using(var context = new ApteanClinicContext())
                {
                    context.Appointments.AddRange(appointments);
                   // context.Entry(appointments).State = EntityState.Modified;
                    context.Entry<List<Appointment>>(appointments).State = EntityState.Modified;
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
