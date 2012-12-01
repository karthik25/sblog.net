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
using System.Web;
using sBlog.Net.Domain.Interfaces;
using System.Web.Caching;
using sBlog.Net.Infrastructure;

namespace sBlog.Net.Services
{
    public class CacheService : ICacheService
    {
        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, 
                                                 item, 
                                                 null, 
                                                 DateTime.Now.AddMinutes(ApplicationConfiguration.CacheDuration), 
                                                 Cache.NoSlidingExpiration);
            }
            return item;
        }
    }
}
