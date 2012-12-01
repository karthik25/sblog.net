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
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Models;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.FluentExtensions
{
    public static class PostViewModelExtensions
    {
        public static PostEntity ToPostEntity(this PostViewModel postViewModel, ITag tagRepository)
        {
            var postEntity = postViewModel.Post;
            postEntity.Categories = GetSelectedCategories(postViewModel.Categories);
            if (!string.IsNullOrEmpty(postViewModel.Tags))
                postEntity.Tags = GetSelectedTags(postViewModel.Tags, tagRepository);
            return postEntity;
        }

        private static List<TagEntity> GetSelectedTags(string selectedTags, ITag tagRepository)
        {
            var tagStatus = new List<TagEntity>();

            if (selectedTags.Trim() != string.Empty)
            {
                var selectedTagsSplit = selectedTags.Split(',').ToList();
                var allTags = tagRepository.GetAllTags();

                selectedTagsSplit.ForEach(tag =>
                {
                    var tagEntity = new TagEntity { TagID = allTags.Single(t => t.TagName.ToLower() == tag.ToLower()).TagID };
                    tagStatus.Add(tagEntity);
                });
            }

            return tagStatus;
        }

        private static List<CategoryEntity> GetSelectedCategories(CheckBoxListViewModel checkBoxListViewModel)
        {
            var categories = new List<CategoryEntity>();
            if (checkBoxListViewModel != null)
            {
                var selectedItems = checkBoxListViewModel.Items.Where(c => c.IsChecked).ToList();
                selectedItems.ForEach(i => categories.Add(new CategoryEntity { CategoryID = Int32.Parse(i.Value) }));
            }
            return categories;
        }
    }
}
