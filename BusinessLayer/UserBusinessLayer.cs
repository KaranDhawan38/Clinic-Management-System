using ApteanClinic.Database;
using ApteanClinic.Models;
using BusinessLayer;
using Logger;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace ApteanClinic.BusinessLayer
{
    public class UserBusinessLayer
    {
        private UserDataLayer userDataLayer;

        public UserBusinessLayer()
        {
            userDataLayer = new UserDataLayer();
        }

        public bool ResetPassword(ResetPassword password,int id)
        {
            if(userDataLayer.PasswordValid(Encryption.Encrypt(password.OldPassword),id))
            {
                userDataLayer.ChangePassword(Encryption.Encrypt(password.NewPassword), id);
                return true;
            }
            return false;
        }

        public User GetUserById(int id)
        {
            return userDataLayer.GetUserById(id);
        }

        public User ValidateUserLoginCredentials(Login login)
        {
            try
            {
                //throw new Exception("Hello exception");
                var user = GetUserByEmail(login.Email);
                if (user == null)
                {
                    return null;
                }
                if (user.Password != Encryption.Encrypt(login.Password))
                {
                    return null;
                }
                return user;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void AddUser(User user)
        {
            try
            {
                user.Password = Encryption.Encrypt(user.Password);
                userDataLayer.AddUser(user);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return userDataLayer.GetUser(email);
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
                return userDataLayer.GetDoctorIdByUserId(id);
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
                return userDataLayer.GetNurseIdByUserId(id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public void UpdateAdminInfo(User user)
        {
            userDataLayer.UpdateAdminInfo(user);
        }

        public int GetPatientIdByUserId(int id)
        {
            try
            {
                return userDataLayer.GetPatientIdByUserId(id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public Boolean UpdateUserPassword(string contact,string password)
        {
            try
            {
                User user = userDataLayer.GetUserByMobile(contact);
                if (user == null) return false;
                user.Password = Encryption.Encrypt(password);
                user.ConfirmPassword = Encryption.Encrypt(password);
                user = userDataLayer.UpdateUser(user);
                if (user != null) return true;
                else return false;
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

        public List<int> GetDashboardStats(string role,int id)
        {
            try
            {
                return userDataLayer.GetDashboardStats(role, id);
            }
            catch (Exception e)
            {
                ExceptionHandler.PrintException(e, new StackTrace(true));
                throw e;
            }
        }

    }
}