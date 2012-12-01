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
namespace sBlog.Net.Areas.Admin.Models.JsonEntities
{
    public class CommentEntry
    {
        public int CommentID { get; set; }
        public bool DeleteStatus { get; set; }
        public string DeleteStatusString { get; set; }
    }
}
