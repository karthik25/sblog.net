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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sBlog.Net.MetaData.MetaData
{
    public class LogOnModelMetaData
    {
        [Required(ErrorMessage = "username is required")]
        [DisplayName("username")]
        [RegularExpression(@"[A-Za-z0-9]+(?:[_-][A-Za-z0-9]+)*", ErrorMessage = "username entered is invalid")]
        public object UserName { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        [DisplayName("password")]
        public object Password { get; set; }

        [DisplayName("remember me?")]
        public object RememberMe { get; set; }
    }
}
