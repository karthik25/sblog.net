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
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.FluentExtensions
{
    public static class CacheServiceFluentExtensions
    {
        public static List<PostEntity> GetPostsFromCache(this ICacheService cacheService, IPost postRepository, string keyName, bool isMarkdown)
        {
            var markdown = new MarkdownDeep.Markdown { ExtraMode = true };
            var posts = cacheService.Get(keyName, () => postRepository.GetPosts());
            if (isMarkdown)
            {
                posts.ForEach(p => p.PostContent = markdown.Transform(p.PostContent));
            }
            return posts;
        }

        public static List<PostEntity> GetPagesFromCache(this ICacheService cacheService, IPost postRepository, string keyName, bool isMarkdown)
        {
            var markdown = new MarkdownDeep.Markdown{ ExtraMode = true };
            var pages = cacheService.Get(keyName, () => postRepository.GetPages());
            if (isMarkdown)
            {
                pages.ForEach(p => p.PostContent = markdown.Transform(p.PostContent));
            }
            return pages;
        }
    }
}