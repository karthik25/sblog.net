#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion

using System;
using System.Net;
using System.Net.Mail;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.DependencyManagement;
using sBlog.Net.Domain.Utilities;

namespace sBlog.Net.Infrastructure
{
    public static class Emailer
    {
        public static bool SendMessage(string subjectMsg, string emailMessage)
        {
            return SendMessage(SiteFromAddress, SiteFromAddress, subjectMsg, emailMessage);
        }

        public static bool SendMessage(string toAddress, string subjectMsg, string emailMessage)
        {
            return SendMessage(SiteFromAddress, toAddress, subjectMsg, emailMessage);
        }

        public static bool SendMessage(string fromAddress, string toAddress, string subjectMsg, string emailMessage)
        {
            try
            {
                var message = new MailMessage {From = new MailAddress(fromAddress)};
                message.To.Add(new MailAddress(toAddress));
                message.Subject = subjectMsg;
                message.IsBodyHtml = true;
                message.Body = emailMessage;

                var smtpClient = new SmtpClient(SiteSmtpAddress);

                var smtpPassword = SiteSmtpPassword;
                if (!string.IsNullOrEmpty(smtpPassword))
                {
                    smtpClient.Credentials = new NetworkCredential(fromAddress, smtpPassword);
                }

                smtpClient.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }        

        private static string SiteFromAddress
        {
            get
            {
                var instance = GetSettingsInstance();
                return instance.BlogAdminEmailAddress;
            }
        }

        private static string SiteSmtpAddress
        {
            get
            {
                var instance = GetSettingsInstance();
                return instance.BlogSmtpAddress;
            }
        }

        private static string SiteSmtpPassword
        {
            get 
            { 
                var instance = GetSettingsInstance();
                var smtpPassword = instance.BlogSmtpPassword;
                if (!string.IsNullOrEmpty(smtpPassword))
                {
                    smtpPassword = TripleDES.DecryptString(smtpPassword);
                }
                return smtpPassword;
            }
        }

        private static ISettings GetSettingsInstance()
        {
            return InstanceFactory.CreateSettingsInstance();
        }
    }
}