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
    public class ContactNumbersController : BaseController
    {

        private ContactDal dal = new ContactDal();
        private LookupDal dalLookup = new LookupDal();


        public ActionResult Index(int id)
        {
            ViewBag.PersonID = id;
            List<Contact> contactNumbers = dal.SearchPersonContactNumbersByPersonId(id);
            return PartialView("_Index", contactNumbers);
        }

        [ChildActionOnly]
        public ActionResult List(int id)
        {
            ViewBag.PersonID = id;
            var contactNumbers = dal.SearchPersonContactNumbersByPersonId(id);

            return PartialView("_List", contactNumbers.ToList());
        }

        public ActionResult Create(int PersonID)
        {
            Contact contactNumber = new Contact();
            contactNumber.RuleMessageList = new List<BusinessRule>();
            contactNumber.ContactNumberTypes = new SelectList(dalLookup.GetAllContactNumberTypes(), "Id", "Name");
            return PartialView("_Create", contactNumber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contactNumber)
        {
            if (ModelState.IsValid)
            {
                if (dal.SearchPersonContactNumbersByPersonId(contactNumber.PersonID).Where(x => x.ContactNumberTypeId == contactNumber.ContactNumberTypeId).Count() > 0)
                {

                    Contact contactNumberNew = new Contact();
                    contactNumberNew.RuleMessageList = new List<BusinessRule>();
                    contactNumberNew.ContactNumberTypes = new SelectList(dalLookup.GetAllContactNumberTypes(), "Id", "Name");
                    var rule = new BusinessRule() { RuleMessage = $"{dalLookup.GetAllContactNumberTypes().Where(ct => ct.Id == contactNumber.ContactNumberTypeId).FirstOrDefault().Description.ToLower()} number already added!" };
                    contactNumberNew.RuleMessageList = new List<BusinessRule>();
                    contactNumberNew.RuleMessageList.Add(rule);
                    return PartialView("_Create", contactNumberNew);
                }
                dal.SavePersonContactNumber(contactNumber);

                string url = Url.Action("Index", "ContactNumbers", new { id = contactNumber.PersonID });
                return Json(new { success = true, url = url });
            }

            contactNumber.ContactNumberTypes = new SelectList(dalLookup.GetAllContactNumberTypes(), "Id", "Name");
            return PartialView("_Create", contactNumber);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contactEdit = dal.SearchPersonContactNumbersByContactId(id.Value);
            contactEdit.ContactNumberTypes = new SelectList(dalLookup.GetAllContactNumberTypes(), "Id", "Name");

            if (contactEdit == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Edit", contactEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contactNumber)
        {
            if (ModelState.IsValid)
            {   
                dal.UpdatePersonContactNumber(contactNumber);
                string url = Url.Action("Index", "ContactNumbers", new { id = contactNumber.PersonID });
                return Json(new { success = true, url = url });
            }

            contactNumber.ContactNumberTypes = new SelectList(dalLookup.GetAllContactNumberTypes(), "Id", "Name");
            return PartialView("_Edit", contactNumber);
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
            Contact contact = dal.SearchPersonContactNumbersByContactId(id);
            dal.DeletePersonContactNumber(contact);
            string url = Url.Action("Index", "ContactNumbers", new { id = contact.PersonID });
            return Json(new { success = true, url = url });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dal.Dispose();
                dalLookup.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}
