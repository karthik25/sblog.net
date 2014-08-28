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
namespace sBlog.Net.Areas.Admin.Models
{
    public class AdminDashboardViewModel
    {
        public int PostCount { get; set; }
        public int PagesCount { get; set; }
        public int CategoriesCount { get; set; }
        public int TagsCount { get; set; }

        public int AllCommentsCount { get; set; }
        public int ApprovedCount { get; set; }
        public int PendingCount { get; set; }
        public int SpamCount { get; set; }

        public bool CanView { get; set; }

        public string BlogName { get; set; }

        public string DisplayName { get; set; }
    }
}
