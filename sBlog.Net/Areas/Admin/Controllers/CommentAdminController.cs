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
using sBlog.Net.Models;
using System.ComponentModel;
using sBlog.Net.Controllers;
using sBlog.Net.Enumerations;
using sBlog.Net.FluentExtensions;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.Areas.Admin.Models.Comments;
using sBlog.Net.Areas.Admin.Models.JsonEntities;

namespace sBlog.Net.Areas.Admin.Controllers
{
    public class CommentAdminController : BlogController
    {
        private readonly IPost _postRepository;
        private readonly IComment _commentRepository;

        private readonly int _itemsPerPage;

        public CommentAdminController(IPost postRepository, IComment commentRepository, ISettings settingsRepository)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            ExpectedMasterName = string.Empty;

            _itemsPerPage = settingsRepository.ManageItemsPerPage;

            IsAdminController = true;
        }

        [Authorize]
        public ActionResult ManageComments([DefaultValue(1)] int page, string type)
        {
            var userId = GetUserId();
            var status = type != null ? (int)Enum.Parse(typeof(CommentStatus), type, true) : int.MaxValue;
            var posts = _postRepository.GetPostsByUserID(userId);
            var filteredComments = _commentRepository.GetAllComments()
                                                    .Where(c => posts.Select(p => p.PostID)
                                                                     .Contains(c.PostID))
                                                    .ToList();
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

        public ActionResult CommentPartial(CommentLink commentLink)
        {
            if (Request.IsAjaxRequest())
            {
                _commentRepository.UpdateCommentStatus(commentLink.CommentID, commentLink.CommentStatus);
                var links = commentLink.GetLinks();
                return PartialView("CommentPartial", links);
            }
            throw new NotSupportedException("Not an ajax request");
        }

        public string CommentPartialReplacer(CommentLink commentLink)
        {
            if (Request.IsAjaxRequest())
            {
                _commentRepository.UpdateCommentStatus(commentLink.CommentID, commentLink.CommentStatus);
                return "Comment status has been updated";
            }
            throw new NotSupportedException("Not an ajax request");
        }

        [Authorize]
        [HttpGet]
        public JsonResult TrashComment(int commentId, string token)
        {
            if (!CheckToken(token))
            {
                throw new Exception("Possible unauthorized access");
            }

            var commentEntry = new CommentEntry { CommentID = commentId, DeleteStatus = true, DeleteStatusString = "Delete succeeded" };
            try
            {
                _commentRepository.DeleteCommentByCommentID(commentId);
            }
            catch 
            {
                commentEntry.DeleteStatus = false;
                commentEntry.DeleteStatusString = "Unable to delete the comment";
            }
            return Json(commentEntry, JsonRequestBehavior.AllowGet);
        }
    }
}
