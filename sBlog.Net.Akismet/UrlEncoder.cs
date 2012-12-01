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
using System.Net;

namespace sBlog.Net.Akismet
{
    public static class UrlEncoder
    {
        public static string UrlEncode(string data)
        {
            return WebUtility.HtmlEncode(data);
        }
    }
}
