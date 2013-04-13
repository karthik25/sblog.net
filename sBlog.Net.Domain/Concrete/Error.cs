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
using System.Data.Entity;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Extensions;

namespace sBlog.Net.Domain.Concrete
{
    public class Error : System.Data.Entity.DbContext, IError
    {
        public IDbSet<ApplicationErrorEntity> Errors { get; set; }

        public Error()
            : base("AppDb")
        {
            
        }

        public void InsertException(Exception exception)
        {
            try
            {
                var errorEntity = exception.ToApplicationErrorEntity();
                Errors.Add(errorEntity);
                SaveChanges();
            }
            catch
            {
                // eat the exception, this is the end of it !!!
            }
        }
    }
}
