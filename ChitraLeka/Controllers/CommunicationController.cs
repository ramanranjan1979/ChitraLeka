using ChitraLeka.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class CommunicationController : BaseController
    {
        // GET: Communication
        public ActionResult Index()
        {
            return View();
        }

        // GET: Communication
        public ActionResult MailingList()
        {
            return View();
        }
    }
}