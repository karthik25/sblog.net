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
/* Comment.cs 
 * 
 * This class extends the DefaultDisposable class,
 * Which implements the IDisposable interface for this class.
 * 
 * If you modify the class to add more disposable managed
 * resources, you can remove DefaultDisposable and implement
 * the Dispose() method yourself
 * 
 * */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Concrete
{
    public class Comment : System.Data.Entity.DbContext, IComment
    {
        public IDbSet<CommentEntity> Comments { get; set; }

        public Comment()
            : base("AppDb")
        {
            
        }
        
        public List<CommentEntity> GetAllComments()
        {
            return Comments.OrderByDescending(c => c.CommentPostedDate).ToList();
        }

        public List<CommentEntity> GetAllComments(int status)
        {
            return Comments.Where(c => c.CommentStatus == status).OrderByDescending(c => c.CommentPostedDate).ToList();
        }

        public List<CommentEntity> GetCommentsByPostID(int postID)
        {
            return Comments.Where(c => c.PostID == postID && (c.CommentStatus == 0 || c.CommentStatus == 1)).OrderByDescending(c => c.CommentPostedDate).ToList();
        }

        public void DeleteCommentByCommentID(int commentID)
        {
            var commentEntity = Comments.SingleOrDefault(c => c.CommentID == commentID);
            if (commentEntity != null)
            {
                Comments.Remove(commentEntity);
                SaveChanges();
            }
        }

        public void AddComment(CommentEntity commentEntity)
        {
            commentEntity.CommentPostedDate = DateTime.Now;
            Comments.Add(commentEntity);
            SaveChanges();
        }

        public void DeleteCommentsByPostID(int postID)
        {
            var comments = Comments.Where(c => c.PostID == postID);
            if (comments.Any())
            {
                foreach (var commentEntity in comments)
                {
                    Comments.Remove(commentEntity);
                }
                SaveChanges();
            }
        }

        public void UpdateCommentStatus(int commentID, int status)
        {
            var commentEntity = Comments.SingleOrDefault(c => c.CommentID == commentID);
            if (commentEntity != null)
            {
                commentEntity.CommentStatus = status;
                SaveChanges();
            }
        }

        public void DeleteCommentsByPostID(IEnumerable<int> postList)
        {
            var comments = Comments.Where(c => postList.Contains(c.PostID));
            if (comments.Any())
            {
                foreach (var commentEntity in comments)
                {
                    Comments.Remove(commentEntity);
                }
                SaveChanges();
            }
        }
    }
}
