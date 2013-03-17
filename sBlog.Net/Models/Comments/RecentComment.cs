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
using System;

namespace sBlog.Net.Models.Comments
{
    public class RecentComment
    {
        private string _commentContent;
        public string CommentContent 
        {
            get
            {
                return _commentContent.Length > 25 ? _commentContent.Substring(0, 25) + "..." : _commentContent;
            }
            set
            {
                _commentContent = value;
            }
        }
        
        public DateTime PostAddedDate { get; set; }
        public string PostUrl { get; set; }
        public byte EntryType { get; set; }

        public bool DisqusComment { get; set; }
    }
}
