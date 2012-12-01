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
using sBlog.Net.Areas.Admin.Models.Comments;

namespace sBlog.Net.Areas.Admin.Models
{
    public class AdminCommentViewModel : AdminBaseViewModel
    {
        public List<CommentInfo> Comments { get; set; }
        public string Type { get; set; }
        public int AllCommentsCount { get; set; }
        public int ApprovedCommentsCount { get; set; }
        public int PendingCommentsCount { get; set; }
        public int SpamCommentsCount { get; set; }
        public int TrashCommentsCount { get; set; }
    }
}
