using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace ChitraLekaService.Notification
{
    public class ServiceBase
    {
        private bool _testMode;
        private HttpContext _currentContext;

        public ServiceBase(bool TestMode, HttpContext Context)
        {
            _testMode = TestMode;
            _currentContext = Context;
        }

        protected internal void EmailBySMTP(string toEmailAddress, string fromAddress, string body, string subject)
        {

            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress(fromAddress);
            m.To.Add(_testMode ? ConfigurationManager.AppSettings["SMTP_TO"] : toEmailAddress);
            m.Subject = subject;
            m.Body = body;
            sc.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
            string str1 = "gmail.com";
            if (fromAddress.ToLower().Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTP_FROM"], ConfigurationManager.AppSettings["SMTP_Password"]);
                    sc.EnableSsl = true;
                    m.IsBodyHtml = true;
                    sc.Send(m);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTP_FROM"], ConfigurationManager.AppSettings["SMTP_Password"]);
                    sc.EnableSsl = false;
                    m.IsBodyHtml = true;
                    sc.Send(m);

                }
                catch (Exception ex)
                {

                }
            }
        }

        protected internal void EmailBySMTP(string toEmailAddress, string body, string subject)
        {

            MailMessage message = new MailMessage(ConfigurationManager.AppSettings["SMTP_FROM"], _testMode ? ConfigurationManager.AppSettings["SMTP_TO"] : toEmailAddress);
            message.Body = body;
            message.IsBodyHtml = true;
            message.Subject = subject;
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTP_HOST"]);
            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTP_UserName"], ConfigurationManager.AppSettings["SMTP_Password"]);
            smtp.Send(message);
        }
    }

    public class EmailResponse
    {
        public string Content { get; set; }

        public string Subject { get; set; }

        public string To { get; set; }

        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

    }


    public class EmailService : ServiceBase
    {

        private bool _testMode;
        private HttpContext _currentContext;

        public EmailService(bool TestMode, HttpContext Context) : base(TestMode, Context)
        {
            _testMode = TestMode;
            _currentContext = Context;
        }



        public EmailResponse Default(string to, string from, string name, string subject, string message, string templatePath)
        {
            EmailResponse response = new EmailResponse();
            StringBuilder sb = new StringBuilder();

            string key = "TEMP_DEFAULT_MESSAGE";
            var rawString = string.Empty;

            if (_currentContext == null)
            {
                rawString = System.IO.File.ReadAllText(templatePath);
            }
            else
            {
                if (HttpContext.Current.Cache[key] == null)
                {
                    HttpContext.Current.Cache.Add(key, System.IO.File.ReadAllText(templatePath), null, Cache.NoAbsoluteExpiration, new TimeSpan(45, 0, 0), CacheItemPriority.Normal, null);
                }
                rawString = (string)HttpContext.Current.Cache[key];
            }

            rawString = rawString.Replace("{#NAME#}", name);
            rawString = rawString.Replace("{#MESSAGE#}", message);

            sb.Append(rawString);

            response.Content = sb.ToString();
            response.To = _testMode ? ConfigurationManager.AppSettings["SMTP_TO"] : to;
            response.Subject = subject;
            response.Error = false;
            response.ErrorMessage = string.Empty;

            try
            {
                EmailBySMTP(to, from, sb.ToString(), subject);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }

        public string GetHtml(string templatePath, Dictionary<string, string> Parameters)
        {
            var rawString = System.IO.File.ReadAllText(templatePath);
            foreach (var parameter in Parameters)
            {
                if (rawString.Contains("{#" + parameter.Key.ToUpper() + "#}"))
                {
                    rawString = rawString.Replace("{#" + parameter.Key.ToUpper() + "#}", parameter.Value);
                }
            }

            return rawString;
        }
    }
}
