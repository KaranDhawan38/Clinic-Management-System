using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Logger;

namespace ApteanClinic.Filters
{
    public class CheckId : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            int id;
            if(filterContext.Controller.ValueProvider.GetValue("id")!=null)
            {
                id = int.Parse(filterContext.Controller.ValueProvider.GetValue("id").AttemptedValue.ToString());
                string role = HttpContext.Current.Session["Role"].ToString();
                int userId;
                bool flag = false;
                switch (role)
                {
                    case "Patient":
                        {
                            userId = int.Parse(HttpContext.Current.Session["PatientId"].ToString());
                            if (userId != id)
                            {
                                Logging.loggError($"Access Denied for this action for Patient having Id = {userId} ");
                                flag = true;
                            }
                        }
                        break;
                    case "Nurse":
                        {
                            userId = int.Parse(HttpContext.Current.Session["NurseId"].ToString());
                            if (controllerName == "Nurse" && userId != id)
                            {

                                Logging.loggError($"Access Denied for this action for Nurse having Id = {userId} ");
                                flag = true;
                            }
                        }
                        break;
                    case "Doctor":
                        {
                            userId = int.Parse(HttpContext.Current.Session["DoctorId"].ToString());
                            if (controllerName == "Doctors" && userId != id)
                            {
                                Logging.loggError($"Access Denied for this action for Doctor having Id = {userId} ");
                                flag = true;
                            }
                        }
                        break;
                    case "Admin":
                        break;
                }
                if (flag == true)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                        { "action", "Index" },
                        { "controller", "Error" },
                        { "error", "404" }
                        }
                        );
                }
            }
        }
    }
}