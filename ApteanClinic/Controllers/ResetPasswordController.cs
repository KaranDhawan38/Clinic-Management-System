using ApteanClinic.BusinessLayer;
using ApteanClinic.Filters;
using ApteanClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logger;

namespace ApteanClinic.Controllers
{
    [AuthenticateUser]
    public class ResetPasswordController : Controller
    {
        UserBusinessLayer user;

        public ResetPasswordController()
        {
            user = new UserBusinessLayer();
        }

        // GET: ResetPassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(!user.ResetPassword(resetPassword,int.Parse(Session["UserId"].ToString())))
            {
                Logging.loggError($"Old password is incorrect for user Id = {Session["UserId"]}, please try again");
                ModelState.AddModelError("OldPassword", "Old Password is Incorrect");
                return View();
            }
            Logging.loggInfo($"Password reset for user Id = {Session["UserId"]}");
            return Redirect("/HomePage/Dashboard");
        }
    }
}