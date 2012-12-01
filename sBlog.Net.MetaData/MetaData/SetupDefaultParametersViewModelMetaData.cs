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
    public class SetupDefaultParametersViewModelMetaData
    {
        [Required(ErrorMessage = "Blog name is required")]
        [DisplayName("Blog name")]
        public object BlogName { get; set; }

        [Required(ErrorMessage = "Blog's root url is required")]
        [DisplayName("Enter your blog's root url")]
        public object AkismetUrl { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Enter your password")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Password entered is invalid")]
        public object Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DisplayName("Re-enter your password")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Confirm password entered is invalid")]
        public object ConfirmPassword { get; set; }
    }
}
