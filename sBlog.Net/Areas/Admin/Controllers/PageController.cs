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
using System.Linq;
using System.Web.Mvc;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Infrastructure;
using sBlog.Net.Models;
using sBlog.Net.Domain.Entities;
using sBlog.Net.FluentExtensions;
using sBlog.Net.Controllers;
using System.ComponentModel;
using sBlog.Net.Enumerations;
using sBlog.Net.Areas.Admin.Models;

namespace sBlog.Net.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class PageController : BlogController
    {
        private readonly IPost _postRepository;
        private readonly ITag _tagRepository;

        private readonly int _itemsPerPage;

        private const byte ItemEntryType = 2;

        public PageController(IPost postRepository, ITag tagRepository, ISettings settingsRepository)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            ExpectedMasterName = string.Empty;

            _itemsPerPage = settingsRepository.ManageItemsPerPage;

            IsAdminController = true;
        }

        public ActionResult Add()
        {
            ValidateActionRequest("Unauthorized attempt to add a post");

            var postModel = new PostViewModel
            {
                Post = new PostEntity { EntryType = ItemEntryType, UserCanAddComments = true, CanBeShared = true },
                Title = SettingsRepository.BlogName,
                SharingEnabled = SettingsRepository.BlogSocialSharing
            };

            return View(postModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(PostViewModel postModel)
        {
            if (ModelState.IsValid)
            {
                var postEntity = postModel.ToPostEntity(_tagRepository);
                postEntity.PostAddedDate = DateTime.Now;
                postEntity.PostEditedDate = postEntity.PostAddedDate;
                postEntity.OwnerUserID = GetUserId();

                if (string.IsNullOrEmpty(postEntity.PostUrl))
                {
                    postEntity.PostUrl = UniqueUrlHelper.FindUniqueUrl(_postRepository, postEntity.PostTitle, ItemEntryType);
                }

                var pageID = _postRepository.AddPost(postEntity);

                if (pageID > 0)
                {
                    return RedirectToAction("Edit", new { postID = pageID, newlyAdded = true });
                }
            }
            postModel.Title = SettingsRepository.BlogName;
            postModel.SharingEnabled = SettingsRepository.BlogSocialSharing;

            return View(postModel);
        }

        public ActionResult Edit(int postID, [DefaultValue(false)] bool newlyAdded)
        {
            ValidateActionRequest("Unauthorized attempt to edit the page");
            var post = _postRepository.GetPostByID(postID);

            var postModel = new PostViewModel
            {
                Post = post,
                Title = SettingsRepository.BlogName,
                SharingEnabled = SettingsRepository.BlogSocialSharing
            };

            if (newlyAdded)
            {
                postModel.UpdateStatus = true;
                postModel.IsNewPostOrPage = true;
            }

            return View(postModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PostViewModel postModel)
        {
            if (ModelState.IsValid)
            {
                var postEntity = postModel.ToPostEntity(_tagRepository);
                postEntity.PostEditedDate = DateTime.Now;

                if (string.IsNullOrEmpty(postEntity.PostUrl))
                {
                    postEntity.PostUrl = UniqueUrlHelper.FindUniqueUrl(_postRepository, postEntity.PostTitle, ItemEntryType, postEntity.PostID);
                }

                _postRepository.UpdatePost(postEntity);

                postModel.UpdateStatus = true;
                postModel.IsNewPostOrPage = false;
            }
            postModel.Title = SettingsRepository.BlogName;
            postModel.SharingEnabled = SettingsRepository.BlogSocialSharing;

            return View(postModel);
        }

        public JsonResult Delete(int postID)
        {
            ValidateActionRequest("An unauthorized access was detected");

            if (postID > 0)
            {
                _postRepository.DeletePost(postID);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePage(int postID)
        {
            ValidateActionRequest("An unauthorized access was detected");

            if (postID > 0)
            {
                _postRepository.DeletePost(postID);
            }
            return RedirectToRoute("AdminPagesAdd");
        }

        public ActionResult ManagePages([DefaultValue(1)] int page, string type)
        {
            var status = type != null ? (int)Enum.Parse(typeof(PostStatus), type, true) : 0;
            var allPages = _postRepository.GetPostsByUserID(GetUserId(), ItemEntryType);
            var allPagesByStatus = allPages.GetPostsByStatus(status);
            var postModel = new AdminPostOrPageViewModel
            {
                Posts = allPagesByStatus.Skip((page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList(),
                PagingInfo = new PagingInformation
                {
                    CurrentPage = page,
                    ItemsPerPage = _itemsPerPage,
                    TotalItems = allPagesByStatus.Count
                },
                Type = type,
                AllPostsCount = allPages.Count,
                PrivatePostsCount = allPages.Count(p => p.IsPrivate),
                OneTimeCode = GetToken(),
                Title = SettingsRepository.BlogName
            };
            return View(postModel);
        }

        private void ValidateActionRequest(string message)
        {
            if (GetUserId() != 1)
            {
                throw new UnauthorizedAccessException(message);
            }
        }
    }
}
