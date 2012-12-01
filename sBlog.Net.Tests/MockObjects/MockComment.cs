using System;
using System.Collections.Generic;
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockComment : IComment
    {
        private readonly List<CommentEntity> _commentsTable;

        public MockComment()
        {
            _commentsTable = GetMockComments();
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
            throw new NotImplementedException();
        }

        public void DeleteCommentsByPostID(int postID)
        {
            throw new NotImplementedException();
        }

        public void AddComment(CommentEntity commentEntity)
        {
            _commentsTable.Add(commentEntity);
        }

        public void UpdateCommentStatus(int commentID, int status)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommentsByPostID(IEnumerable<int> postList)
        {
            throw new NotImplementedException();
        }

        private List<CommentEntity> GetMockComments()
        {
            var comments = new List<CommentEntity>();
            var i = 0;
            for (i = 1; i < 6; i++)
            {
                comments.Add(new CommentEntity { CommentID = i, CommentContent = "Comment number " + i, CommentPostedDate = DateTime.Parse("12/22/2011"), CommentStatus = 0, CommentUserFullName = "Test Commenter", PostID = 1 });
            }
            for (i = 6; i < 11; i++)
            {
                comments.Add(new CommentEntity { CommentID = i, CommentContent = "Comment number " + i, CommentPostedDate = DateTime.Parse("12/22/2011"), CommentStatus = 0, CommentUserFullName = "Test Commenter", PostID = 2 });
            }
            for (i = 11; i < 16; i++)
            {
                comments.Add(new CommentEntity { CommentID = i, CommentContent = "Comment number " + i, CommentPostedDate = DateTime.Parse("12/22/2011"), CommentStatus = 0, CommentUserFullName = "Test Commenter", PostID = 3 });
            }

            return comments;
        }

        public void Dispose()
        {
            
        }
    }
}
