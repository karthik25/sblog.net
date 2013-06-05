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
using System.ComponentModel;
using sBlog.Net.Controllers;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.Models;
using sBlog.Net.Enumerations;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.Areas.Admin.Controllers
{
    public class UserAdminController : BlogController
    {
        private readonly IPost _postRepository;
        private readonly IComment _commentRepository;
        private readonly ICategory _categoryRepository;
        private readonly ITag _tagRepository;
        private readonly IUser _userRepository;
        private readonly IRole _roleRepository;

        private readonly int _itemsPerPage;

        public UserAdminController(IPost postRepository, IComment commentRepository, ICategory categoryRepository, ITag tagRepository, IUser userRepository, ISettings settingsRepository, IRole roleRepository)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            ExpectedMasterName = string.Empty;

            _itemsPerPage = settingsRepository.ManageItemsPerPage;

            IsAdminController = true;
        }

        public ActionResult UserManagement([DefaultValue(1)] int page)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var allUsers = _userRepository.GetAllUsers().Where(u => u.UserID != 1).ToList();
            var filteredList = allUsers.Skip((page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();

            filteredList.ForEach(author =>
            {
                author.PostsCount = _postRepository.GetPostsByUserID(author.UserID, 1).Count(p => !p.IsPrivate);
                var roleForUser = _roleRepository.GetRoleForUser(author.UserID);
                author.RoleId = (short) (roleForUser == -1 ? 2 : roleForUser);
            });

            var usersModel = new AdminAuthorsViewModel
            {
                Authors = filteredList,
                PagingInfo = new PagingInformation
                {
                    CurrentPage = page,
                    ItemsPerPage = _itemsPerPage,
                    TotalItems = allUsers.Count
                },
                AuthorsCount = allUsers.Count,
                OneTimeCode = GetToken(),
                Title = SettingsRepository.BlogName
            };
            return View(usersModel);
        }

        /// <summary>
        /// Deletes the author. Following is the sequence in which a delete is performed
        /// 
        ///     * Category mappings
        ///     * Tag mappings
        ///     * Comments
        ///     * Posts
        ///     * User itself
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="token">The one time token to check if this is a valid ajax request</param>
        /// <exception cref="NotSupportedException">If the user id is that of an admin</exception>
        /// <returns></returns>
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult DeleteAuthor(int userID, string token)
        {
            if (userID == 1 || !CheckToken(token))
            {
                throw new NotSupportedException("Cannot delete the admin");
            }

            try
            {
                var posts = _postRepository.GetPostsByUserID(userID, 1).Select(p => p.PostID).ToList();

                _categoryRepository.DeletePostCategoryMapping(posts);
                _tagRepository.DeleteTagsForsPosts(posts);
                _commentRepository.DeleteCommentsByPostID(posts);
                _postRepository.DeletePostsByUserID(userID);
                _roleRepository.DeleteRolesForUser(userID);
                _userRepository.DeleteUser(userID);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "SuperAdmin")]
        public JsonResult ActivateDeactivateAuthor(int authorId, int currentStatus, string token)
        {
            if (authorId == 1 || !CheckToken(token))
            {
                throw new UnauthorizedAccessException("Cannot deactivate the admin user");
            }

            var authorStatus = currentStatus == 1;
            var status = _userRepository.ToggleUserActiveStatus(authorId, !authorStatus);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult UpdateUserRole(int userId, short roleId)
        {
            if (userId == 1)
            {
                throw new UnauthorizedAccessException("Cannot modify the role of the admin user");
            }

            _roleRepository.AddRoleForUser(userId, roleId);
            var userEntity = _userRepository.GetUserObjByUserID(userId);
            userEntity.RoleId = roleId;
            return PartialView("UserRole", userEntity);
        }

        public ActionResult ManagePublicPosts([DefaultValue(1)] int page)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var allUsers = _userRepository.GetAllUsers().ToList();
            var allPosts = _postRepository.GetPosts(1, 1);
            var filteredPosts = allPosts.Skip((page - 1) * 5).Take(5).ToList();
            filteredPosts.ForEach(post =>
            {
                var user = allUsers.Single(u => u.UserID == post.OwnerUserID);
                post.OwnerUserName = user.UserDisplayName;
                post.UserName = user.UserName;
            });
            var postModel = new AdminPostOrPageViewModel
            {
                Posts = allPosts.Skip((page - 1) * 5).Take(5).ToList(),
                PagingInfo = new PagingInformation
                {
                    CurrentPage = page,
                    ItemsPerPage = 5,
                    TotalItems = allPosts.Count
                },
                AllPostsCount = allPosts.Count,
                Title = SettingsRepository.BlogName
            };
            return View(postModel);
        }

        public ActionResult ManagePublicComments([DefaultValue(1)] int page, string type)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var status = type != null ? (int)Enum.Parse(typeof(CommentStatus), type, true) : int.MaxValue;
            var posts = _postRepository.GetPosts().Where(p => p.OwnerUserID != 1).ToList();
            var filteredComments = _commentRepository.GetAllComments()
                                                    .Where(c => posts.Select(p => p.PostID)
                                                                     .Contains(c.PostID)).ToList();
            var commentsByType = status == int.MaxValue ? filteredComments : filteredComments.Where(c => c.CommentStatus == status).ToList();
            var commentModel = new AdminCommentViewModel
            {
                Comments = commentsByType.Skip((page - 1) * _itemsPerPage)
                                         .Take(_itemsPerPage)
                                         .ToList()
                                         .GetCommentLinks(posts, status),
                PagingInfo = new PagingInformation
                {
                    CurrentPage = page,
                    ItemsPerPage = _itemsPerPage,
                    TotalItems = commentsByType.Count()
                },
                Type = type,
                AllCommentsCount = filteredComments.Count(),
                ApprovedCommentsCount = filteredComments.Count(c => c.CommentStatus == 0),
                PendingCommentsCount = filteredComments.Count(c => c.CommentStatus == 1),
                SpamCommentsCount = filteredComments.Count(c => c.CommentStatus == 2),
                TrashCommentsCount = filteredComments.Count(c => c.CommentStatus == -1),
                OneTimeCode = GetToken(),
                Title = SettingsRepository.BlogName
            };
            return View(commentModel);
        }
    }
}
