using System.Web.Mvc;
using ChitraLeka.ViewModel;
using ChitraLekaDAL;
using ChitraLeka.ViewModel.Account;
using ChitraLeka.Filter;
using ChitraLeka.Common;
using ChitraLeka.Session;
using ChitraLeka.ViewModel.Contact;

namespace ChitraLeka.Controllers
{
    [AuthLogin(AccountType = "admin")]
    public class AccountController : BaseController
    {
        private SecurityDal sDal = new SecurityDal();
        private ContactDal cDal = new ContactDal();
        private SessionManager sm = new SessionManager();

        public AccountController()
        {
        }

     

        public ActionResult MyAccount()
        {
            Person myProfile = sm.UserSession.Person;
            myProfile.PersonEmail = cDal.SearchPersonEmailAddressesByPersonId(myProfile.Id);
            myProfile.PersonAddress = cDal.SearchPersonAddressesByPersonId(myProfile.Id);
            myProfile.PersonContactNumber = cDal.SearchPersonContactNumbersByPersonId(myProfile.Id);

            return View(myProfile);
        }

        

    }
}