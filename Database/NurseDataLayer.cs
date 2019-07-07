using ApteanClinic.Models;
using ApteanClinic.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Diagnostics;

namespace ApteanClinic.Database
{
    public class NurseDataLayer
    {
        public Nurse AddNurse(Nurse nurse)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    context.Nurses.Add(nurse);
                    context.SaveChanges();
                    return nurse;
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Nurse GetNurseById(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Nurses.Include(n => n.NurseUser).Where(n => n.Id == id).FirstOrDefault();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public int? GetAvailableNurse(DateTime date, int time)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    List<int> TotalNurse = context.Nurses
                                         .Select(n => n.Id).ToList();

                    if (TotalNurse.Count == 0) return null;

                    List<int> BusyNurse = context.Appointments
                                        .Where(d => d.Date == date && d.Time == time)
                                        .Select(n => n.NurseId).ToList();

                    var list = TotalNurse.Except(BusyNurse);
                    int? AvailableNurse = list.FirstOrDefault();
                    return AvailableNurse;
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Nurse GetNurseByEmail(string email)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Nurses.Include(n => n.NurseUser).Where(n => n.NurseUser.Email == email).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public string GetNurseNameById(int id)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Nurses.Where(n => n.Id == id).Select(n => n.NurseUser.Name).FirstOrDefault();
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
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void DeleteNurse(Nurse nurse)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    User nurseBasicDetails = nurse.NurseUser;
                    context.Entry(nurse).State = EntityState.Deleted;
                    context.Nurses.Remove(nurse);
                    context.SaveChanges();
                    UserDataLayer userData = new UserDataLayer();
                    userData.DeleteUser(nurseBasicDetails);
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void UpdateNurseDetails(Nurse nurse)
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    User user = nurse.NurseUser;
                    context.Entry(user).State = EntityState.Modified;
                    context.Entry(nurse).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Nurse> GetAllNurses()
        {
            try
            {
                using (var context = new ApteanClinicContext())
                {
                    return context.Nurses.Include(n => n.NurseUser).ToList();
                }
            }catch(Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
    }
}