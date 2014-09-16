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
            _cacheService = cacheService;
        }

        //
        // GET: /page/[pageNumber]

        public ActionResult Index(int? pageNumber)
        {
            var posts = GetPostsInternal();
            var blogPostModel = posts.GetBlogPostPageViewModel(pageNumber, SettingsRepository, GetRootUrl());
            return View(blogPostModel);
        }

        //
        // GET: /2011/12

        public ActionResult PostsByYearAndMonth(string year, string month, int? pageNumber)
        {
            var posts = GetPostsInternal().Where(p => p.PostAddedDate.Month == Int32.Parse(month) && p.PostAddedDate.Year == Int32.Parse(year))
                                          .ToList();
            var blogPostModel = posts.GetBlogPostPageViewModel(pageNumber, SettingsRepository, GetRootUrl());
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
            var blogPostModel = posts.GetBlogPostPageViewModel(pageNumber, SettingsRepository, GetRootUrl());
            blogPostModel.Tag = GetTagEntity(tagName);
            return View(blogPostModel);
        }

        //
        // GET: /category/categoryName

        public ActionResult PostsByCategory(string categoryName, int? pageNumber)
        {
            var posts = GetPostsInternal().Where(p => p.Categories.Any(c => c.CategorySlug == categoryName.ToLower()))
                                          .ToList();
            var blogPostModel = posts.GetBlogPostPageViewModel(pageNumber, SettingsRepository, GetRootUrl());
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
                UserCanEdit = Request.IsAuthenticated && (currentPost.OwnerUserID == GetUserId() || User.IsInRole("SuperAdmin")),
                BlogName = SettingsRepository.BlogName,
                BlogCaption = SettingsRepository.BlogCaption,
                CommentEntity = GetCommentEntityByAuth(),
                DisqusEnabled = SettingsRepository.DisqusEnabled,
                DisqusUrl = GetRootUrl().TrimEnd('/') + Url.RouteUrl("IndividualPost", new { year = currentPost.PostAddedDate.Year.ToString(), month = currentPost.PostMonth, url = currentPost.PostUrl }),
                ShortName = SettingsRepository.BlogDisqusShortName,
                DisqusDevMode = System.Web.HttpContext.Current.IsDebuggingEnabled
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
            var markdown = new MarkdownDeep.Markdown{ ExtraMode = true };
            postList.ForEach(p =>
                {
                    if (ContentInterpretationExtensions.IsMarkDown())
                        p.PostContent = markdown.Transform(p.PostContent);
                    if (p.IsPrivate)
                        p.PostTitle = string.Format("[Private] {0}", p.PostTitle);
                });
            return postList;
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
