using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesLibrary.ActionFilter
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Session["loggedUser"] == null)
                filterContext.Result = new RedirectResult("/Home/Login");
        }
    }
}