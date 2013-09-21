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
using sBlog.Net.Infrastructure;
using sBlog.Net.Models;
using sBlog.Net.FluentExtensions;
using sBlog.Net.Controllers;
using System.ComponentModel;
using sBlog.Net.Enumerations;
using sBlog.Net.Areas.Admin.Models;

namespace sBlog.Net.Areas.Admin.Controllers
{
    [Authorize]
    public class PostController : BlogController
    {
        private readonly IPost _postRepository;
        private readonly ICategory _categoryRepository;
        private readonly ITag _tagRepository;

        private readonly int _itemsPerPage;

        private const byte ItemEntryType = 1;

        public PostController(IPost postRepository, ICategory categoryRepository, ITag tagRepository, ISettings settingsRepository)
            : base (settingsRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            ExpectedMasterName = string.Empty;

            _itemsPerPage = settingsRepository.ManageItemsPerPage;

            IsAdminController = true;
        }

        public ActionResult Add()
        {
            var postModel = new PostViewModel
                                {
                                    Post = new PostEntity {EntryType = ItemEntryType, UserCanAddComments = true, CanBeShared = true},
                                    Categories = GetModel(_categoryRepository.GetCategories()),
                                    Tags = string.Empty,
                                    Title = SettingsRepository.BlogName,
                                    SharingEnabled =  SettingsRepository.BlogSocialSharing
                                };
            return View(postModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(PostViewModel postModel)
        {
            if (ModelState.IsValid)
            {
                var tags = _tagRepository.GetTagEntities(postModel.Tags.Split(',').ToList());
                _tagRepository.AddTags(tags);
                var postEntity = postModel.ToPostEntity(_tagRepository);
                postEntity.PostAddedDate = DateTime.Now;
                postEntity.PostEditedDate = postEntity.PostAddedDate;
                postEntity.OwnerUserID = GetUserId();
                
                if (string.IsNullOrEmpty(postEntity.PostUrl))
                {
                    postEntity.PostUrl = UniqueUrlHelper.FindUniqueUrl(_postRepository, postEntity.PostTitle, ItemEntryType);
                }

                var biltyPostUrl = BitlyUrlService.GetBiltyPostUrl(SettingsRepository, postEntity.PostUrl);
                if (biltyPostUrl != null)
                {
                    postEntity.BitlyUrl = biltyPostUrl;
                    postEntity.BitlySourceUrl = postEntity.PostUrl;
                }

                var postId = _postRepository.AddPost(postEntity);

                if (postId > 0)
                {
                    return RedirectToAction("Edit", new {postID = postId, newlyAdded = true });
                }
            }
            postModel.Title = SettingsRepository.BlogName;
            postModel.SharingEnabled = SettingsRepository.BlogSocialSharing;

            return View(postModel);
        }

        public ActionResult Edit(int postID, [DefaultValue(false)] bool newlyAdded)
        {
            var post = _postRepository.GetPostByID(postID);

            ValidateEditRequest(post);

            var postModel = new PostViewModel
                {
                    Post = post,
                    Categories = GetModel(_categoryRepository.GetCategories(), post.Categories),
                    Tags = string.Join(",",_tagRepository.GetTagsByPostID(postID).Select(t => t.TagName).ToArray()),
                    Title = SettingsRepository.BlogName,
                    SharingEnabled = SettingsRepository.BlogSocialSharing,
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
                var tags = _tagRepository.GetTagEntities(postModel.Tags.Split(',').ToList());
                _tagRepository.AddTags(tags);
                var postEntity = postModel.ToPostEntity(_tagRepository);
                postEntity.PostEditedDate = DateTime.Now;

                if (string.IsNullOrEmpty(postEntity.PostUrl))
                {
                    postEntity.PostUrl = UniqueUrlHelper.FindUniqueUrl(_postRepository, postEntity.PostTitle, ItemEntryType, postEntity.PostID);
                }

                if (postEntity.PostUrl != postEntity.BitlySourceUrl)
                {
                    var biltyPostUrl = BitlyUrlService.GetBiltyPostUrl(SettingsRepository, postEntity.PostUrl);
                    if (biltyPostUrl != null)
                    {
                        postEntity.BitlyUrl = biltyPostUrl;
                        postEntity.BitlySourceUrl = postEntity.PostUrl;
                    }
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
            VerifyOwnership(postID);

            if (postID > 0)
            {
                _postRepository.DeletePost(postID);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePost(int postID)
        {
            VerifyOwnership(postID);

            if (postID > 0)
            {
                _postRepository.DeletePost(postID);
            }
            return RedirectToRoute("AdminPostsAdd");
        }

        public ActionResult ManagePosts([DefaultValue(1)] int page, string type)
        {
            var status = type != null ? (int)Enum.Parse(typeof(PostStatus), type, true) : 0;
            var allPosts = _postRepository.GetPostsByUserID(GetUserId(), ItemEntryType);
            var allPostsByStatus = allPosts.GetPostsByStatus(status);
            var postModel = new AdminPostOrPageViewModel
            {
                Posts = allPostsByStatus.Skip((page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList(),
                PagingInfo = new PagingInformation
                {
                    CurrentPage = page,
                    ItemsPerPage = _itemsPerPage,
                    TotalItems = allPostsByStatus.Count
                },
                Type = type,
                AllPostsCount = allPosts.Count,
                PrivatePostsCount = allPosts.Count(p => p.IsPrivate),
                OneTimeCode = GetToken(),
                Title = SettingsRepository.BlogName
            };
            return View(postModel);
        }

        [HttpGet]
        public JsonResult VerifyUrlUrlExists(string postTitle, string entryType, int postId)
        {
            var post = postId > 0 ? _postRepository.GetPostByID(postId) : null;

            var finalUrl = post != null ? UniqueUrlHelper.FindUniqueUrl(_postRepository, postTitle, byte.Parse(entryType), postId) 
                                           : UniqueUrlHelper.FindUniqueUrl(_postRepository, postTitle, byte.Parse(entryType));

            return Json(finalUrl, JsonRequestBehavior.AllowGet);
        }

        private void ValidateEditRequest(PostEntity postEntity)
        {
            if (postEntity.IsPrivate)
            {
                if (postEntity.OwnerUserID != GetUserId())
                {
                    throw new UnauthorizedAccessException("Unauthorized attempt to edit the post");
                }
            }
            else
            {
                if (postEntity.OwnerUserID != GetUserId() && !User.IsInRole("SuperAdmin"))
                {
                    throw new UnauthorizedAccessException("Unauthorized attempt to edit the post");
                }
            }
        }

        private void VerifyOwnership(int postID)
        {
            var post = _postRepository.GetPostByID(postID);
            var userId = GetUserId();

            if (post.OwnerUserID != userId && userId != 1)
            {
                throw new UnauthorizedAccessException("An unauthroized access was detected");
            }
        }

        private static CheckBoxListViewModel GetModel(List<CategoryEntity> baseList, IEnumerable<CategoryEntity> selectedCategories = null)
        {
            var model = new CheckBoxListViewModel {HeaderText = "select categories"};
            var checkItems = new List<CheckBoxListItem>();
            baseList.ForEach(category =>
            {
                var item = new CheckBoxListItem
                {
                    Text = category.CategoryName,
                    Value = category.CategoryID.ToString(),
                    IsChecked = selectedCategories != null && selectedCategories.Any(c => c.CategoryID == category.CategoryID)
                };
                checkItems.Add(item);
            });
            
            if (selectedCategories == null)
            {
                var general = checkItems.SingleOrDefault(c => c.Text == "General");
                if (general != null)
                {
                    general.IsChecked = true;
                }
            }
            
            model.Items = checkItems;
            return model;
        }
    }
}
