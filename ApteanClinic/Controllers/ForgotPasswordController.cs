using ApteanClinic.BusinessLayer;
using ApteanClinic.Filters;
using ApteanClinic.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logger;

namespace ApteanClinic.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private UserBusinessLayer userBusinessLayer;
        private static int GeneratedOTP;
        private static string MobileNumber;
        // GET: ForgotPassword
        public ActionResult Index()
        {
            ViewBag.OTP = "false";
            return View();
        }

        public ActionResult OTPSend(ForgotPassword forgotPassword)
        {
            ModelState.Remove("OTP");
            if (!ModelState.IsValid)
            {
                ViewBag.OTP = "false";
                return View("Index");
            }
            GeneratedOTP = Verify.GetOTP(forgotPassword.MobileNumber);
            MobileNumber = forgotPassword.MobileNumber;
            ViewBag.OTP = "true";
            Logging.loggInfo($"Sending otp to user having mobile number {MobileNumber}");
            return View("Index");
        }

        public ActionResult OTPCheck(ForgotPassword forgotPassword)
        {
            ModelState.Remove("MobileNumber");
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            if (GeneratedOTP == forgotPassword.OTP)
            {
                Logging.loggInfo($"Otp Matched for mobile number {MobileNumber} , redirecting to ResetPassword Page");
                return RedirectToAction("NewPassword");
            }
            ModelState.AddModelError("OTP", "Wrong OTP");
            ViewBag.OTP = "true";
            Logging.loggError($"Otp not matched for mobile number {MobileNumber}");
            return View("Index");
        }

        public ActionResult NewPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewPassword(string newpassword, string confirmpassword)
        {
            if(newpassword == "")
            {
                Logging.loggError($"New Password empty error");
                ModelState.AddModelError("", "Please enter a valid password");
                return View();
            }
            else if(newpassword != confirmpassword)
            {
                Logging.loggError($"Password Don't match");
                ModelState.AddModelError("", "Passwords don't match");
                return View();
            }
            userBusinessLayer = new UserBusinessLayer();
            Boolean passwordUpdate = userBusinessLayer.UpdateUserPassword(MobileNumber, newpassword);
            if (passwordUpdate)
            {
                Logging.loggInfo($"Password updated of user having mobile number = {MobileNumber}");
                return Redirect("/Login/Index");
            }
            else
            {
                ViewBag.OTP = "false";
                ModelState.AddModelError("", "Operation failed.Please try again");
                Logging.loggError($"Unable to update password");
                return RedirectToAction("Index");
            }
        }
    }
}