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
using sBlog.Net.Domain.Entities;
using sBlog.Net.Models;

namespace sBlog.Net.Areas.Admin.Models
{
    public class PostViewModel : AdminBaseViewModel
    {
        public PostEntity Post { get; set; }
        public string Tags { get; set; }
        public CheckBoxListViewModel Categories { get; set; }
        public bool IsNewPostOrPage { get; set; }
        public bool SharingEnabled { get; set; }

        public bool AjaxSaved { get; set; }
    }
}
