using ChitraLekaDAL;
using ChitraLeka.ViewModel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChitraLeka.ViewModel.Contact;
using ChitraLeka.Filter;
using ChitraLeka.ViewModel.Shared;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class RegistrationController : BaseController
    {
        private RegistrationDal regDal = new RegistrationDal();
        private LookupDal lookupDal = new LookupDal();
        private ContactDal contactDal = new ContactDal();
        private BusinessRuleList ruleList = new BusinessRuleList();
        


        // GET: Registration
        public ActionResult Index()
        {
            RegistrationList regList = regDal.GetAllCandidateForRegistration();
            return View(regList);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult InitiateCandidateRegistration(int id)
        {
            Registration reg = new Registration();
            List<Grade> gradeTypes = null;
            gradeTypes = lookupDal.GetAllGrades();
            reg.GradeTypes = new SelectList(gradeTypes, "Id", "Name");
            reg.Candidate = contactDal.searchPersonById(id);
            reg.Candidate.DOB = reg.Candidate.DOB.Date;
            return View(reg);
        }

        [HttpPost]
        public ActionResult CompleteRegistration(Registration reg)
        {
            if (ModelState.IsValid)
            {
                regDal.CompleteRegistration(reg);
                return RedirectToAction("Index");
            }

            return View("InitiateCandidateRegistration", reg);
        }

        public ActionResult EnterParentDetails(int id)
        {
            Person father = new Person();
            Person mother = new Person();

            List<PersonType> personTypes = null;
            List<GenderType> genderTypes = new List<GenderType>();
            List<Salutation> salutationTypes = new List<Salutation>();

            genderTypes.Add(new GenderType() { Id = 2, Name = "Male" });

            personTypes = lookupDal.GetAllPersonTypes().Where(x => x.Name == "Father").ToList();
            salutationTypes = lookupDal.GetAllSalutation().Where(x => x.Name == "Mr").ToList();

            father.PersonTypes = new SelectList(personTypes, "Id", "Name");
            father.Gender = new SelectList(genderTypes, "Id", "Name");
            father.Salutations = new SelectList(salutationTypes, "Id", "Name");

            List<PersonType> personTypes1 = null;
            List<GenderType> genderTypes1 = new List<GenderType>();
            List<Salutation> salutationTypes1 = new List<Salutation>();

            genderTypes1.Add(new GenderType() { Id = 1, Name = "Female" });

            personTypes1 = lookupDal.GetAllPersonTypes().Where(x => x.Name == "Mother").ToList();
            salutationTypes1 = lookupDal.GetAllSalutation().Where(x => x.Name == "Mrs").ToList();

            mother.PersonTypes = new SelectList(personTypes1, "Id", "Name");
            mother.Gender = new SelectList(genderTypes1, "Id", "Name");
            mother.Salutations = new SelectList(salutationTypes1, "Id", "Name");

            Parent candidateParent = new Parent();

            candidateParent.Father = father;
            candidateParent.Mother = mother;
            candidateParent.ChildPersonId = id;

            return View(candidateParent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateParentsDetail(Parent parent)
        {
            if (ModelState.IsValid)
            {
                bool IsVaid = true;
                ruleList.RuleMessageList = new List<BusinessRule>();

                var child = contactDal.searchPersonById(parent.ChildPersonId);

                if (Common.Helper.GetDifferenceInYears(parent.Father.DOB, DateTime.Now) < 21 || parent.Father.DOB > DateTime.Now)
                {
                    IsVaid = false;
                    BusinessRule br1 = new BusinessRule() { RuleMessage = "Please check father's date of birth" };
                    ruleList.RuleMessageList.Add(br1);
                }


                if (child.DOB.Date <= parent.Father.DOB.Date)
                {
                    IsVaid = false;
                    BusinessRule br2 = new BusinessRule() { RuleMessage = "Father's date of birth cannot be smaller than child" };
                    ruleList.RuleMessageList.Add(br2);
                }

                if (Common.Helper.GetDifferenceInYears(parent.Mother.DOB, DateTime.Now) < 21 || parent.Mother.DOB > DateTime.Now)
                {
                    IsVaid = false;
                    BusinessRule br2 = new BusinessRule() { RuleMessage = "Please check mother's date of birth" };
                    ruleList.RuleMessageList.Add(br2);
                }

                if (child.DOB.Date <= parent.Mother.DOB.Date)
                {
                    IsVaid = false;
                    BusinessRule br2 = new BusinessRule() { RuleMessage = "Mother's date of birth cannot be smaller than child" };
                    ruleList.RuleMessageList.Add(br2);
                }

                if (IsVaid == false)
                {
                    Parent candidateParent = new Parent();


                    candidateParent.RuleMessageList = ruleList.RuleMessageList;

                    Person father = new Person();
                    Person mother = new Person();

                    List<PersonType> personTypes = null;
                    List<GenderType> genderTypes = new List<GenderType>();
                    List<Salutation> salutationTypes = new List<Salutation>();

                    genderTypes.Add(new GenderType() { Id = 2, Name = "Male" });

                    personTypes = lookupDal.GetAllPersonTypes().Where(x => x.Name == "Father").ToList();
                    salutationTypes = lookupDal.GetAllSalutation().Where(x => x.Name == "Mr").ToList();

                    father.PersonTypes = new SelectList(personTypes, "Id", "Name");
                    father.Gender = new SelectList(genderTypes, "Id", "Name");
                    father.Salutations = new SelectList(salutationTypes, "Id", "Name");

                    List<PersonType> personTypes1 = null;
                    List<GenderType> genderTypes1 = new List<GenderType>();
                    List<Salutation> salutationTypes1 = new List<Salutation>();

                    genderTypes1.Add(new GenderType() { Id = 1, Name = "Female" });

                    personTypes1 = lookupDal.GetAllPersonTypes().Where(x => x.Name == "Mother").ToList();
                    salutationTypes1 = lookupDal.GetAllSalutation().Where(x => x.Name == "Mrs").ToList();

                    mother.PersonTypes = new SelectList(personTypes1, "Id", "Name");
                    mother.Gender = new SelectList(genderTypes1, "Id", "Name");
                    mother.Salutations = new SelectList(salutationTypes1, "Id", "Name");

                    

                    candidateParent.Father = father;
                    candidateParent.Mother = mother;
                    candidateParent.ChildPersonId = parent.ChildPersonId;

                    return View("EnterParentDetails", candidateParent);
                }

                regDal.SaveParentDetails(parent);
                return RedirectToAction("Index");
            }
            return View(parent);
        }

        public ActionResult ViewRegistrationDetails(int registrationId)
        {
            Registration reg = regDal.GetRegistrationDetailByRegistrationId(registrationId);
            reg.Candidate = contactDal.searchPersonById(reg.Candidate.Id);
            return View(reg);
        }

        public ActionResult ViewParentsDetails(int FatherPersonId, int MotherPersonId, int RegistrationId, int PersonId)
        {
            Registration reg = new Registration();// regDal.GetRegistrationDetailByRegistrationId(RegistrationId);
            reg.Candidate = contactDal.searchPersonById(PersonId);
            reg.CandidateFather = contactDal.searchPersonById(FatherPersonId);
            reg.CandidateMother = contactDal.searchPersonById(MotherPersonId);
            return View(reg);
        }

        public ActionResult ViewParentsDetailsWhenNotRegistered(int FatherPersonId, int MotherPersonId, int PersonId)
        {
            Registration reg = new Registration();
            reg.Candidate = contactDal.searchPersonById(PersonId);
            reg.CandidateFather = contactDal.searchPersonById(FatherPersonId);
            reg.CandidateMother = contactDal.searchPersonById(MotherPersonId);
            return View("ViewParentsDetails", reg);
        }

    }
}