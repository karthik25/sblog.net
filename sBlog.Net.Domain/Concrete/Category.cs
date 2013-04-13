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
/* Category.cs 
 * 
 * This class extends the DefaultDisposable class,
 * Which implements the IDisposable interface for this class.
 * 
 * If you modify the class to add more disposable managed
 * resources, you can remove DefaultDisposable and implement
 * the Dispose() method yourself
 * 
 * */
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Concrete
{
    public class Category : System.Data.Entity.DbContext, ICategory
    {
        public IDbSet<CategoryEntity> Categories { get; set; }
        public IDbSet<CategoryMapping> CategoryMappings { get; set; }

        public Category()
            : base("AppDb")
        {

        }

        public List<CategoryEntity> GetCategories()
        {
            return Categories.OrderBy(c => c.CategoryID).ToList();
        }

        public List<CategoryEntity> GetCategoriesByPostID(int postID)
        {            
            var categoriesEntities = new List<CategoryEntity>();
            var allCategories = GetCategories();
            var postCategoryMappings = CategoryMappings.Where(m => m.PostID == postID).ToList();

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
            IEnumerable<CategoryMapping> currentMappings = CategoryMappings.Where(c => c.CategoryID == categoryID);
            if (currentMappings.Any())
            {
                foreach (var categoryMapping in currentMappings)
                {
                    CategoryMappings.Remove(categoryMapping);
                }
                SaveChanges();
            }

            var entity = Categories.SingleOrDefault(c => c.CategoryID == categoryID);
            if (entity != null)
            {
                Categories.Remove(entity);
                SaveChanges();
            }
        }

        public void DeleteCategory(string category)
        {
            var categoryEntity = Categories.SingleOrDefault(c => c.CategoryName == category);
            
            if (categoryEntity != null)
            {
                DeleteCategory(categoryEntity.CategoryID);
            }
        }

        public void UpdateCategoryByID(int id, string newName)
        {
            var categoryEntity = Categories.SingleOrDefault(c => c.CategoryID == id);
            if (categoryEntity != null)
            {
                categoryEntity.CategoryName = newName;
                SaveChanges();
            }
        }

        public int AddCategory(CategoryEntity entity)
        {
            if (entity != null)
            {
                Categories.Add(entity);
                SaveChanges();
                return entity.CategoryID;
            }
            return -1;
        }

        public void AddPostCategoryMapping(List<CategoryEntity> categoryEntity, int postID)
        {
            var postCategoryMappings = new List<CategoryMapping>();

            categoryEntity.ForEach(c => postCategoryMappings.Add(new CategoryMapping
                                                                     {
                                                                         CategoryID = c.CategoryID,
                                                                         PostID = postID
                                                                     }));
            
            foreach (var postCategoryMapping in postCategoryMappings)
            {
                CategoryMappings.Add(postCategoryMapping);
            }

            SaveChanges();
        }

        public void UpdatePostCategoryMapping(List<CategoryEntity> categoryEntity, int postID)
        {
            var postCategoryMappings = new List<CategoryMapping>();
            var postMappings = CategoryMappings.Where(p => p.PostID == postID).ToList();
            categoryEntity.ForEach(c => postCategoryMappings.Add(new CategoryMapping { CategoryID = c.CategoryID, PostID = postID }));

            foreach (var postMapping in postMappings)
            {
                CategoryMappings.Remove(postMapping);
            }

            foreach (var postCategoryMapping in postCategoryMappings)
            {
                CategoryMappings.Add(postCategoryMapping);
            }

            SaveChanges();
        }

        public void DeletePostCategoryMapping(int postID)
        {
            var mappings = CategoryMappings.Where(m => m.PostID == postID).ToList();
            if (mappings.Count > 0)
            {
                foreach (var categoryMapping in mappings)
                {
                    CategoryMappings.Remove(categoryMapping);
                }
                SaveChanges();
            }
        }

        public void DeletePostCategoryMapping(IEnumerable<int> postList)
        {
            var mappings = CategoryMappings.Where(c => postList.Contains(c.PostID));
            if (mappings.Any())
            {
                foreach (var categoryMapping in mappings)
                {
                    CategoryMappings.Remove(categoryMapping);
                }
                SaveChanges();
            }
        }
    }
}
