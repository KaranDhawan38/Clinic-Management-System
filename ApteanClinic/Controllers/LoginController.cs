using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApteanClinic.BusinessLayer;
using ApteanClinic.Database;
using ApteanClinic.Models;
using BusinessLayer;
using log4net;
using Microsoft.AspNet.Identity.Owin;
using Logger;

namespace ApteanClinic.Controllers
{
    [OutputCache(Duration = 0)]
    public class LoginController : Controller
    {

        private UserBusinessLayer userBusinessLayer;
        // private ApteanClinicContext db = new ApteanClinicContext();
        // GET: Login
        public LoginController()
        {
            userBusinessLayer = new UserBusinessLayer();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return View();
            else
                return Redirect("/HomePage/DashBoard");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login login, string ReturnUrl)
        {
            if (Session["UserId"] == null)
            {
                if (!ModelState.IsValid)
                {

                    Logging.loggError($"Invald model state");
                    return View("Index");
                }
                var user = userBusinessLayer.ValidateUserLoginCredentials(login);
                if (user == null)
                {
                    Logging.loggError($"Invalid Credentials");
                    ModelState.AddModelError("Password", "Invalid Credentials");
                    return View();
                }
                Session["UserId"] = user.Id;
                Session["Username"] = user.Name;
                Session["Role"] = user.Role;
                switch (Session["Role"].ToString())
                {
                    case "Doctor":
                        {
                            Session["DoctorId"] = userBusinessLayer.GetDoctorIdByUserId(user.Id);
                        }
                        break;
                    case "Nurse":
                        {
                            Session["NurseId"] = userBusinessLayer.GetNurseIdByUserId(user.Id);
                        }
                        break;
                    case "Patient":
                        {
                            Session["PatientId"] = userBusinessLayer.GetPatientIdByUserId(user.Id);
                        }
                        break;
                }
                Session.Timeout = 1440; //1440 Minutes = 24 Hours

                Logging.loggInfo($"Logged in with user id = {user.Id} and Role = {user.Role}");
            }
            if (ReturnUrl == null)
                return Redirect("/HomePage/Dashboard");
            else
                return Redirect(ReturnUrl);
        }
    }
}