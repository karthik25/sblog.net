using System;
using System.Collections.Generic;
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockCategory : ICategory
    {
        private readonly List<CategoryEntity> _categoriesTable;
        private readonly List<CategoryMapping> _postCategoryMapping;

        public MockCategory()
        {
            _categoriesTable = GetMockCategories();
            _postCategoryMapping = GetMockMappings();
        }

        public List<CategoryEntity> GetCategories()
        {
            return _categoriesTable.ToList();
        }

        public List<CategoryEntity> GetCategoriesByPostID(int postID)
        {
            var categoriesEntities = new List<CategoryEntity>();
            var allCategories = GetCategories();
            var postCategoryMappings = _postCategoryMapping.Where(m => m.PostID == postID).ToList();

            postCategoryMappings.ForEach(mapping =>
            {
                var category = allCategories.Single(c => c.CategoryID == mapping.CategoryID);
                var categoryEntity = new CategoryEntity { CategoryID = mapping.CategoryID, CategoryName = category.CategoryName, CategorySlug = category.CategorySlug };
                categoriesEntities.Add(categoryEntity);
            });

            return categoriesEntities;
        }

        public void DeleteCategory(int categoryID)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(string category)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategoryByID(int id, string newName)
        {
            throw new NotImplementedException();
        }

        public int AddCategory(CategoryEntity entity)
        {
            throw new NotImplementedException();
        }

        public void AddPostCategoryMapping(List<CategoryEntity> categoryEntity, int postID)
        {
            throw new NotImplementedException();
        }

        public void UpdatePostCategoryMapping(List<CategoryEntity> categoryEntity, int postID)
        {
            throw new NotImplementedException();
        }

        public void DeletePostCategoryMapping(int postID)
        {
            throw new NotImplementedException();
        }

        public void DeletePostCategoryMapping(IEnumerable<int> postList)
        {
            throw new NotImplementedException();
        }

        private static List<CategoryEntity> GetMockCategories()
        {
            var categories = new List<CategoryEntity>
                                 {
                                     new CategoryEntity {CategoryID = 1, CategoryName = "CSharp", CategorySlug = "csharp" },
                                     new CategoryEntity {CategoryID = 2, CategoryName = "Ruby", CategorySlug = "ruby"}
                                 };

            return categories;
        }

        private static List<CategoryMapping> GetMockMappings()
        {
            var postCatMapping = new List<CategoryMapping>();
            int i;

            // 4 * 2 + 3 * 2 - Ruby, 3 * 2 + 4 * 2 - CSharp

            for (i = 1; i < 8; i++)
            {
                var categoryID = i % 2 == 0 ? 1 : 2;
                postCatMapping.Add(new CategoryMapping { PostCategoryMappingID = i, CategoryID = categoryID, PostID = i });
            }
            for (i = 8; i < 15; i++)
            {
                var categoryID = i % 2 == 0 ? 1 : 2;
                postCatMapping.Add(new CategoryMapping { PostCategoryMappingID = i, CategoryID = categoryID, PostID = i });
            }
            for (i = 15; i < 22; i++)
            {
                var categoryID = i % 2 == 0 ? 1 : 2;
                postCatMapping.Add(new CategoryMapping { PostCategoryMappingID = i, CategoryID = categoryID, PostID = i });
            }
            for (i = 22; i < 29; i++)
            {
                var categoryID = i % 2 == 0 ? 1 : 2;
                postCatMapping.Add(new CategoryMapping { PostCategoryMappingID = i, CategoryID = categoryID, PostID = i });
            }

            return postCatMapping;
        }

        public void Dispose()
        {
            
        }
    }
}
