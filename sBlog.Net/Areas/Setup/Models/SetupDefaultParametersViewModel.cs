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

namespace sBlog.Net.Areas.Setup.Models
{
    [MetadataType(typeof(SetupDefaultParametersViewModelMetaData))]
    public class SetupDefaultParametersViewModel
    {
        public string BlogName { get; set; }
        public string AkismetUrl { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string RandomizedCode { get; set; }
    }
}
