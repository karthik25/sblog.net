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
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using sBlog.Net.MetaData.MetaData;

namespace sBlog.Net.Areas.Admin.Models
{
    [MetadataType(typeof(AdminSettingsViewModelMetaData))]
    public class AdminSettingsViewModel : AdminBaseViewModel
    {
        public string BlogName { get; set; }
        public string BlogCaption { get; set; }
        public string BlogTheme { get; set; }        
        public List<SelectListItem> BlogThemes { get; set; }
        public bool BlogSyntaxHighlighting { get; set; }        
        public bool BlogSocialSharing { get; set; }        
        public int PostsPerPage { get; set; }
        public int ManageItemsPerPage { get; set; }
        public bool AkismetEnabled { get; set; }
        public bool AkismetDeleteSpam { get; set; }
        public string AkismetKey { get; set; }
        public string AkismetUrl { get; set; }
        public string AdminEmailAddress { get; set; }
        public string BlogSmtpAddress { get; set; }
        public string BlogSmtpPassword { get; set; }
        public bool BlogErrorAction { get; set; }
    }
}
