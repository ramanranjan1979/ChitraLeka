using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChitraLeka.ViewModel.Shared;
using ChitraLekaDAL;
using System.Net;
using ChitraLeka.ViewModel.Contact;

namespace ChitraLeka.Controllers
{
    public class SharedController : BaseController
    {
        private ContactDal dal = new ContactDal();

        public ActionResult GetLoginSupport()
        {
            Support model = new Support() { isMemnber = false };
            return PartialView("_SupportPopup", model);
        }

        public ActionResult UploadSupportTicket(Support model)
        {
            string responseCode = "OK";
            return Json(new { Ok = responseCode }, JsonRequestBehavior.AllowGet);

        }
               


        public ActionResult ViewPerson(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person p = dal.searchPersonById(id.Value);

            if (p == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Person", p);
        }
    }
}