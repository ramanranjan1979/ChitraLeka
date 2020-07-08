using ChitraLeka.Session;
using ChitraLeka.ViewModel.Account;
using ChitraLeka.ViewModel.Contact;
using ChitraLekaDAL;
using ChitraLekaService.Notification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChitraLeka.Controllers
{
    public class HomeController : BaseController
    {
        private SecurityDal sDal = new SecurityDal();
        private ContactDal cDal = new ContactDal();
        private NotificationDal nDal = new NotificationDal();
        private SessionManager sm = new SessionManager();
        private NotificationDal mxDal = new NotificationDal();
        private EmailService eService = new EmailService(bool.Parse(ConfigurationManager.AppSettings["TESTMODE"]), System.Web.HttpContext.Current);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Disclaimer()
        {
            return View();
        }

        public ActionResult Aboutus()
        {
            return View();
        }

        public ActionResult Classes()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult Contactus()
        {
            return View();
        }

        public ActionResult Enquiry()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            sm.UserSession = null;

            return RedirectToAction("Login", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View(new ForgotPassword());
        }

        [HttpPost]
        public ActionResult ResetPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                bool canRequest = true;

                var person = sDal.GetPersonByUserName(model.UserName);

                if (person == null)
                {
                    ViewBag.Message = "Username is invalid";
                    canRequest = false;
                }
                else
                {
                    if (person.PrimaryEmail.ToLower() == model.EmailAddress.ToLower() == false)
                    {
                        ViewBag.Message = $"Username {model.UserName} cannot be found with emailaddress {model.EmailAddress}";
                        canRequest = false;
                    }
                }
                if (canRequest)
                {
                    if (sDal.GetPeronSecurityCodeBySecurityType(person.Id, "FORGOTPASSWORD").Where(x => x.ExpiredOn.HasValue == false).Count() >= 2)
                    {
                        ViewBag.Message = "Looks like, We had already sent you the password reset instructions.If you haven't received an email from us, please check your spam or junk mail folder.";
                        canRequest = false;
                    }
                }

                if (canRequest)
                {
                    var code = sDal.RaisePersonSecurityRequest(person.Id, "FORGOTPASSWORD");

                    string resetLink = string.Format("http://{0}/verifypassword/{1}", System.Web.HttpContext.Current.Request.Url.Authority, code);

                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add("NAME", $"{person.FirstName} {person.LastName}");
                    param.Add("SECURITYCODE", code);
                    param.Add("RESETLINK", resetLink);
                    string html = eService.GetHtml(AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["mxTemplatePath"] + "tmpForgotPassword.html", param);

                    mxDal.PushNotification(person, param, 3, person.PrimaryEmail, html);
                }

                ViewBag.ResetRequestProcessesd = true;
                return View("ForgotPassword", new ForgotPassword());
            }
            else
            {
                return View("ForgotPassword", new ForgotPassword());
            }

          
        }

        [HttpPost]
        public JsonResult ValidateUser(string UserName, string Password)
        {
            LoginStatus status = sDal.ValidateUser(UserName, Password);
            if (status.Success)
            {
                sm.UserSession = status.LoggedInPerson;
            }
            return Json(status);
        }

        [HttpGet]
        public ActionResult Login()
        {
            Login lVM = new Login();

            return View(lVM);
        }

        public FileStreamResult Track(int? id)
        {

            var q = nDal.GetMailoutQueueById(id.Value);

            if (q != null)
            {
                nDal.TrackMailout(q, GetUserIp());
            }
            //our image stored in the Img folder    
            var image = new Bitmap(Server.MapPath(@"\Images\btn-close.gif"));

            //the counting logic : for this example i will write a file
            //in the image folder for each view
            //StreamWriter w = new StreamWriter(Server.MapPath(@"\Images\") + id + ".txt");
            //w.Close();

            //create a new memory stream
            MemoryStream stream = new MemoryStream();

            //save the image in the stream
            image.Save(stream, ImageFormat.Gif);

            //This will rewind the stream to the beginning of the saved image.
            //Otherwise the stream will be positioned at
            //the end of the stream and nothing is sent to the receiver.
            stream.Seek(0, SeekOrigin.Begin);

            //return the filestreamresult
            return new FileStreamResult(stream, "image/gif");
        }


        [HttpGet]
        public ActionResult verifypassword(string Code)
        {
            ChitraLeka.ViewModel.Security.ResetPassword rpVM = new ViewModel.Security.ResetPassword();

            var personSecurityCode = sDal.GetPersonSecurityCodeByCode(Code);

            if (personSecurityCode.ExpiredOn != null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                rpVM.Person = personSecurityCode.Person;                
            }

            return View("ResetPassword", rpVM);
        }

        [HttpPost]
        public ActionResult ApplySecurityCodeForRestPassword(ChitraLeka.ViewModel.Security.ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                sDal.ApplyPersonSecurityCode(model.Person.Id,model.SecurityCode);
            }

            return RedirectToAction("ThankYouForChangingPassword", "Home");
        }

        public ActionResult ThankYouForChangingPassword()
        {
            return View();
        }


    }
}