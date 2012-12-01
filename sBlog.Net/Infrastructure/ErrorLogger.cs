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
using sBlog.Net.DependencyManagement;

namespace sBlog.Net.Infrastructure
{
    public class ErrorLogger
    {
        private readonly Exception _exception;

        public ErrorLogger(Exception exception)
        {
            _exception = exception;
        }

        public void Log()
        {
            LogError();
            SendEmail();
        }

        /// <summary>
        /// Sends an email if enabled in blog settings. It expects an email address in blog settings and also an smtp server
        /// If they are invalid, it fails silently
        /// </summary>
        private void SendEmail()
        {
            var settingsRepository = InstanceFactory.CreateSettingsInstance();
            var emailError = settingsRepository.BlogSiteErrorEmailAction;

            if (emailError)
            {
                Emailer.SendMessage(settingsRepository.BlogAdminEmailAddress, settingsRepository.BlogAdminEmailAddress,
                                    string.Format("An exception occurred :: {0}", settingsRepository.BlogName), 
                                    string.Format("Date: {0} Error: {1}", DateTime.Now, _exception));
            }
        }

        private void LogError()
        {
            var errorRepository = InstanceFactory.CreateErrorInstance();
            errorRepository.InsertException(_exception);
        }
    }
}
