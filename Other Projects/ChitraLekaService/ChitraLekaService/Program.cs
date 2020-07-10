using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChitraLekaDAL;
using ChitraLekaService.Notification;
using System.Configuration;

namespace ChitraLekaService
{
    class Program
    {
        private static EmailService eService = new EmailService(false, System.Web.HttpContext.Current);
        
        static void Main(string[] args)
        {
            eService.EmailBySMTP("ramanranjan1979@gmail.com", "info@modizson.com", "body", "subject");
            Console.ReadLine();

        }

        
    }
}
