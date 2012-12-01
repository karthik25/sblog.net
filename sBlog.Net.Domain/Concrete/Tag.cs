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
/* Tag.cs 
 * 
 * This class extends the DefaultDisposable class,
 * Which implements the IDisposable interface for this class.
 * 
 * If you modify the class to add more disposable managed
 * resources, you can remove DefaultDisposable and implement
 * the Dispose() method yourself
 * 
 * */
using System.Linq;
using System.Data.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;
using System.Collections.Generic;

namespace sBlog.Net.Domain.Concrete
{
    public class Tag : DefaultDisposable, ITag
    {
        private readonly Table<TagEntity> _tagsTable;
        private readonly Table<TagMapping> _postTagMapping;

        public Tag()
        {
            _tagsTable = context.GetTable<TagEntity>();
            _postTagMapping = context.GetTable<TagMapping>();
        }

        public List<TagEntity> GetAllTags()
        {
            return _tagsTable.ToList();
        }

        public List<TagEntity> GetTagsByPostID(int postID)
        {            
            var tagEntities = new List<TagEntity>();
            var allTags = GetAllTags();
            var tagsForPost = _postTagMapping.Where(t => t.PostID == postID).ToList();

            tagsForPost.ForEach(mapping =>
            {
                var tag = allTags.Single(t => t.TagID == mapping.TagID);
                var tagEntity = new TagEntity { TagID = mapping.TagID, TagName = tag.TagName, TagSlug = tag.TagSlug };
                tagEntities.Add(tagEntity);
            });

            return tagEntities;
        }

        public void AddTags(List<TagEntity> tags)
        {            
            if (tags.Any())
            {
                _tagsTable.InsertAllOnSubmit(tags);
                context.SubmitChanges();
            }
        }

        public void DeleteTag(int tagID)
        {
            IEnumerable<TagMapping> currentMappings = _postTagMapping.Where(t => t.TagID == tagID);
            if (currentMappings.Any())
            {
                _postTagMapping.DeleteAllOnSubmit(currentMappings);
                context.SubmitChanges();
            }

            var tagEntity = _tagsTable.SingleOrDefault(t => t.TagID == tagID);
            if (tagEntity != null)
            {
                _tagsTable.DeleteOnSubmit(tagEntity);
                context.SubmitChanges();
            }
        }

        public void AddTagsForPost(List<TagEntity> tags, int postID)
        {
            var tagMappings = new List<TagMapping>();
            tags.ForEach(t => tagMappings.Add(new TagMapping { TagID = t.TagID, PostID = postID }));
            _postTagMapping.InsertAllOnSubmit(tagMappings);
            context.SubmitChanges();
        }

        public void UpdateTagsForPost(List<TagEntity> tags, int postID)
        {
            var tagMappings = new List<TagMapping>();
            var postTags = _postTagMapping.Where(p => p.PostID == postID).ToList();
            tags.ForEach(t => tagMappings.Add(new TagMapping { TagID = t.TagID, PostID = postID }));
            _postTagMapping.DeleteAllOnSubmit(postTags);
            _postTagMapping.InsertAllOnSubmit(tagMappings);
            context.SubmitChanges();
        }

        public void DeleteTagsForPost(int postID)
        {
            var tagMappings = _postTagMapping.Where(t => t.PostID == postID).ToList();
            if (tagMappings.Count > 0)
            {
                _postTagMapping.DeleteAllOnSubmit(tagMappings);
                context.SubmitChanges();
            }
        }

        public void DeleteTagsForsPosts(IEnumerable<int> postList)
        {
            var tagMappings = _postTagMapping.Where(t => postList.Contains(t.PostID));
            if (tagMappings.Any())
            {
                _postTagMapping.DeleteAllOnSubmit(tagMappings);
                context.SubmitChanges();
            }
        }

        ~Tag()
        {
            Dispose(false);
        }
    }
}
