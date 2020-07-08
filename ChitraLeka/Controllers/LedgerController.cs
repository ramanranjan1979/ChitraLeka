using ChitraLeka.Filter;
using ChitraLeka.ViewModel.Ledger;
using ChitraLekaDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class LedgerController : BaseController
    {
        private LedgerDal lDal = new LedgerDal();
        private ContactDal cDal = new ContactDal();
        private RegistrationDal rDal = new RegistrationDal();
        private LookupDal lkpDal = new LookupDal();

        // GET: Ledger
        public ActionResult CreateInvoice()
        {
            InvoiceCreation model = new InvoiceCreation();

            List<InvoiceType> invoiceTypes = null;
            List<ViewModel.Student.Student> studentList = null;

            invoiceTypes = lkpDal.GetAllInvoiceTypes();
            model.InvoiceTypes = new SelectList(invoiceTypes, "Id", "Code");

            studentList = rDal.GetAllStudents();
            model.StudentList = new SelectList(studentList, "StudentId", "Candidate.FirstName");

            model.IsDue = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveInvoice(InvoiceCreation NewInvoice)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;

                NewInvoice.PersonId = rDal.GetStudentDetailByStudentId(NewInvoice.StudentId).Candidate.Id;

                if (NewInvoice.DueDate < DateTime.Today)
                {
                    ModelState.AddModelError("DueDate", "Due date cannot be set to past date!");
                    isValid = false;
                }

                if (NewInvoice.DueDate > DateTime.Today.AddDays(5))
                {
                    ModelState.AddModelError("DueDate", "Due date cannot be more than 5 days from now!");
                    isValid = false;
                }

                if (lDal.GetOpenInvoice(NewInvoice.InvoiceTypeId, NewInvoice.PersonId).InvoiceList.Count > 0)
                {
                    ModelState.AddModelError("InvoiceAmount", $"There is already a pending invoice of type {NewInvoice.InvoiceTypeId}");
                    isValid = false;
                }

                if (!isValid)
                {
                    List<InvoiceType> invoiceTypes = null;
                    List<ViewModel.Student.Student> studentList = null;
                    invoiceTypes = lkpDal.GetAllInvoiceTypes();
                    NewInvoice.InvoiceTypes = new SelectList(invoiceTypes, "Id", "Code");
                    studentList = rDal.GetAllStudents();
                    NewInvoice.StudentList = new SelectList(studentList, "StudentId", "Candidate.FirstName");
                    NewInvoice.IsDue = true;
                    return View("CreateInvoice", NewInvoice);
                }

                lDal.CreateInvoice(NewInvoice);
                return RedirectToAction("DueInvoice");
            }
            else
            {
                List<InvoiceType> invoiceTypes = null;
                List<ViewModel.Student.Student> studentList = null;
                invoiceTypes = lkpDal.GetAllInvoiceTypes();
                NewInvoice.InvoiceTypes = new SelectList(invoiceTypes, "Id", "Code");
                studentList = rDal.GetAllStudents();
                NewInvoice.StudentList = new SelectList(studentList, "StudentId", "Candidate.FirstName");
                NewInvoice.IsDue = true;
                return View("CreateInvoice", NewInvoice);
            }
        }

        public ActionResult DueInvoice()
        {
            InvoiceListing AllInvoices = lDal.GetAllInvoices();
            foreach (var invoice in AllInvoices.InvoiceList)
            {
                invoice.Person = cDal.searchPersonById(invoice.PersonId);
                invoice.Student = rDal.GetStudentDetailByStudentId(invoice.StudentId);
            }
            return View(AllInvoices);
        }

        public ActionResult ViewInvoiceDetails(int InvoiceId)
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetStudentDetailByStudentId(int studentId)
        {
            var student = rDal.GetStudentDetailByStudentId(studentId).StudentGrades.StudentGrades.Where(x => x.IsCurrent).FirstOrDefault();

            return Json(new
            {
                t = student.Term,
                g = student.Grade
            }
            );
        }
    }
}