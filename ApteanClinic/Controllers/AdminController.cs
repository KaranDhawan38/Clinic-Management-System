using ApteanClinic.BusinessLayer;
using ApteanClinic.Filters;
using ApteanClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApteanClinic.Controllers
{
    [AuthenticateUser]
    public class AdminController : Controller
    {
        UserBusinessLayer userBusinessLayer;

        public AdminController()
        {
            userBusinessLayer = new UserBusinessLayer();
        }

        [HttpGet]
        [AuthorizeUser(Roles ="Admin")]
        public ActionResult Edit()
        {
            User usermodel= userBusinessLayer.GetUserById(int.Parse(Session["UserId"].ToString()));
            return View(usermodel);
        }

        [HttpPost]
        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Edit(User user)
        {
            userBusinessLayer.UpdateAdminInfo(user);
            Session["Username"] = user.Name;
            return Redirect("/HomePage/Dashboard");
        }
    }
}