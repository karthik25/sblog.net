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

namespace sBlog.Net.Domain.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(string cacheID, Func<T> getItemCallback) where T : class;
    }
}
