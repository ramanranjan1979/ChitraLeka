using ChitraLeka.ViewModel;
using ChitraLeka.ViewModel.Contact;
using ChitraLeka.ViewModel.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ChitraLeka.Session
{
    public class SessionManager
    {

        public Security UserSession
        {
            get
            {
                return (Security)HttpContext.Current.Session["Security"];
            }
            set
            {
                HttpContext.Current.Session["Security"] = value;

            }
        }

    }


}