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
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Extensions
{
    public static class ExceptionExtensions
    {
        public static ApplicationErrorEntity ToApplicationErrorEntity(this Exception exception)
        {
            var applicationErrorEntity = new ApplicationErrorEntity
            {
                ErrorDateTime = DateTime.Now,
                ErrorMessage = exception.Message.Replace("'","''"),
                ErrorDescription = exception.ToString().Replace("'","''")
            };
            return applicationErrorEntity;
        }
    }
}
