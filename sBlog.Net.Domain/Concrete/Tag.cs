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
using System;
using System.Data.Entity;
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;
using System.Collections.Generic;

namespace sBlog.Net.Domain.Concrete
{
    public class Tag : System.Data.Entity.DbContext, ITag
    {
        public IDbSet<TagEntity> Tags { get; set; }
        public IDbSet<TagMapping> TagMappings { get; set; }

        public Tag()
            :base("AppDb")
        {
            
        }

        public List<TagEntity> GetAllTags()
        {
            return Tags.ToList();
        }

        public List<TagEntity> GetTagsByPostID(int postID)
        {            
            var tagEntities = new List<TagEntity>();
            var allTags = GetAllTags();
            var tagsForPost = TagMappings.Where(t => t.PostID == postID).ToList();

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
                foreach (var tagEntity in tags)
                {
                    Tags.Add(tagEntity);
                }
                SaveChanges();
            }
        }

        public void DeleteTag(int tagID)
        {
            IEnumerable<TagMapping> currentMappings = TagMappings.Where(t => t.TagID == tagID);
            if (currentMappings.Any())
            {
                foreach (var currentMapping in currentMappings)
                {
                    TagMappings.Remove(currentMapping);
                }
                SaveChanges();
            }

            var tagEntity = Tags.SingleOrDefault(t => t.TagID == tagID);
            if (tagEntity != null)
            {
                Tags.Remove(tagEntity);
                SaveChanges();
            }
        }

        public void AddTagsForPost(List<TagEntity> tags, int postID)
        {
            var tagMappings = new List<TagMapping>();
            tags.ForEach(t => tagMappings.Add(new TagMapping { TagID = t.TagID, PostID = postID }));
            foreach (var tagMapping in tagMappings)
            {
                TagMappings.Add(tagMapping);
            }
            SaveChanges();
        }

        public void UpdateTagsForPost(List<TagEntity> tags, int postID)
        {
            var tagMappings = new List<TagMapping>();
            var postTags = TagMappings.Where(p => p.PostID == postID).ToList();
            tags.ForEach(t => tagMappings.Add(new TagMapping { TagID = t.TagID, PostID = postID }));
            foreach (var tagMapping in postTags)
            {
                TagMappings.Remove(tagMapping);
            }            
            foreach (var tagMapping in tagMappings)
            {
                TagMappings.Add(tagMapping);
            }
            SaveChanges();
        }

        public void DeleteTagsForPost(int postID)
        {
            var tagMappings = TagMappings.Where(t => t.PostID == postID).ToList();
            if (tagMappings.Count > 0)
            {
                foreach (var tagMapping in tagMappings)
                {
                    TagMappings.Remove(tagMapping);
                }
                SaveChanges();
            }
        }

        public void DeleteTagsForsPosts(IEnumerable<int> postList)
        {
            var tagMappings = TagMappings.Where(t => postList.Contains(t.PostID));
            if (tagMappings.Any())
            {
                foreach (var tagMapping in tagMappings)
                {
                    TagMappings.Remove(tagMapping);
                }
                SaveChanges();
            }
        }
    }
}
