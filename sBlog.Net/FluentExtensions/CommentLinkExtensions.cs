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

namespace sBlog.Net.FluentExtensions
{
    public static class CommentLinkExtensions
    {
        public static List<CommentLink> GetLinks(this CommentLink commentLink)
        {
            var links = new List<CommentLink>();
            CommentLinkType.GetTypes(commentLink.CommentStatus).ForEach(t => links.Add(new CommentLink { CommentID = commentLink.CommentID, PostID = commentLink.PostID, LinkText = t.Description, ActionMethod = "CommentPartial", CommentStatus = t.Status }));
            return links;
        }
    }
}
