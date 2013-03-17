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
namespace sBlog.Net.Domain.Interfaces
{
    public interface ISettings
    {
        // properties
        // to free up untyped calls to GetValue
        string BlogName { get; set; }
        string BlogCaption { get; set; }
        int BlogPostsPerPage { get; set; }
        string BlogTheme { get; set; }
        bool BlogSocialSharing { get; set; }
        bool BlogSyntaxHighlighting { get; set; }
        string BlogSyntaxTheme { get; set; }
        string BlogSyntaxScripts { get; set; }
        bool BlogAkismetEnabled { get; set; }
        string BlogAkismetKey { get; set; }
        string BlogAkismetUrl { get; set; }
        bool BlogAkismetDeleteSpam { get; set; }
        int BlogSocialSharingChoice { get; set; }
        bool BlogSiteErrorEmailAction { get; set; }
        string BlogAdminEmailAddress { get; set; }
        string BlogSmtpAddress { get; set; }
        string BlogSmtpPassword { get; set; }
        bool InstallationComplete { get; set; }
        int ManageItemsPerPage { get; set; }
        bool DisqusEnabled { get; set; }
        string BlogDisqusShortName { get; set; }

        string BlogDbVersion { get; set; }

        // raw methods
        string GetValue(string key);
        bool UpdateSettings(string key, string value);
    }
}
