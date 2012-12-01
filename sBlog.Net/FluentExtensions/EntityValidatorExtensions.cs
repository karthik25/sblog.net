using sBlog.Net.Domain.Entities;

namespace sBlog.Net.FluentExtensions
{
    public static class EntityValidatorExtensions
    {
        public static bool IsTagEntityValid(this TagEntity tagEntity)
        {
            return tagEntity != null && ValidateIfNullOrEmpty(tagEntity.TagName, tagEntity.TagSlug);
        }

        public static bool IsCategoryEntityValid(this CategoryEntity categoryEntity)
        {
            return categoryEntity != null && ValidateIfNullOrEmpty(categoryEntity.CategoryName, categoryEntity.CategorySlug);
        }

        private static bool ValidateIfNullOrEmpty(string name, string slug)
        {
            var itemName = name;
            if (itemName != null)
                itemName = itemName.Trim();

            var itemSlug = slug;
            if (itemSlug != null)
                itemSlug = itemSlug.Trim();

            return !string.IsNullOrEmpty(itemName) && !string.IsNullOrEmpty(itemSlug);
        }
    }
}
