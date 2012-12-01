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
using System.Linq;
using System.Web.Mvc;
using sBlog.Net.CustomExceptions;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.FluentExtensions;
using sBlog.Net.Models;
using sBlog.Net.Domain.Entities;
using System.Text.RegularExpressions;

namespace sBlog.Net.Controllers
{
    public class ViewPageController : BlogController
    {
        private readonly IPost _postRepository;
        private readonly IUser _userRepository;
        private readonly ICacheService _cacheService;

        public ViewPageController(IPost postRepository, IUser userRepository, ISettings settingsRepository, ICacheService cacheService)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _cacheService = cacheService;
            ExpectedMasterName = "_LayoutPage";
        }

        public ActionResult Index(string pageUrl, string status)
        {
            var pages = GetPages();
            var page = pages.SingleOrDefault(p => p.PostUrl == pageUrl && p.EntryType == 2);

            if (page == null)
            {
                throw new UrlNotFoundException("Unable to find a page w/ the url {0}", pageUrl);
            }

            if (page.IsPrivate)
            {
                page.PostTitle = string.Format("[Private] {0}", page.PostTitle);
            }

            if (!Request.IsAuthenticated && status == "comment-posted")
            {
                var recentPage = _postRepository.GetPostByUrl(pageUrl, 2);
                page.Comments = recentPage.Comments;
            }

            var model = new ViewPostOrPageModel
            {
                Post = page,
                BlogSharingEnabled = SettingsRepository.BlogSocialSharing,
                SharingType = SettingsRepository.BlogSocialSharingChoice,
                UserCanEdit = Request.IsAuthenticated && GetUserId() == 1,
                BlogName = SettingsRepository.BlogName,
                BlogCaption = SettingsRepository.BlogCaption,
                CommentEntity = GetCommentEntityByAuth()
            };
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Pages()
        {
            var pages = GetPages();
            pages = pages.Take(4).ToList();
            var requestedPage = GetRequestedPage(Request.Url.ToString());
            return PartialView(GetBlogMenus(pages, requestedPage));
        }

        public ActionResult PagesList()
        {
            var pages = GetPages();
            pages = pages.Skip(4).ToList();
            return PartialView("PagesList", pages);
        }

        public ActionResult Credits()
        {
            return View();
        }

        private List<PostEntity> GetPages()
        {
            var pages = Request.IsAuthenticated
                            ? _postRepository.GetPages(GetUserId())
                            : _cacheService.GetPagesFromCache(_postRepository, CachePagesUnauthKey);
            return pages.OrderBy(p => p.Order.Value).ToList();
        }

        private BlogMenuViewModel GetBlogMenus(List<PostEntity> pages, string requestPage)
        {
            var viewModel = new BlogMenuViewModel();
            var pagesList = new List<BlogMenuOption> {new BlogMenuOption {Title = "Home", Url = "/", Selected = false}};
            pages.ForEach(p => pagesList.Add(new BlogMenuOption { Title = p.PostTitle, Url = p.PostUrl, Selected = p.PostUrl == requestPage }));
            if (!pagesList.Any(p => p.Selected) && requestPage == string.Empty)
            {
                var home = pagesList.Single(p => p.Title == "Home");
                home.Selected = true;
            }
            viewModel.Pages = pagesList;
            return viewModel;
        }

        private string GetRequestedPage(string url)
        {
            var pageName = Regex.Match(url, @"pages\/([^)]*)").Groups[1].Value;
            return pageName;
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
