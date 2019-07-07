using ApteanClinic.Models;
using ApteanClinic.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ApteanClinic.Database
{
    public class DoctorDataLayer
    {
        public Doctor AddDoctor(Doctor doctor)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Doctors.Add(doctor);
                    context.DoctorTimes.AddRange(doctor.ShiftTime);
                    context.SaveChanges();
                    return doctor;
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Doctors.Where(d => d.Id == id).Select(d => d.DoctorUser.Name).FirstOrDefault();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }


        //Returns a list of doctor of particular speciality
        public List<Doctor> GetDoctorsBySpeciality(Speciality speciality)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Doctors.Include(d => d.DoctorUser).Where(d => d.Speciality == speciality).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Doctor GetDoctorById(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Doctors.Include(d => d.DoctorUser).Where(d => d.Id == id).FirstOrDefault();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<DoctorTime> GetDoctorShiftTimeByDoctorId(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.DoctorTimes.Include(t => t.Doctor).Where(t => t.Doctor.Id == id).ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Appointment> GetDoctorAppointments(int doctorId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.Where(a => a.DoctorId == doctorId).OrderByDescending(a => a.Date).OrderByDescending(a => a.Time).ToList();
                }
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
                using (var context = new ApteanClinicContext())
                {
                    return context.Doctors.Include(d => d.DoctorUser).Where(d => d.DoctorUser.Email == email).FirstOrDefault();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<DoctorTime> GetDoctorTimeSlot(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.DoctorTimes.Where(t => t.Doctor.Id == id).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<int> GetDocotorBookedSlots(int id , DateTime date)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.Where(a => a.DoctorId == id).Where(a => a.Date == date && (a.Status == AppointmentStatus.Pending || a.Status == AppointmentStatus.Approved)).Select(a => a.Time).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Appointment> GetDoctorUpcomingAppointments(int docId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.Where(a => a.DoctorId == docId).Where(a => a.Status == AppointmentStatus.Approved).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Appointment> GetDoctorAllAppointments(int docId)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Appointments.Where(a => a.DoctorId == docId).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Doctor> GetAllDoctors()
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Doctors.Include(d => d.DoctorUser).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void DeleteDoctor(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    var doctor = context.Doctors.Include(d => d.DoctorUser).Where(d => d.Id == id).FirstOrDefault();
                    User doctorBasicDetails = doctor.DoctorUser;
                    List<DoctorTime> doctorTimes = context.DoctorTimes.Include(d => d.Doctor).Where(d => d.Doctor.Id == id).ToList();
                    for (int i = 0; i < doctorTimes.Count; i++)
                    {
                        context.DoctorTimes.Remove(doctorTimes[i]);
                    }
                    context.Doctors.Remove(doctor);
                    context.SaveChanges();
                    UserDataLayer userData = new UserDataLayer();
                    userData.DeleteUser(doctorBasicDetails);
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
        public void EditDoctorDetails(Doctor doctor)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    User user = doctor.DoctorUser;
                    context.Entry(user).State = EntityState.Modified;
                    context.Entry(doctor).State = EntityState.Modified;
                    context.SaveChanges();

                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
    }
}