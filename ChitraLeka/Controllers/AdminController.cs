using ChitraLeka.Filter;
using ChitraLeka.Session;
using ChitraLeka.ViewModel;
using ChitraLeka.ViewModel.Contact;
using ChitraLeka.ViewModel.Shared;
using ChitraLekaDAL;
using ChitraLekaService.Notification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class AdminController : BaseController
    {
        private SecurityDal sDal = new SecurityDal();
        private LedgerDal lDal = new LedgerDal();
        private ContactDal cDal = new ContactDal();
        private RegistrationDal rDal = new RegistrationDal();
        private LookupDal lkpDal = new LookupDal();
        private StudentDal stuDal = new StudentDal();
        private NotificationDal mxDal = new NotificationDal();
        private EmailService eService = new EmailService(bool.Parse(ConfigurationManager.AppSettings["TESTMODE"]), System.Web.HttpContext.Current);
        private SessionManager sm = new SessionManager();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ActionLogin(int? id)
        {
            if (id.HasValue)
            {
                sDal.UpdateUserLogin(id.Value, "LOCK");
            }
            return RedirectToAction("Index", "Security");
        }

        [HttpPost]
        public JsonResult GetMxTemplateByMailoutTypeId(int mailoutTypeId)
        {
            List<MxType> mxTypes = lkpDal.GetAllMailoutTemplates();
            foreach (var mx in mxTypes)
            {
                mx.HtmlBody = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + $"{mx.Name}.html");
            }

            return Json(new { html = mxTypes.Where(x => x.Id == mailoutTypeId).FirstOrDefault().HtmlBody });
        }

        public ActionResult MailoutTypes()
        {
            List<MxType> mxTypes = lkpDal.GetAllMailoutTemplates();
            foreach (var mx in mxTypes)
            {
                mx.HtmlBody = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + $"{mx.Name}.html");
            }
            return View(mxTypes);
        }

        public ActionResult ComposeEmail()
        {
            MailoutCompose vModel = new MailoutCompose() { From = new SessionManager().UserSession.Person.PrimaryEmail };
            vModel.PersonTypeList = new SelectList(lkpDal.GetAllPersonTypes().Where(x => x.Id != 2), "Id", "Name");
            return View(vModel);
        }

        public JsonResult GetGrades(string id)
        {
            if (id == "1")
            {
                var data = lkpDal.GetAllGrades();
                data.Add(new Grade() { Id = 0, Name = "--Please select a grade--" });
                return Json(new SelectList(data.OrderBy(o => o.Id), "Id", "Name"));
            }
            else
            {
                return Json(new SelectList(lkpDal.GetAllPersonTypes().Where(x => x.Id.ToString() == id), "Id", "Name"));
            }

        }

        public JsonResult GetStudentByGrades(string id1, string id2)
        {
            if (id1 == "1")
            {
                var data = stuDal.GetAllStudents(int.Parse(id2));
                List<Person> targets = new List<Person>();

                foreach (var item in data)
                {
                    Person p = new Person()
                    {
                        FirstName = $"{item.Candidate.PrimaryEmail}-{item.Candidate.FirstName} {item.Candidate.LastName}",
                        Id = item.Candidate.Id
                    };

                    targets.Add(p);
                }

                return Json(new SelectList(targets, "Id", "FirstName"));
            }
            else
            {
                return Json(new SelectList(lkpDal.GetAllPersonTypes().Where(x => x.Id.ToString() == id1), "Id", "Name"));
            }

        }

        public JsonResult GetPersonByPersonTypeId(string pTypeId)
        {

            var data = cDal.GetAllPerson().Where(x => x.PersonTypeId == pTypeId).ToList();

            List<Person> targets = new List<Person>();

            foreach (var item in data)
            {
                Person p = new Person()
                {
                    FirstName = $"{item.PrimaryEmail}-{item.FirstName} {item.LastName}",
                    Id = item.Id
                };

                targets.Add(p);
            }

            return Json(new SelectList(targets, "Id", "FirstName"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendNotification(MailoutCompose notification)
        {
            if (ModelState.IsValid)
            {
                var arrPersonId = notification.PersonListId.Split(',');
                foreach (var pid in arrPersonId)
                {
                    Dictionary<string, string> param = new Dictionary<string, string>();

                    param.Add("Name", $"{cDal.searchPersonById(int.Parse(pid)).FirstName} {cDal.searchPersonById(int.Parse(pid)).LastName}");
                    param.Add("Message", notification.Body);

                    string html = eService.GetHtml(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + "tmpDefault.html", param);

                    mxDal.PushNotification(cDal.searchPersonById(int.Parse(pid)), param, 13, cDal.searchPersonById(int.Parse(pid)).PrimaryEmail, html);
                }

                return RedirectToAction("ComposeEmail");
            }

            return RedirectToAction("ComposeEmail");
        }

        public ActionResult GetNotificationPreview(string content)
        {
            StringBuilder sb = new StringBuilder();
            var rawString = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + "tmpDefault.html");
            rawString = rawString.Replace("{#MESSAGE#}", content);
            return Json(rawString);
        }

        public ActionResult SendTestNotification(string content, string toEmail, string subject)
        {
            if (!string.IsNullOrEmpty(toEmail) && !string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(subject))
            {
                var response = eService.Default(toEmail, ConfigurationManager.AppSettings["SMTP_FROM"], $"{sm.UserSession.Person.FirstName} {sm.UserSession.Person.LastName}", subject, content, AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + "tmpDefault.html");
            }

            return Json("DONE");
        }

        public ActionResult GetMailoutList()
        {
            return View(mxDal.GetAllMailouts());
        }
    }
}