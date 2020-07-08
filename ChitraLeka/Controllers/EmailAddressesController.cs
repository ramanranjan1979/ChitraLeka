using System.Linq;
using System.Net;
using System.Web.Mvc;
using ChitraLekaDAL;
using ChitraLeka.ViewModel;
using ChitraLeka.ViewModel.Contact;
using System.Collections.Generic;
using ChitraLeka.Controllers;
using ChitraLeka.Filter;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class EmailAddressesController : BaseController
    {

        private ContactDal dal = new ContactDal();



        public ActionResult Index(int id)
        {
            ViewBag.PersonID = id;
            List<EmailAddress> emailAddresses = dal.SearchPersonEmailAddressesByPersonId(id);
            return PartialView("_Index", emailAddresses);
        }

        [ChildActionOnly]
        public ActionResult List(int id)
        {
            ViewBag.PersonID = id;
            var emailAddresses = dal.SearchPersonEmailAddressesByPersonId(id);
            return PartialView("_List", emailAddresses.ToList());
        }

        public ActionResult Create(int PersonID)
        {
            EmailAddress e = new EmailAddress() { PersonId = PersonID };
            return PartialView("_Create", e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmailAddress e)
        {
            if (ModelState.IsValid)
            {
                
                dal.SavePersonEmailAddress(e);

                string url = Url.Action("Index", "EmailAddresses", new { id = e.PersonId });
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", e);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAddress contactEmail = dal.SearchEmailAddressByEmailAddressId(id.Value);

            if (contactEmail == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Edit", contactEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmailAddress editEmailAddress)
        {
            if (ModelState.IsValid)
            {
                dal.UpdateEmailAddress(editEmailAddress);
                string url = Url.Action("Index", "Addresses", new { id = editEmailAddress.PersonId });
                return Json(new { success = true, url = url });
            }


            return PartialView("_Edit", editEmailAddress);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = dal.SearchPersonAddressesByPersonId(id).FirstOrDefault();
            if (address == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = dal.SearchPersonAddress(id);

            dal.DeletePersonAddress(address);



            string url = Url.Action("Index", "Addresses", new { id = address.PersonID });
            return Json(new { success = true, url = url });

        }


        [HttpPost]
        public JsonResult DoesEmailExist(string PrimaryEmail)
        {
            EmailAddress anyEmail = dal.GetEmailAddress(PrimaryEmail);
            return Json(anyEmail.Value == null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dal.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}
