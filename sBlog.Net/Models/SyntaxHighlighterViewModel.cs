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
using System.Web.Mvc;
using System.Collections.Generic;
using sBlog.Net.Areas.Admin.Models;

namespace sBlog.Net.Models
{
    public class SyntaxHighlighterViewModel : AdminBaseViewModel
    {
        public CheckBoxListViewModel Brushes { get; set; }
        public List<SelectListItem> AvailableThemes { get; set; }
        public string Theme { get; set; }
        public IDictionary<string, object> EditThemeAttributes { get; set; }
        public bool IsEnabled { get; set; }
        public bool CanEnable { get; set; }
    }
}
