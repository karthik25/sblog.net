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
namespace sBlog.Net.Akismet.Entities
{
    public class RequestData
    {
        public string Blog { get; set; }
        public string UserIp { get; set; }
        public string UserAgent { get; set; }
        public string Referrer { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
