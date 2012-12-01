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
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Extensions;
using System.Data.Linq;

namespace sBlog.Net.Domain.Concrete
{
    public class Error : IError
    {
        private readonly Table<ApplicationErrorEntity> _errorsTable;
        private readonly DataContext _context;
        private readonly string _connectionString;

        public Error()
        {
            _connectionString = ApplicationDomainConfiguration.ConnectionString;
            _context = new DataContext(_connectionString);
            _errorsTable = _context.GetTable<ApplicationErrorEntity>();
        }

        public void InsertException(Exception exception)
        {
            try
            {
                var errorEntity = exception.ToApplicationErrorEntity();
                _errorsTable.InsertOnSubmit(errorEntity);
                _context.SubmitChanges();
            }
            catch
            {
                // eat the exception, this is the end of it !!!
            }
        }
    }
}
