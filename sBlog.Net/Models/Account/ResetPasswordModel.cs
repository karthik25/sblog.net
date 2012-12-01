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
    [MetadataType(typeof(ResetPasswordModelMetaData))]
    public class ResetPasswordModel : AccountBaseViewModel
    {
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}