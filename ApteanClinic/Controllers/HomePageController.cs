using ApteanClinic.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApteanClinic.BusinessLayer;
using System.Web.Mvc;
using ApteanClinic.Models;
using log4net;
using Logger;

namespace ApteanClinic.Controllers
{
    [OutputCache(Duration = 0)]
    [AuthenticateUser]
    public class HomePageController : Controller
    {

        private UserBusinessLayer userBusinessLayer;
        private AppointmentBusinessLayer appointmentBusinessLayer;

        public HomePageController()
        {
            userBusinessLayer = new UserBusinessLayer();
        }

        public ActionResult Dashboard(DashboardModel dashboard)
        {
            List<int> stats=userBusinessLayer.GetDashboardStats(Session["Role"].ToString(),int.Parse(Session["UserId"].ToString()));
            appointmentBusinessLayer = new AppointmentBusinessLayer();
            dashboard.totalUsers = stats[0];
            dashboard.totalDoctors = stats[1];
            dashboard.totalAppointments = stats[2];
            dashboard.totalAppointmentsToday = stats[3];
            dashboard.totalPatients = stats[4];
            appointmentBusinessLayer.CancelPastAppointments();
            return View(dashboard);
        }

        public ActionResult Logout()
        {

            int id = (int)Session["UserId"];
            Logging.loggInfo($"Logging out user having user id = {id}");
            Session.Clear();
            return Redirect("/Login/Index");
        }
    }
}