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
using sBlog.Net.MetaData.Attributes;

namespace sBlog.Net.MetaData.MetaData
{
    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public class RegisterModelMetaData
    {
        [DisplayName("Display name")]
        public object UserDisplayName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [DisplayName("User name")]
        [RegularExpression(@"[A-Za-z0-9]+(?:[_-][A-Za-z0-9]+)*", ErrorMessage = "Username entered is invalid")]
        [StringLength(50, ErrorMessage = "User name cannot be more than 50 characters")]
        public object UserName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        [RegularExpression(@"^((([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Email address entered is invalid")]
        [StringLength(50, ErrorMessage = "Email cannot be more than 50 characters")]
        public object Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [ValidatePasswordLength(6, 20)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Password entered is invalid")]
        public object Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [ValidatePasswordLength(6, 20)]
        [DataType(DataType.Password)]
        [DisplayName("Confirm new password")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Confirm password entered is invalid")]
        public object ConfirmPassword { get; set; }

        [Required]
        public object NewUserTicket { get; set; }

        [Required]
        public object UserID { get; set; }
    }
}
