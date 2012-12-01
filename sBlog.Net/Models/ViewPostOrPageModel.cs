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

namespace sBlog.Net.Models
{
    public class ViewPostOrPageModel
    {
        public PostEntity Post { get; set; }
        public bool BlogSharingEnabled { get; set; }
        public int SharingType { get; set; }
        public PostEntity NextPost { get; set; }
        public PostEntity PreviousPost { get; set; }
        public bool UserCanEdit { get; set; }
        public string BlogName { get; set; }
        public string BlogCaption { get; set; }
        public CommentEntity CommentEntity { get; set; }
    }
}
