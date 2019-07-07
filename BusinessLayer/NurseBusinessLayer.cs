using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ApteanClinic.Database;
using ApteanClinic.Models;
using ApteanClinic.Models.Enum;
using BusinessLayer;

namespace ApteanClinic.BusinessLayer
{
    public class NurseBusinessLayer
    {
        private NurseDataLayer nurseDataLayer;
        public NurseBusinessLayer()
        {
            nurseDataLayer = new NurseDataLayer();
        }

        public void AddNurse(Nurse nurse)
        {
            try
            {
                nurse.NurseUser.Password = nurse.NurseUser.ConfirmPassword = Encryption.Encrypt(nurse.NurseUser.Password);
                nurseDataLayer.AddNurse(nurse);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<Nurse> GetAllNurses()
        {
            try
            {
                return nurseDataLayer.GetAllNurses();
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Nurse GetNurseById(int id)
        {
            try
            {
                return nurseDataLayer.GetNurseById(id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public int GetAvailableNurse(DateTime date, int time)
        {
            try
            {
                var Nurse = nurseDataLayer.GetAvailableNurse(date, time);
                if (Nurse == null)
                {
                    return -1;
                }
                else
                {
                    return Nurse.Value;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void UpdateNurseDetails(Nurse nurse)
        {
            try
            {
                nurseDataLayer.UpdateNurseDetails(nurse);
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
                return nurseDataLayer.GetNurseNameById(id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Nurse GetNurseByEmail(string email)
        {
            try
            {
                return nurseDataLayer.GetNurseByEmail(email);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void DeleteNurse(Nurse nurse)
        {
            try
            {
                nurseDataLayer.DeleteNurse(nurse);
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
                nurseDataLayer.Dispose();
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }
    }
}