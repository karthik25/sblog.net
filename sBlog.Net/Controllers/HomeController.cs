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
using System.Web.Mvc;
using sBlog.Net.CustomExceptions;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Models;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.Controllers
{
    public class HomeController : BlogController
    {
        private readonly int _postsPerPage;

        private readonly IPost _postRepository;
        private readonly IUser _userRepository;
        private readonly ICategory _categoryRepository;
        private readonly ITag _tagRepository;
        private readonly ICacheService _cacheService;

        public HomeController(IPost postRepository, IUser userRepository, ICategory categoryRepository, ITag tagRepository, ISettings settingsRepository, ICacheService cacheService)
            : base (settingsRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _postsPerPage = settingsRepository.BlogPostsPerPage;
            _cacheService = cacheService;
        }

        //
        // GET: /page/[pageNumber]

        public ActionResult Index(int? pageNumber)
        {
            var posts = GetPostsInternal();
            var blogPostModel = GetBlogPostPageViewModel(posts, pageNumber);
            return View(blogPostModel);
        }

        //
        // GET: /2011/12

        public ActionResult PostsByYearAndMonth(string year, string month, int? pageNumber)
        {
            var posts = GetPostsInternal().Where(p => p.PostAddedDate.Month == Int32.Parse(month) && p.PostAddedDate.Year == Int32.Parse(year))
                                          .ToList();
            var blogPostModel = GetBlogPostPageViewModel(posts, pageNumber);
            blogPostModel.Year = year;
            blogPostModel.Month = month;
            return View(blogPostModel);
        }

        //
        // GET: /tags/tagName

        public ActionResult PostsByTag(string tagName, int? pageNumber)
        {
            var posts = GetPostsInternal().Where(p => p.Tags.Any(t => t.TagSlug == tagName.ToLower()))
                                          .ToList();
            var blogPostModel = GetBlogPostPageViewModel(posts, pageNumber);
            blogPostModel.Tag = GetTagEntity(tagName);
            return View(blogPostModel);
        }

        //
        // GET: /category/categoryName

        public ActionResult PostsByCategory(string categoryName, int? pageNumber)
        {
            var posts = GetPostsInternal().Where(p => p.Categories.Any(c => c.CategorySlug == categoryName.ToLower()))
                                          .ToList();
            var blogPostModel = GetBlogPostPageViewModel(posts, pageNumber);
            blogPostModel.Category = GetCategoryEntity(categoryName);
            return View(blogPostModel);
        }

        //
        // GET: /2011/12/some-url

        public ActionResult View(string year, string month, string url, string status)
        {
            var allPosts = GetPostsInternal();
            var currentPost = allPosts.SingleOrDefault(p => p.PostUrl == url && p.EntryType == 1);

            if (currentPost == null)
            {
                throw new UrlNotFoundException("Unable to find a post w/ the url {0} for the month {1} and year {2}", url, month, year);
            }

            var index = allPosts.IndexOf(currentPost);

            if (!Request.IsAuthenticated && status == "comment-posted")
            {
                var recentPost = _postRepository.GetPostByUrl(url, 1);
                currentPost.Comments = recentPost.Comments;
            }

            var model = new ViewPostOrPageModel
            {
                Post = currentPost,
                BlogSharingEnabled = SettingsRepository.BlogSocialSharing,
                SharingType = SettingsRepository.BlogSocialSharingChoice,
                PreviousPost = index == 0 || index < 0 ? null : allPosts[index - 1],
                NextPost = index == (allPosts.Count - 1) || index < 0 ? null : allPosts[index + 1],
                UserCanEdit = Request.IsAuthenticated && (currentPost.OwnerUserID == GetUserId() || GetUserId() == 1),
                BlogName = SettingsRepository.BlogName,
                BlogCaption = SettingsRepository.BlogCaption,
                CommentEntity = GetCommentEntityByAuth()
            };
            return View(model);
        }

        public ActionResult Error()
        {
            var errType = Request.QueryString["err"];

            var blogErrorViewModel = new BlogErrorViewModel { Title = SettingsRepository.BlogName, 
                                                              ErrorCode = errType, 
                                                              ErrorDescription = "An unknown error has occurred" };
            if (!string.IsNullOrEmpty(errType))
            {
                switch (errType)
                {
                    case "unauthorized":
                        blogErrorViewModel.ErrorDescription = "An unauthorized access was detected";
                        break;
                    case "invalid-month":
                        blogErrorViewModel.ErrorDescription = "Month/year in the url is invalid";
                        break;
                    default:
                        blogErrorViewModel.ErrorDescription = "An unknown error has occurred";
                        break;
                }
            }
            return View("Error", blogErrorViewModel);
        }

        private List<PostEntity> GetPostsInternal()
        {
            var posts = Request.IsAuthenticated
                            ? GetProcessedPosts(_postRepository.GetPosts(GetUserId()))
                            : _cacheService.GetPostsFromCache(_postRepository, CachePostsUnauthKey);
            return posts;
        }

        private static List<PostEntity> GetProcessedPosts(List<PostEntity> postList)
        {
            postList.ForEach(p =>
                {
                    if (p.IsPrivate)
                        p.PostTitle = string.Format("[Private] {0}", p.PostTitle);
                });
            return postList;
        }

        private BlogPostPageViewModel GetBlogPostPageViewModel(ICollection<PostEntity> posts, int? pageNumber)
        {
            var pgNumber = pageNumber ?? 1;
            var totalItems = GetPageCount(posts.Count);
            var blogPostModel = GetBlogPostPageModel(pgNumber, totalItems);
            blogPostModel.Posts = posts.Skip((pgNumber - 1) * _postsPerPage)
                                       .Take(_postsPerPage)
                                       .ToList();
            blogPostModel.BlogName = SettingsRepository.BlogName;
            blogPostModel.BlogCaption = SettingsRepository.BlogCaption;
            blogPostModel.CurrentPageNumber = pageNumber.HasValue ? pageNumber.Value : 1;
            return blogPostModel;
        }

        private int GetPageCount(int totalItems)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / _postsPerPage);
            return totalPages;
        }

        private TagEntity GetTagEntity(string tagName)
        {
            var tagEntity = _tagRepository.GetAllTags().SingleOrDefault(t => t.TagSlug == tagName.ToLower()) ??
                            new TagEntity {TagName = tagName};
            return tagEntity;
        }

        private CategoryEntity GetCategoryEntity(string categoryName)
        {
            var categoryEntity = _categoryRepository.GetCategories().SingleOrDefault(c => c.CategorySlug == categoryName.ToLower()) ??
                                 new CategoryEntity {CategoryName = categoryName};
            return categoryEntity;
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

        private CommentEntity GetCommentEntityByAuth()
        {
            var userId = GetUserId();
            if (userId == -1)
                return new CommentEntity();
            var user = _userRepository.GetUserObjByUserID(userId);
            return new CommentEntity
                {
                    CommentUserFullName = user.UserDisplayName,
                    CommenterEmail = user.UserEmailAddress,
                    CommenterSite = user.UserSite
                };
        }
    }
}
