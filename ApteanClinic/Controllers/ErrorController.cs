using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApteanClinic.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string error)
        {
            if (error == null)
                error = "404";
            if (TempData["ErrorCode"] == null)
            {
                TempData["ErrorCode"] = error;
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}