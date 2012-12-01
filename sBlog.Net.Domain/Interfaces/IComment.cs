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
using System.Collections.Generic;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Interfaces
{
    public interface IComment : IDisposable
    {
        List<CommentEntity> GetAllComments();
        List<CommentEntity> GetAllComments(int status);
        
        List<CommentEntity> GetCommentsByPostID(int postID);
        void DeleteCommentByCommentID(int commentID);
        void DeleteCommentsByPostID(int postID);
        void AddComment(CommentEntity commentEntity);
        void UpdateCommentStatus(int commentID, int status);

        void DeleteCommentsByPostID(IEnumerable<int> postList);
    }
}
