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
    public class AddressesController : BaseController
    {

        private ContactDal dal = new ContactDal();
        private LookupDal dalLookup = new LookupDal();



        public ActionResult Index(int id)
        {
            ViewBag.PersonID = id;
            List<Address> addresses = dal.SearchPersonAddressesByPersonId(id);
            return PartialView("_Index", addresses);
        }

        [ChildActionOnly]
        public ActionResult List(int id)
        {
            ViewBag.PersonID = id;
            var addresses = dal.SearchPersonAddressesByPersonId(id);

            return PartialView("_List", addresses.ToList());
        }

        public ActionResult Create(int PersonID)
        {
            Address address = new Address();
            address.RuleMessageList = new List<BusinessRule>();
            address.AddressTypes = new SelectList(dalLookup.GetAllAddressTypes(), "Id", "Name");
            return PartialView("_Create", address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Address address)
        {
            if (ModelState.IsValid)
            {
                if (dal.SearchPersonAddressesByPersonId(address.PersonID).Where(x => x.AddressTypeId == address.AddressTypeId).Count() > 0)
                {
                    Address addressNew = new Address();
                    addressNew.AddressTypes = new SelectList(dalLookup.GetAllAddressTypes(), "Id", "Name");

                    var rule = new BusinessRule() { RuleMessage = $"{dalLookup.GetAllAddressTypes().Where(t => t.Id == address.AddressTypeId).SingleOrDefault().Description.ToLower() }  already added!" };
                    addressNew.RuleMessageList = new List<BusinessRule>();
                    addressNew.RuleMessageList.Add(rule);

                    return PartialView("_Create", addressNew);
                }

                dal.SavePersonAddress(address);

                string url = Url.Action("Index", "Addresses", new { id = address.PersonID });
                return Json(new { success = true, url = url });
            }

            address.AddressTypes = new SelectList(dalLookup.GetAllAddressTypes(), "Id", "Name");

            return PartialView("_Create", address);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = dal.SearchPersonAddressesByAddressId(id.Value);
            if (address == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Edit", address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Address address)
        {
            if (ModelState.IsValid)
            {
                dal.UpdatePersonAddress(address);
                string url = Url.Action("Index", "Addresses", new { id = address.PersonID });
                return Json(new { success = true, url = url });
            }

            return PartialView("_Edit", address);
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
