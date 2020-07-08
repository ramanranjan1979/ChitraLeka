using System.Web;
using System.Web.Mvc;
using ChitraLeka.Common;
using ChitraLekaDAL;
using ChitraLeka.Session;

namespace ChitraLeka.Filter
{
    public class AuthLogin : AuthorizeAttribute
    {
        private SecurityDal dal = new SecurityDal();

        //Use to filter out different account type
        public string AccountType { get; set; } = string.Empty;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            SessionManager sm = new SessionManager();

            if (sm.UserSession == null)
            {
                return false;
            }
            else
            {
                if (sm.UserSession.IsActive == true)
                {
                    if (AccountType == string.Empty)
                    {
                        return true;
                    }
                    else
                    {
                        switch (AccountType.ToLower())
                        {
                            case "admin":
                                if(sm.UserSession.RoleNameList.Contains(AccountType.ToLower()))
                                { 
                                    return true;
                                }
                                else
                                {
                                    //var email = dal.GetMemberEmailByMemberId(sm.UserSession.Id);
                                    //_mDal.LogSocialAction(email, "LOG OUT", sm.UserSession.MemberId, $"Member Tried to access admin area");

                                    sm.UserSession = null;

                                    return false;
                                }
                            default:
                                return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult("Login", new System.Web.Routing.RouteValueDictionary { });
        }
    }
}