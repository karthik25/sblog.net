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
using System.Linq;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Models;

namespace sBlog.Net.FluentExtensions
{
    public static class PostExtensions
    {
        public static IList<PostEntity> GetPostsByStatus(this List<PostEntity> allPosts, int status)
        {
            var postEntities = allPosts;

            switch (status)
            {
                case 1:
                    postEntities = postEntities.Where(p => p.IsPrivate).ToList();
                    break;
                case 2:
                    postEntities = postEntities.Where(p => !p.IsPrivate).ToList();
                    break;
            }

            return postEntities;
        }

        public static BlogPostPageViewModel GetBlogPostPageViewModel(this ICollection<PostEntity> posts, int? pageNumber, ISettings settingsRepository, string rootUrl)
        {
            var pgNumber = pageNumber ?? 1;
            var totalItems = GetPageCount(posts.Count, settingsRepository.BlogPostsPerPage);
            var blogPostModel = GetBlogPostPageModel(pgNumber, totalItems);
            var postList = posts.Skip((pgNumber - 1) * settingsRepository.BlogPostsPerPage)
                                       .Take(settingsRepository.BlogPostsPerPage)
                                       .ToList();
            blogPostModel.Posts = GetPostList(postList, settingsRepository, rootUrl);
            blogPostModel.BlogName = settingsRepository.BlogName;
            blogPostModel.BlogCaption = settingsRepository.BlogCaption;
            blogPostModel.CurrentPageNumber = pageNumber.HasValue ? pageNumber.Value : 1;

            blogPostModel.DisqusEnabled = settingsRepository.DisqusEnabled;
            blogPostModel.ShortName = settingsRepository.BlogDisqusShortName;
            blogPostModel.DisqusDevMode = System.Web.HttpContext.Current.IsDebuggingEnabled;

            return blogPostModel;
        }

        private static BlogPostPageViewModel GetBlogPostPageModel(int currentPage, int totalPages)
        {
            return new BlogPostPageViewModel
            {
                NextPageValid = currentPage != 1 && totalPages > 1,
                NextPageNumber = currentPage - 1,
                PreviousPageValid = currentPage < totalPages && currentPage != totalPages,
                PreviousPageNumber = currentPage + 1
            };
        }

        private static int GetPageCount(int totalItems, int postsPerPage)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / postsPerPage);
            return totalPages;
        }

        private static List<PostModel> GetPostList(List<PostEntity> postList, ISettings settingsRepository, string rUrl)
        {
            var disqusEnabled = settingsRepository.DisqusEnabled;
            var rootUrl = rUrl.TrimEnd('/');

            var postModeList = new List<PostModel>();

            postList.ForEach(post =>
            {
                var postModel = new PostModel { Post = post, RootUrl = rootUrl, DisqusEnabled = disqusEnabled };
                postModeList.Add(postModel);
            });

            return postModeList;
        }
    }
}