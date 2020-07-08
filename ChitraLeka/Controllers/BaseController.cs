using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Routing;
using ChitraLeka.Common;
using ChitraLeka.Session;
using ChitraLekaDAL;

namespace ChitraLeka.Controllers
{
    public class BaseController : Controller
    {
        private SecurityDal _DAL = new SecurityDal();

        private List<string> DO_NOT_ALLOW = new List<string>();

        public BaseController(){}

      

        protected internal string GetUserIp()
        {
            var UserIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(UserIPAddress))
            {
                UserIPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            return UserIPAddress;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exceptionDetails = filterContext.Exception.InnerException == null ? $"Exception:{filterContext.Exception.ToString()}" : $"Exception:{filterContext.Exception.ToString()}#InnerException{filterContext.Exception.InnerException.ToString()}";

            int? pid = null;

            if (new SessionManager().UserSession != null)
            {
                pid= new int();
                pid = new SessionManager().UserSession.Person.Id;
            }

            _DAL.LogMe("Exception", exceptionDetails, pid);

            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Error/Error.cshtml",
                ViewData = new ViewDataDictionary(exceptionDetails)
            };
        }


    }
}