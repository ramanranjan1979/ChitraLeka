using ChitraLeka.Filter;
using ChitraLeka.Session;
using ChitraLeka.ViewModel.Contact;
using ChitraLekaDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class SecurityController : BaseController
    {
        private SecurityDal sDal = new SecurityDal();

        public ActionResult Index()
        {
            var data = sDal.GetAllRolesLogin();
            return View(data);
        }

        public ActionResult CreateUserLogin()
        {
            ViewModel.Security.NewPeronLogin nPL = new ViewModel.Security.NewPeronLogin();
            nPL.people = new SelectList(sDal.GetPendingLogin().OrderBy(o => o.PrimaryEmail), "Id", "PrimaryEmail");
            nPL.roles = new SelectList(sDal.GetAllRoles(), "Id", "Name");
            return View(nPL);
        }

        public ActionResult SaveUserLogin(ViewModel.Security.NewPeronLogin nPL)
        {
            if (ModelState.IsValid)
            {
                nPL.UserName = sDal.GenerateUserName(nPL.PersonId);
                nPL.Password = Common.Helper.EncryptString(ChitraLeka.Common.Helper.RandomString(10));
                sDal.SaveUserLogin(nPL);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeMyPassword(int loginId, string password, string newpassword, string confirmpassword)
        {
            ChitraLeka.ViewModel.Account.LoginStatus ls = new ViewModel.Account.LoginStatus() { Success = false, Message = "Password cannot be changed" };
            var uname = new SessionManager().UserSession.UserName;
            ls = sDal.ValidateUser(uname, password);

            if (!ls.Success)
            {
                ls.Message = "Incorrect old password";
                return Json(ls);
            }


            if (string.Equals(Common.Helper.EncryptString(newpassword), Common.Helper.EncryptString(password)))
            {
                ls.Success = false;
                ls.Message = "Old passwod and new password cannot be same";
            }
            else
            {
                if (ls.Success)
                {
                    sDal.UpdateUserLogin(loginId, "Password", Common.Helper.EncryptString(newpassword));
                    ls.Message = "Password has been channged";
                }
            }
            return Json(ls);
        }


        public ActionResult SystemHealth()
        {
            var data = sDal.GetSystemLog();
            return View(data);
        }
    }
}