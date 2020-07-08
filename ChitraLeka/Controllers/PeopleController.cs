using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ChitraLeka.ViewModel;
using ChitraLekaDAL;
using ChitraLeka.ViewModel.Contact;
using ChitraLeka.Filter;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class PeopleController : BaseController
    {
        private ContactDal contactDal = new ContactDal();
        private LookupDal lookupDal = new LookupDal();


        public ActionResult Index()
        {
            return View(contactDal.GetAllPerson());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = contactDal.searchPersonById(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }


        public ActionResult Create()
        {
            Person model = new Person();
            List<PersonType> personTypes = null;
            List<GenderType> genderTypes = new List<GenderType>();
            List<Salutation> salutationTypes = new List<Salutation>();

            genderTypes.Add(new GenderType() { Id = 1, Name = "Female" });
            genderTypes.Add(new GenderType() { Id = 2, Name = "Male" });

            personTypes = lookupDal.GetAllPersonTypes().Where(p => p.Id < 6).ToList();
            salutationTypes = lookupDal.GetAllSalutation();

            model.PersonTypes = new SelectList(personTypes, "Id", "Name");
            model.Gender = new SelectList(genderTypes, "Id", "Name");
            model.Salutations = new SelectList(salutationTypes, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                if (person.DOB > DateTime.Today)
                {
                    //ModelState.AddModelError("DOB", "Date of birth cannot be set to future date!");
                    Person model = new Person();

                    model.RuleMessageList = new List<BusinessRule>();
                    var rule = new BusinessRule() { RuleMessage = "Date of birth cannot be set to future date!" };
                    model.RuleMessageList.Add(rule);

                    List<PersonType> personTypes = null;
                    List<GenderType> genderTypes = new List<GenderType>();
                    List<Salutation> salutationTypes = new List<Salutation>();

                    genderTypes.Add(new GenderType() { Id = 1, Name = "Female" });
                    genderTypes.Add(new GenderType() { Id = 2, Name = "Male" });

                    personTypes = lookupDal.GetAllPersonTypes().Where(p => p.Id < 6).ToList();
                    salutationTypes = lookupDal.GetAllSalutation();

                    model.PersonTypes = new SelectList(personTypes, "Id", "Name");
                    model.Gender = new SelectList(genderTypes, "Id", "Name");
                    model.Salutations = new SelectList(salutationTypes, "Id", "Name");

                    return View(model);
                }

                if (Common.Helper.GetDifferenceInYears(person.DOB, DateTime.Now) <= 3)
                {
                    //ModelState.AddModelError("DOB", "Date of birth cannot be set to future date!");
                    Person model = new Person();

                    model.RuleMessageList = new List<BusinessRule>();
                    var rule = new BusinessRule() { RuleMessage = "Please check the date of birth" };
                    model.RuleMessageList.Add(rule);

                    List<PersonType> personTypes = null;
                    List<GenderType> genderTypes = new List<GenderType>();
                    List<Salutation> salutationTypes = new List<Salutation>();

                    genderTypes.Add(new GenderType() { Id = 1, Name = "Female" });
                    genderTypes.Add(new GenderType() { Id = 2, Name = "Male" });

                    personTypes = lookupDal.GetAllPersonTypes().Where(p => p.Id < 6).ToList();
                    salutationTypes = lookupDal.GetAllSalutation();

                    model.PersonTypes = new SelectList(personTypes, "Id", "Name");
                    model.Gender = new SelectList(genderTypes, "Id", "Name");
                    model.Salutations = new SelectList(salutationTypes, "Id", "Name");

                    return View(model);
                }

                contactDal.SavePerson(person);

                return RedirectToAction("Index");
            }

            return View(person);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var person = contactDal.searchPersonById(id.Value);

            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {

            if (ModelState.IsValid)
            {
                if (person.DOB > DateTime.Today)
                {
                    ModelState.AddModelError("DOB", "Date of birth cannot be set to future date!");
                    return View(person);
                }

                var fatherChildren = contactDal.GetFatherChildren(person.Id);
                var motherChildren = contactDal.GetMotherChildren(person.Id);

                if (fatherChildren.Count > 0)
                {
                    foreach (var child in fatherChildren)
                    {
                        if (child.DOB <= person.DOB)
                        {
                            ModelState.AddModelError("DOB", $"Date of birth cannot be less than or equals to your child {child.FirstName} {child.LastName} date of birth!");
                            return View(person);
                        }
                    }
                }

                if (motherChildren.Count > 0)
                {
                    foreach (var child in motherChildren)
                    {
                        if (child.DOB <= person.DOB)
                        {
                            ModelState.AddModelError("DOB", $"Date of birth cannot be less than or equals to your child {child.FirstName} {child.LastName} date of birth!");
                            return View(person);
                        }
                    }
                }

                contactDal.UpdatePerson(person);

                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = contactDal.searchPersonById(id.Value);

            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = contactDal.searchPersonById(id);

            contactDal.DeletePersonById(id);

            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contactDal.Dispose();
                lookupDal.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
