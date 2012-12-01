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
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;
using System.Data.Linq;

namespace sBlog.Net.Domain.Concrete
{
    public class Comment : DefaultDisposable, IComment
    {
        private readonly Table<CommentEntity> _commentsTable;

        public Comment()
        {
            _commentsTable = context.GetTable<CommentEntity>();
        }
        
        public List<CommentEntity> GetAllComments()
        {
            return _commentsTable.OrderByDescending(c => c.CommentPostedDate).ToList();
        }

        public List<CommentEntity> GetAllComments(int status)
        {
            return _commentsTable.Where(c => c.CommentStatus == status).OrderByDescending(c => c.CommentPostedDate).ToList();
        }

        public List<CommentEntity> GetCommentsByPostID(int postID)
        {
            return _commentsTable.Where(c => c.PostID == postID && (c.CommentStatus == 0 || c.CommentStatus == 1)).OrderByDescending(c => c.CommentPostedDate).ToList();
        }

        public void DeleteCommentByCommentID(int commentID)
        {
            var commentEntity = _commentsTable.SingleOrDefault(c => c.CommentID == commentID);
            if (commentEntity != null)
            {
                _commentsTable.DeleteOnSubmit(commentEntity);
                context.SubmitChanges();
            }
        }

        public void AddComment(CommentEntity commentEntity)
        {
            commentEntity.CommentPostedDate = DateTime.Now;
            _commentsTable.InsertOnSubmit(commentEntity);
            context.SubmitChanges();
        }

        public void DeleteCommentsByPostID(int postID)
        {
            var comments = _commentsTable.Where(c => c.PostID == postID);
            if (comments.Any())
            {
                _commentsTable.DeleteAllOnSubmit(comments);
                context.SubmitChanges();
            }
        }

        public void UpdateCommentStatus(int commentID, int status)
        {
            var commentEntity = _commentsTable.SingleOrDefault(c => c.CommentID == commentID);
            if (commentEntity != null)
            {
                commentEntity.CommentStatus = status;
                context.SubmitChanges();
            }
        }

        public void DeleteCommentsByPostID(IEnumerable<int> postList)
        {
            var comments = _commentsTable.Where(c => postList.Contains(c.PostID));
            if (comments.Any())
            {
                _commentsTable.DeleteAllOnSubmit(comments);
                context.SubmitChanges();
            }
        }

        ~Comment()
        {
            Dispose(false);
        }
    }
}
