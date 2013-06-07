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
using System.ComponentModel;
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.Models;
using sBlog.Net.Controllers;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.Areas.Admin.Controllers
{    
    public class TagAdminController : BlogController
    {
        private readonly ITag _tagRepository;

        private readonly int _itemsPerPage;

        public TagAdminController(ITag tagRepository, ISettings settingsRepository)
            : base(settingsRepository)
        {
            _tagRepository = tagRepository;
            ExpectedMasterName = string.Empty;

            _itemsPerPage = settingsRepository.ManageItemsPerPage;

            IsAdminController = true;
        }

        [Authorize]
        public ActionResult ManageTags([DefaultValue(1)] int page)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var allTags = _tagRepository.GetAllTags();
            var tagModel = new AdminTagsViewModel
            {
                Tags = allTags.Skip((page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList(),
                PagingInfo = new PagingInformation
                {
                    CurrentPage = page,
                    ItemsPerPage = _itemsPerPage,
                    TotalItems = allTags.Count
                },
                OneTimeCode = GetToken(),
                Title = SettingsRepository.BlogName
            };
            return View(tagModel);
        }

        [Authorize]
        public ActionResult AddTagPartial(string tagName, string token)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            if (Request.IsAjaxRequest() && CheckToken(token))
            {
                TagEntity tagEntity = null;
                var newTag = _tagRepository.GetTagEntities(new List<string> { tagName }).FirstOrDefault();
                if (newTag != null)
                {
                    _tagRepository.AddTags(new List<TagEntity> { newTag });
                    tagEntity = _tagRepository.GetAllTags().Single(t => t.TagName.ToLower() == tagName.ToLower());
                }
                return PartialView("Tag", tagEntity);
            }
            throw new NotSupportedException("Request is not an ajax request/Possible unauthorized access");
        }

        [Authorize]
        [HttpGet]
        public JsonResult DeleteTag(int tagId, string token)
        {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
            {
                throw new Exception("Possible unauthorized access");
            }

            if (!CheckToken(token))
            {
                throw new Exception("Possible unauthorized access");
            }

            _tagRepository.DeleteTag(tagId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
