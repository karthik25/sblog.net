using System;
using System.Collections.Generic;
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockTag : ITag
    {
        private readonly List<TagEntity> _tagsTable;
        private readonly List<TagMapping> _postTagMapping;

        public MockTag()
        {
            _tagsTable = GetMockTags();
            _postTagMapping = GetMockMappings();
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

        public void DeleteTag(int tagID)
        {
            throw new NotImplementedException();
        }

        public void AddTags(List<TagEntity> tags)
        {
            throw new NotImplementedException();
        }

        public void AddTagsForPost(List<TagEntity> tags, int postID)
        {
            throw new NotImplementedException();
        }

        public void UpdateTagsForPost(List<TagEntity> tags, int postID)
        {
            throw new NotImplementedException();
        }

        public void DeleteTagsForPost(int postID)
        {
            throw new NotImplementedException();
        }

        public void DeleteTagsForsPosts(IEnumerable<int> postList)
        {
            throw new NotImplementedException();
        }

        private static List<TagEntity> GetMockTags()
        {
            var tags = new List<TagEntity>
                           {
                               new TagEntity {TagID = 1, TagName = "CSharp", TagSlug = "csharp" },
                               new TagEntity {TagID = 2, TagName = "Ruby", TagSlug = "ruby" },
                               new TagEntity {TagID = 3, TagName = "mvc 3", TagSlug = "mvc-3" }
                           };

            return tags;
        }

        private static List<TagMapping> GetMockMappings()
        {
            var mappings = new List<TagMapping>();
            int i;

            // 7 - csharp, 7 mvc 3 (public), 7 - ruby (private)

            for (i = 1; i < 8; i++)
            {
                mappings.Add(new TagMapping { PostTagMappingID = i, TagID = 1, PostID = i });
                mappings.Add(new TagMapping { PostTagMappingID = i, TagID = 3, PostID = i });
            }
            for (i = 8; i < 15; i++)
            {
                mappings.Add(new TagMapping { PostTagMappingID = i, TagID = 2, PostID = i });
            }
            for (i = 15; i < 22; i++)
            {
                mappings.Add(new TagMapping { PostTagMappingID = i, TagID = 1, PostID = i });
                mappings.Add(new TagMapping { PostTagMappingID = i, TagID = 3, PostID = i });
            }
            for (i = 22; i < 29; i++)
            {
                mappings.Add(new TagMapping { PostTagMappingID = i, TagID = 2, PostID = i });
            }

            return mappings;
        }

        public void Dispose()
        {
            
        }
    }
}
