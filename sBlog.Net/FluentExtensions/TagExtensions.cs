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
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Generators;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.FluentExtensions
{
    public static class TagExtensions
    {
        public static List<TagEntity> GetTagEntities(this ITag tagRepository, List<string> tags)
        {
            var newTags = new List<string>();
            var allTags = tagRepository.GetAllTags();

            var existingTags = allTags.Select(t => t.TagName.ToLower()).ToList();
            
            tags.ForEach(tag =>
                {
                    if (tag != null && tag.Trim() != string.Empty && !existingTags.Contains(tag.ToLower()) && newTags.SingleOrDefault(newtag => newtag.ToLower() == tag.ToLower()) == null)
                        newTags.Add(tag.Trim());
                });

            var allSlugs = allTags.Select(t => t.TagSlug).ToList();

            var newTagsList = new List<TagEntity>();
            newTags.ForEach(tag =>
                {
                    var slug = tag.GetUniqueSlug(allSlugs);
                    allSlugs.Add(slug);
                    newTagsList.Add(new TagEntity {TagName = tag, TagSlug = slug});
                });

            return newTagsList;
        }
    }
}