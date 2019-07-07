using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ApteanClinic.Filters
{
    public class AuthorizeUser : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!Roles.Contains(HttpContext.Current.Session["Role"].ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "action", "Index" },
                        { "controller", "Error" },
                        { "error", "401" }
                    }
                    );
            }
        }
    }
}