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
using System.Configuration;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using sBlog.Net.Configuration;
using sBlog.Net.MetaData.MetaData;

namespace sBlog.Net.Areas.Admin.Models
{
    [MetadataType(typeof(AdminSettingsViewModelMetaData))]
    public class AdminSettingsViewModel : AdminBaseViewModel
    {
        public string BlogName { get; set; }
        public string BlogCaption { get; set; }
        public string BlogTheme { get; set; }        
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
        public bool DisqusEnabled { get; set; }
        public string DisqusShortName { get; set; }
        public string EditorType { get; set; }
        
        public List<SelectListItem> BlogThemes { get; set; }
        public List<SelectListItem> EditorTypes { get; set; }

        public string SelectedTheme
        {
            get { return BlogStaticConfig.Theme.SelectedTheme; }
        }

        private static readonly SblogNetSettingsConfiguration BlogStaticConfig = ConfigurationManager.GetSection("sblognetSettings")
                                                     as SblogNetSettingsConfiguration;
    }
}
