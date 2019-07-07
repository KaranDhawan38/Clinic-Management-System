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
    public class UserDataLayer
    {
        public User AddUser(User user)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public User GetUser(string email)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Users.Where(user => user.Email == email).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                using(var context = new ApteanClinicContext())
                {
                    context.Users.Add(user);
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                    return user;
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public User GetUserByMobile(string contact)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Users.Where(u => u.Contact == contact).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        //public User UpdateUser(User user)
        //{
        //    try
        //    {
        //        using (var context = new ApteanClinicContext())
        //        {
        //            context.Entry(user).State = EntityState.Modified;
        //            context.SaveChanges();
        //            return user;
        //        }
        //    }catch(Exception e)
        //    {
        //        ExceptionHandler.PrintException(e, new StackTrace(true));
        //        throw e;
        //    }
        //}

        public static bool CheckEmail(string email)
        {
            try
            {
                UserDataLayer user = new UserDataLayer();
                return user.GetUser(email) == null;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public int GetDoctorIdByUserId(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    Doctor doctor = context.Doctors.Include(d => d.DoctorUser).Where(d => d.DoctorUser.Id == id).FirstOrDefault();
                    return doctor.Id;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public int GetNurseIdByUserId(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    Nurse nurse = context.Nurses.Include(d => d.NurseUser).Where(d => d.NurseUser.Id == id).FirstOrDefault();
                    return nurse.Id;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public int GetPatientIdByUserId(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    Patient patient = context.Patients.Include(d => d.PatientUser).Where(d => d.PatientUser.Id == id).FirstOrDefault();
                    return patient.Id;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public bool PasswordValid(string password, int id)
        {
            using (var context = new ApteanClinicContext())
            {
                User user = context.Users.Select(a => a).Where(a => a.Id == id).FirstOrDefault();
                if (user.Password == password)
                    return true;
                else
                    return false;
            }
        }

        public void ChangePassword(string newPassword, int id)
        {
            using (var context = new ApteanClinicContext())
            {
                User user = context.Users.Select(a => a).Where(a => a.Id == id).SingleOrDefault();
                user.Password = user.ConfirmPassword = newPassword;
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public User GetUserById(int id)
        {
            using(var context = new ApteanClinicContext())
            {
                return context.Users.Where(user => user.Id == id).FirstOrDefault();
            }
        }

        public void UpdateAdminInfo(User user)
        {
            using (var context = new ApteanClinicContext())
            {
                User userTemp= context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                userTemp.Gender = user.Gender;
                userTemp.Name = user.Name;
                userTemp.BloodGroup = user.BloodGroup;
                userTemp.Email = user.Email;
                userTemp.Contact = user.Contact;
                context.Entry(userTemp).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<int> GetDashboardStats(string role, int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    List<int> stats = new List<int>();
                    stats.Add(context.Users.Count()); //User count is fixed for everyone
                    stats.Add(context.Users.Where(user => user.Role == Role.Doctor).Count()); //Total doctors count is fixed for everyone
                    /***********This part gives the count of total appointments*************/
                    if (role == "Doctor")
                    {
                        Doctor doctor = context.Doctors.Include(d => d.DoctorUser).Where(d => d.DoctorUser.Id == id).FirstOrDefault();
                        stats.Add(context.Appointments.Where(d => d.DoctorId == doctor.Id).Count());
                    }
                    else
                        stats.Add(context.Appointments.Count());
                    /////////////////////////////////////////////////////////////////////////

                    /***********This part will give the count of total appointments for today*****/
                    if (role == "Doctor")
                    {
                        Doctor doctor = context.Doctors.Include(d => d.DoctorUser).Where(d => d.DoctorUser.Id == id).FirstOrDefault();
                        List<Appointment> appointments= context.Appointments.Where(d => d.DoctorId == doctor.Id).ToList();
                        int count = 0;
                        foreach (var appointment in appointments)
                        {
                            if (appointment.Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                                count++;
                        }
                        stats.Add(count);
                    }
                    else
                    {
                        List<Appointment> appointments = context.Appointments.ToList();
                        int count = 0;
                        foreach (var appointment in appointments)
                        {
                            if (appointment.Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                                count++;
                        }
                        stats.Add(count);
                    }
                    ///////////////////////////////////////////////////////////////////////////////

                    /*******This part will count total patients for diffrent roles**************/
                    if (role == "Doctor")
                    {
                        Doctor doctor = context.Doctors.Include(d => d.DoctorUser).Where(d => d.DoctorUser.Id == id).FirstOrDefault();
                        stats.Add(context.Appointments.Where(s => s.DoctorId == doctor.Id).Select(s => s.PatientId).Distinct().Count());
                    }
                    else
                        stats.Add(context.Users.Where(user => user.Role == Role.Patient).Count());
                    /////////////////////////////////////////////////////////////////////////////
                    return stats;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Entry(user).State = EntityState.Deleted;
                    context.Users.Remove(user);
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