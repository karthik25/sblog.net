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
    public class AdminSettingsViewModelMetaData
    {
        [Required(ErrorMessage = "Blog name is required")]
        [DisplayName("Blog name")]
        public object BlogName { get; set; }

        [Required(ErrorMessage = "Blog caption is required")]
        [DisplayName("Blog caption")]
        public object BlogCaption { get; set; }

        [DisplayName("Blog theme")]
        public object BlogTheme { get; set; }

        [DisplayName("Enable syntax highlighter?")]
        public object BlogSyntaxHighlighting { get; set; }

        [DisplayName("Provide (social) sharing options?")]
        public object BlogSocialSharing { get; set; }

        [Required(ErrorMessage = "Posts per page is required")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Posts per page entered is invalid")]
        [DisplayName("Number of posts per page")]
        public object PostsPerPage { get; set; }

        [Required(ErrorMessage = "Items per page is required")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Items per page entered is invalid")]
        [DisplayName("Number of items per page (admin section)")]
        public object ManageItemsPerPage { get; set; }

        [DisplayName("Enable akismet to catch/eliminate spam")]
        public object AkismetEnabled { get; set; }

        [DisplayName("Delete comments identified as spam? (leave it unchecked to just move the comment to the spam queue)")]
        public object AkismetDeleteSpam { get; set; }

        [DisplayName("Akismet key")]
        public object AkismetKey { get; set; }

        [DisplayName("Enter akismet url (your blog's root url)")]
        public object AkismetUrl { get; set; }

        [DisplayName("Enter admin email address")]
        [Required(ErrorMessage = "Admin email address is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^((([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-zA-Z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-zA-Z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Admin email address entered is invalid")]
        public object AdminEmailAddress { get; set; }

        [DisplayName("Enter the smtp address")]
        [Required(ErrorMessage = "Smtp address is required")]
        public object BlogSmtpAddress { get; set; }

        [DisplayName("Enter the smtp password (if any)")]
        public object BlogSmtpPassword { get; set; }

        [DisplayName("Enable sending emails when there are site errors")]
        public object BlogErrorAction { get; set; }
    }
}
