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

namespace sBlog.Net.Models.Comments
{
    public class CommentViewModel
    {
        public PostEntity Post { get; set; }
        public CommentEntity Comment { get; set; }
        public string DisplayName { get; set; }
        public string IsHuman { get; set; }
    }
}
