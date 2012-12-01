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
    public interface ITag : IDisposable
    {
        List<TagEntity> GetAllTags();
        List<TagEntity> GetTagsByPostID(int postID);
        void DeleteTag(int tagID);
        void AddTags(List<TagEntity> tags);
        void AddTagsForPost(List<TagEntity> tags, int postID);
        void UpdateTagsForPost(List<TagEntity> tags, int postID);
        void DeleteTagsForPost(int postID);
        void DeleteTagsForsPosts(IEnumerable<int> postList);
    }
}
