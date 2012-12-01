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
using sBlog.Net.Domain.Interfaces;
using System.ComponentModel;
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.Models;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Controllers;
using sBlog.Net.Domain.Generators;

namespace sBlog.Net.Areas.Admin.Controllers
{
    public class CategoryAdminController : BlogController
    {
        private readonly ICategory _categoryRepository;
        private readonly IPost _postRepository;

        private readonly int _itemsPerPage;

        public CategoryAdminController(ICategory categoryRepository, IPost postRepository, ISettings settingsRepository)
            : base(settingsRepository)
        {
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
            ExpectedMasterName = string.Empty;

            _itemsPerPage = settingsRepository.ManageItemsPerPage;

            IsAdminController = true;
        }

        [Authorize(Users="admin")]
        public ActionResult ManageCategories([DefaultValue(1)] int page)
        {
            var allCategories = _categoryRepository.GetCategories();
            var categoryModel = new AdminCategoriesViewModel
            {
                Categories = allCategories.Skip((page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList(),
                PagingInfo = new PagingInformation
                {
                    CurrentPage = page,
                    ItemsPerPage = _itemsPerPage,
                    TotalItems = allCategories.Count
                },
                OneTimeCode = GetToken(),
                Title = SettingsRepository.BlogName
            };
            return View(categoryModel);
        }

        [Authorize]
        public JsonResult AddCategory(string categoryName)
        {
            if (Request.IsAjaxRequest())
            {
                var entity = AddCategoryInternal(categoryName) ?? new CategoryEntity();
                return Json(entity, JsonRequestBehavior.AllowGet);
            }
            throw new NotSupportedException("Request is not an ajax request");
        }

        [Authorize(Users = "admin")]
        [HttpGet]
        public ActionResult AddCategoryPartial(string categoryName, string token)
        {
            if (Request.IsAjaxRequest() && CheckToken(token))
            {
                var entity = AddCategoryInternal(categoryName);
                return PartialView("Category", entity);
            }
            throw new NotSupportedException("Request is not an ajax request/Possible unauthorized access");
        }

        [Authorize(Users = "admin")]
        [HttpGet]
        public JsonResult DeleteCategory(int categoryId, string token)
        {            
            if (!CheckToken(token))
            {
                throw new Exception("Possible unauthorized access");
            }

            if (categoryId != 1)
            {
                // delete the category
                _categoryRepository.DeleteCategory(categoryId);

                // because this category was deleted, 
                // some posts may not have a category mapped
                // so, add the default category for these posts
                var postsWithNoCategory = _postRepository.GetAllPostsOrPages(true)
                                                         .Where(p => p.Categories.Count == 0)
                                                         .Select(p => p.PostID)
                                                         .ToList();
                postsWithNoCategory.ForEach(post =>
                    {
                        var defaultCategory = _categoryRepository.GetCategories().SingleOrDefault(c => c.CategoryID == 1);
                        if (defaultCategory != null)
                        {
                            var categoryList = new List<CategoryEntity> { defaultCategory };
                            _categoryRepository.UpdatePostCategoryMapping(categoryList, post);
                        }
                    });
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private CategoryEntity AddCategoryInternal(string categoryName)
        {
            var finalCategoryName = categoryName;
            if (finalCategoryName != null)
                finalCategoryName = finalCategoryName.Trim();
            var allCategories = _categoryRepository.GetCategories();
            if (!string.IsNullOrEmpty(finalCategoryName) && allCategories.SingleOrDefault(c => c.CategoryName.ToLower() == finalCategoryName.ToLower()) == null)
            {
                var categories = allCategories.Select(c => c.CategorySlug).ToList();
                var categorySlug = finalCategoryName.GetUniqueSlug(categories);
                var entity = new CategoryEntity {CategoryName = finalCategoryName, CategorySlug = categorySlug};
                entity.CategoryID = _categoryRepository.AddCategory(entity);
                return entity;
            }
            return null;
        }
    }
}
