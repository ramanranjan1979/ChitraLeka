using ChitraLeka.Common.PDF;
using ChitraLeka.Filter;
using ChitraLeka.ViewModel.Ledger;
using ChitraLekaDAL;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class PdfController : BaseController
    {
        private LedgerDal lDal = new LedgerDal();
        private ContactDal cDal = new ContactDal();
        private RegistrationDal regDal = new RegistrationDal();

        public ActionResult PrintInvoice(int InvoiceId)
        {
            Invoice inv = lDal.GetAllInvoices().InvoiceList.Where(i => i.Id == InvoiceId).SingleOrDefault();
            inv.Person = cDal.searchPersonById(inv.PersonId);
            inv.Student= regDal.GetStudentDetailByStudentId(inv.StudentId);
            return new PdfActionResult(inv, inv.InvoiceNumber + ".pdf", "Index");
        }
    }
}
