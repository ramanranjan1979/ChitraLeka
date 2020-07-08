using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChitraLeka.ViewModel.Registration;
using ChitraLekaDAL;
using ChitraLeka.Filter;
using ChitraLeka.ViewModel.Shared;
using System.Configuration;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class LookupController : BaseController
    {

        private LedgerDal lDal = new LedgerDal();
        private ContactDal cDal = new ContactDal();
        private RegistrationDal rDal = new RegistrationDal();
        private LookupDal lkpDal = new LookupDal();
        private StudentDal stuDal = new StudentDal();

        // GET: Lookup
        public ActionResult OurCenter()
        {
            List<AcademicCenter> ac = lkpDal.GetAllAcademyCenter();
            return View(ac);
        }

        // GET: Lookup
        public ActionResult Grades()
        {
            List<Grade> grades = lkpDal.GetAllGrades();
            return View(grades);
        }

        public ActionResult InvoiceTypes()
        {
            List<ViewModel.Ledger.InvoiceType> types = lkpDal.GetAllInvoiceTypes();
            return View(types);
        }

        public ActionResult CreateEvent()
        {
            TermCreation term = new TermCreation();

            List<Grade> gradeTypes = null;
            List<AcademicCenter> locations = null;
            gradeTypes = lkpDal.GetAllGrades();
            locations = lkpDal.GetAllAcademyCenter();
            term.Grades = new SelectList(gradeTypes, "Id", "Name");
            term.Locations = new SelectList(locations, "Id", "Name");

            return View(term);
        }

        public ActionResult CreateEvenDetails(TermCreation tc)
        {
            return View(tc);
        }

        public ActionResult EditTerm(string id)
        {
            TermCreation term = lkpDal.GetTermDetailByTermId(int.Parse(id));
            List<Grade> gradeTypes = null;
            gradeTypes = lkpDal.GetAllGrades();
            term.Grades = new SelectList(gradeTypes, "Id", "Name");
            return View(term);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEvent_Old(TermCreation NewTerm)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;

                if (NewTerm.StartDate < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be set to past date!");
                    isValid = false;
                }

                if (NewTerm.EndDate < DateTime.Today)
                {
                    ModelState.AddModelError("EndDate", "End date cannot be set to past date!");
                    isValid = false;
                }

                if (NewTerm.StartDate > NewTerm.EndDate)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be greater than end date!");
                    ModelState.AddModelError("EndDate", "Start date cannot be greater than end date!");
                    isValid = false;
                }

                if ((NewTerm.EndDate - NewTerm.StartDate).TotalDays <= 50)
                {
                    ModelState.AddModelError("DaysCount", "Check start and end date!");
                    isValid = false;
                }

                var existingTerm = lkpDal.GetAllTerms(NewTerm.GradeId);

                foreach (var term in existingTerm.TermList)
                {
                    if (string.Equals(term.Name, NewTerm.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError("Name", "Duplicate term has been found with this name");
                        isValid = false;
                    }

                    if (NewTerm.StartDate >= term.EndDate && NewTerm.EndDate > term.StartDate)
                    {
                        isValid = true;
                    }
                    else
                    {
                        ModelState.AddModelError("StartDate", $"Start date is overlaping with term {term.Name} !");
                        isValid = false;
                        break;
                    }

                }

                if (isValid == false)
                {
                    List<Grade> gradeTypes = null;
                    gradeTypes = lkpDal.GetAllGrades();
                    NewTerm.Grades = new SelectList(gradeTypes, "Id", "Name");
                    return View("CreateTerm", NewTerm);
                }

                lkpDal.SaveGradeTerm(NewTerm);

                return RedirectToAction("Termlist");
            }
            else
            {
                List<Grade> gradeTypes = null;
                gradeTypes = lkpDal.GetAllGrades();
                NewTerm.Grades = new SelectList(gradeTypes, "Id", "Name");
                return View("CreateTerm", NewTerm);

            }
        }

        [HttpPost]
        public JsonResult SaveEvent(int GradeId, int LocationId, DateTime StartDate, DateTime EndDate, int WeeksCount, int DaysCount, decimal UnitPrice, decimal Fee, string Remarks, string Name)
        {

            return Json("ok");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTerm(TermCreation EditTerm)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;

                if (EditTerm.StartDate < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be set to past date!");
                    isValid = false;
                }

                if (EditTerm.EndDate < DateTime.Today)
                {
                    ModelState.AddModelError("EndDate", "End date cannot be set to past date!");
                    isValid = false;
                }

                if (EditTerm.StartDate > EditTerm.EndDate)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be greater than end date!");
                    ModelState.AddModelError("EndDate", "Start date cannot be greater than end date!");
                    isValid = false;
                }

                if ((EditTerm.EndDate - EditTerm.StartDate).TotalDays <= 50)
                {
                    ModelState.AddModelError("DaysCount", "Check start and end date!");
                    isValid = false;
                }

                var existingTerm = lkpDal.GetAllTerms(EditTerm.GradeId);

                foreach (var term in existingTerm.TermList.Where(x => x.Id != EditTerm.Id))
                {
                    if (string.Equals(term.Name, EditTerm.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError("Name", "Duplicate term has been found with this name");
                        isValid = false;
                    }

                    if (EditTerm.StartDate <= term.StartDate || EditTerm.StartDate <= term.EndDate)
                    {
                        ModelState.AddModelError("StartDate", $"Start date is overlaping with term {term.Name} !");
                        isValid = false;
                        break;
                    }

                    if (EditTerm.EndDate <= term.StartDate || EditTerm.EndDate <= term.EndDate)
                    {
                        ModelState.AddModelError("EndDate", $"End date is overlaping with term {term.Name} !");
                        isValid = false;
                        break;
                    }
                }

                if (isValid == false)
                {
                    List<Grade> gradeTypes = null;
                    gradeTypes = lkpDal.GetAllGrades();
                    EditTerm.Grades = new SelectList(gradeTypes, "Id", "Name");
                    return View("EditTerm", EditTerm);
                }

                lkpDal.UpdateGradeTerm(EditTerm);

                return RedirectToAction("Termlist");
            }

            return View(EditTerm);
        }

        public ActionResult Eventlist()
        {
            TermListing AllTerms = lkpDal.GetAllTerms(null);
            return View(AllTerms);
        }

        [HttpPost]
        public JsonResult GetWeekCount(DateTime d1, DateTime d2)
        {
            int weekcount = (int)(d2.Subtract(d1).TotalDays) / 7;
            return Json(weekcount);
        }

        [HttpPost]
        public JsonResult GetDaysCount(DateTime d1, DateTime d2)
        {
            int daycount = (int)(d2.Subtract(d1).TotalDays);
            return Json(daycount);
        }

        [HttpPost]
        public JsonResult GetTermDetailByTermId(int termId)
        {
            var data = lkpDal.GetTermDetailByTermId(termId);
            return Json(new
            {
                d1Y = data.StartDate.Year,
                d1M = data.StartDate.Month.ToString("0#"),
                d1D = data.StartDate.Day.ToString("0#"),

                d2Y = data.EndDate.Year,
                d2M = data.EndDate.Month.ToString("0#"),
                d2D = data.EndDate.Day.ToString("0#"),

            });
        }


    }
}