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
namespace sBlog.Net.Areas.Admin.Models.Comments
{
    public class CommentLink
    {
        public string LinkText { get; set; }
        public string ActionMethod { get; set; }
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int CommentStatus { get; set; }        
    }
}
