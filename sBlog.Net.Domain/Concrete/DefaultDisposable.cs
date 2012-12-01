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
using System.Data.Linq;

namespace sBlog.Net.Domain.Concrete
{
    public abstract class DefaultDisposable
    {
        protected readonly DataContext context;

        private bool _disposed;
        private readonly object _disposeLock = new object();

        protected DefaultDisposable()
        {
            context = new DataContext(ApplicationDomainConfiguration.ConnectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (_disposeLock)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }

                    _disposed = true;
                }
            }
        }
    }
}
