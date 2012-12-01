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
using System.Linq;
using System.Collections.Generic;

namespace sBlog.Net.Areas.Admin.Models.Comments
{
    public class CommentLinkType
    {
        public int Status { get; set; }
        public string Description { get; set; }

        public static List<CommentLinkType> GetTypes(int excludeStatus)
        {
            var types = new List<CommentLinkType>
                            {
                                new CommentLinkType {Status = 0, Description = "approve"},
                                new CommentLinkType {Status = 1, Description = "move to pending"},
                                new CommentLinkType {Status = 2, Description = "mark as spam"},
                                new CommentLinkType {Status = -1, Description = "move to trash"}
                            };
            return types.Except(types.Where(t => t.Status == excludeStatus)).ToList();
        }
    }
}
