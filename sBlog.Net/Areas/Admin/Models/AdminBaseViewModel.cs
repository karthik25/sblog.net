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
using sBlog.Net.Models;

namespace sBlog.Net.Areas.Admin.Models
{
    public class AdminBaseViewModel
    {
        public PagingInformation PagingInfo { get; set; }
        public bool UpdateStatus { get; set; }
        public string OneTimeCode { get; set; }
        public string Title { get; set; }
    }
}
