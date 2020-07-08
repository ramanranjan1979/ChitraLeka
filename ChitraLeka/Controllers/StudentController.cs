using ChitraLeka.Filter;
using ChitraLeka.ViewModel.Contact;
using ChitraLeka.ViewModel.Registration;
using ChitraLeka.ViewModel.Shared;
using ChitraLeka.ViewModel.Student;
using ChitraLekaDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class StudentController : BaseController
    {
        private RegistrationDal regDal = new RegistrationDal();
        private LookupDal lookupDal = new LookupDal();
        private ContactDal contactDal = new ContactDal();
        private LedgerDal ledgertDal = new LedgerDal();



        // GET: Student
        public ActionResult Index()
        {

            RegistrationList regList = regDal.GetAllRegisteredCandidate();
            foreach (var reg in regList.Registrations)
            {
                reg.Candidate = contactDal.searchPersonById(reg.Candidate.Id);
            }
            return View(regList);
        }

        public ActionResult InitiateAdmission(int RegistrationId)
        {
            Student student = new Student();

            List<AcademicCenter> centers = lookupDal.GetAllAcademyCenter();
            student.AcademyCenters = new SelectList(centers, "Id", "Name");
            var regDetail = regDal.GetRegistrationDetailByRegistrationId(RegistrationId);

            student.RegistrationId = RegistrationId;
            student.Grade = lookupDal.GetAllGrades().Where(g => g.Id == regDetail.Grade).FirstOrDefault().Name;

            List<Term> gradeTerms = lookupDal.GetAllTerms(regDetail.Grade).TermList.Where(x => x.GradeId == regDetail.Grade && DateTime.Now <= x.EndDate).ToList();

            student.GradeTerms = new SelectList(gradeTerms, "Id", "Name");

            return View(student);
        }

        [HttpPost]
        public ActionResult CompleteAdmission(Student newStudent)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;

                if (newStudent.DateOfAdmission < DateTime.Today)
                {
                    ModelState.AddModelError("DateOfAdmission", "Admission date cannot be set to past date!");
                    isValid = false;
                }

                if (newStudent.StartDate < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be set to past date!");
                    isValid = false;
                }

                if (newStudent.EndDate < DateTime.Today)
                {
                    ModelState.AddModelError("EndDate", "End date cannot be set to past date!");
                    isValid = false;
                }

                if (newStudent.StartDate > newStudent.EndDate)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be greater than end date!");
                    isValid = false;
                }

                if (newStudent.StartDate < newStudent.DateOfAdmission)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be greater than admission date!");
                    isValid = false;
                }

                if (newStudent.EndDate < newStudent.DateOfAdmission)
                {
                    ModelState.AddModelError("EndDate", "End date cannot be greater than admission date!");
                    isValid = false;
                }

                if (newStudent.StartDate.Date == newStudent.EndDate.Date)
                {
                    ModelState.AddModelError("StartDate", "Start date and end date cannot be same!");
                    isValid = false;
                }


                if (isValid == false)
                {
                    Student student = new Student();
                    List<AcademicCenter> centers = lookupDal.GetAllAcademyCenter();
                    student.AcademyCenters = new SelectList(centers, "Id", "Name");
                    var regDetail = regDal.GetRegistrationDetailByRegistrationId(newStudent.RegistrationId);

                    student.RegistrationId = newStudent.RegistrationId;
                    student.Grade = lookupDal.GetAllGrades().Where(g => g.Id == regDetail.Grade).FirstOrDefault().Name;

                    return View("InitiateAdmission", student);
                }

                regDal.CompleteAdmission(newStudent);

                ledgertDal.CreateInvoice(newStudent);

                return RedirectToAction("Index");
            }
            return View("InitiateAdmission", newStudent);
        }

        public ActionResult ViewAdmission(int StudentId)
        {
            Student s = regDal.GetStudentDetailByStudentId(StudentId);
            return View(s);
        }

        public ActionResult Promotion()
        {
            return View();
        }
    }
}