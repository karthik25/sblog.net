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
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.FluentExtensions;
using sBlog.Net.Models;

namespace sBlog.Net.Controllers
{
    public class AuthorController : BlogController
    {
        private readonly int _postsPerPage;

        private readonly IPost _postRepository;
        private readonly IUser _userRepository;
        private readonly ICacheService _cacheService;

        public AuthorController(IPost postRepository, IUser userRepository, ICacheService cacheService, ISettings settingsRepository)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _cacheService = cacheService;

            _postsPerPage = SettingsRepository.BlogPostsPerPage;
        }

        public ActionResult AuthorListing(int? pageNumber)
        {
            var pgNumber = pageNumber ?? 1;
            var posts = GetPostsInternal();
            var users = _userRepository.GetAllUsers().Where(u => u.UserActiveStatus == 1).ToList();
            var totalPages = (int)Math.Ceiling((decimal)users.Count / _postsPerPage);
            var authorListingViewModel = GetAuthorViewModel(pgNumber, totalPages);
            authorListingViewModel.BlogName = SettingsRepository.BlogName;
            authorListingViewModel.BlogCaption = SettingsRepository.BlogCaption;
            authorListingViewModel.CurrentPageNumber = pageNumber.HasValue ? pageNumber.Value : 1;

            var authorItems = users.Skip((pgNumber - 1) * _postsPerPage)
                                       .Take(_postsPerPage).Select(u => new AuthorModel { UserID = u.UserID, UserName = u.UserName, UserDisplayName = u.UserDisplayName }).ToList();
            authorItems.ForEach(author =>
                {
                    author.Posts = posts.Where(p => p.EntryType == 1 && p.OwnerUserID == author.UserID && !p.IsPrivate).ToList();
                });
            authorListingViewModel.Authors = authorItems;
            return View(authorListingViewModel);
        }

        public ActionResult PostsByAuthor(string authorName, int? pageNumber)
        {
            var user = _userRepository.GetAllUsers().SingleOrDefault(u => u.UserName == authorName);
            var posts = GetPostsInternal();
            List<PostEntity> authorPosts = null;
            if (user != null)
            {
                authorPosts = posts.Where(p => p.OwnerUserID == user.UserID).ToList();
            }
            var blogPostModel = authorPosts.GetBlogPostPageViewModel(pageNumber, SettingsRepository, GetRootUrl());
            blogPostModel.AuthorName = authorName;
            blogPostModel.AuthorDisplayName = user != null ? user.UserDisplayName : authorName;
            
            return View(blogPostModel);
        }

        private IEnumerable<PostEntity> GetPostsInternal()
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

        private static AuthorListingViewModel GetAuthorViewModel(int currentPage, int totalPages)
        {
            return new AuthorListingViewModel
            {
                NextPageValid = currentPage != 1 && totalPages > 1,
                NextPageNumber = currentPage - 1,
                PreviousPageValid = currentPage < totalPages && currentPage != totalPages,
                PreviousPageNumber = currentPage + 1
            };
        }
    }
}
