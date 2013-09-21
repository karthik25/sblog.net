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
using sBlog.Net.DB.Enumerations;

namespace sBlog.Net.DB.Services
{
    public class SetupStatus
    {
        public SetupStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
