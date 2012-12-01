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
using sBlog.Net.Domain.Entities;
using System.Collections.Generic;
using sBlog.Net.Areas.Admin.Models.Comments;

namespace sBlog.Net.FluentExtensions
{
    public static class CommentInfoExtensions
    {
        public static List<CommentInfo> GetCommentLinks(this List<CommentEntity> commentEntities, List<PostEntity> posts, int status)
        {
            var infos = new List<CommentInfo>();
            commentEntities = status == int.MaxValue ? commentEntities : commentEntities.Where(c => c.CommentStatus == status).ToList();
            commentEntities.ForEach(c => infos.Add(GetCommentInfo(posts, c, status)));
            return infos;
        }

        private static CommentInfo GetCommentInfo(IEnumerable<PostEntity> posts, CommentEntity commentEntity, int status)
        {
            var info = new CommentInfo { Comment = commentEntity, Post = posts.Single(p => p.PostID == commentEntity.PostID) };
            var links = new List<CommentLink>();

            CommentLinkType.GetTypes(commentEntity.CommentStatus).ForEach(t =>
            {
                var methodName = status == int.MaxValue ? "CommentPartial" : "CommentPartialReplacer";
                links.Add(new CommentLink { CommentID = commentEntity.CommentID, PostID = commentEntity.PostID, LinkText = t.Description, ActionMethod = methodName, CommentStatus = t.Status });
            });

            info.Links = links;
            return info;
        }
    }
}
