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
using sBlog.Net.MetaData.MetaData;
using System.ComponentModel.DataAnnotations;

namespace sBlog.Net.Models.Account
{
    [MetadataType(typeof(LogOnModelMetaData))]
    public class LogOnModel : AccountBaseViewModel
    {
        public bool RememberMe { get; set; }
    }
}