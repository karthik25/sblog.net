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
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;
using System.Data.Linq;

namespace sBlog.Net.Domain.Concrete
{
    public class Category : DefaultDisposable, ICategory
    {
        private readonly Table<CategoryEntity> _categoriesTable;
        private readonly Table<CategoryMapping> _postCategoryMapping;

        public Category()
        {
            _categoriesTable = context.GetTable<CategoryEntity>();
            _postCategoryMapping = context.GetTable<CategoryMapping>();
        }

        public List<CategoryEntity> GetCategories()
        {
            return _categoriesTable.OrderBy(c => c.CategoryID).ToList();
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
            IEnumerable<CategoryMapping> currentMappings = _postCategoryMapping.Where(c => c.CategoryID == categoryID);
            if (currentMappings.Any())
            {
                _postCategoryMapping.DeleteAllOnSubmit(currentMappings);
                context.SubmitChanges();
            }

            var entity = _categoriesTable.SingleOrDefault(c => c.CategoryID == categoryID);
            if (entity != null)
            {
                _categoriesTable.DeleteOnSubmit(entity);
                context.SubmitChanges();
            }
        }

        public void DeleteCategory(string category)
        {
            var categoryEntity = _categoriesTable.SingleOrDefault(c => c.CategoryName == category);
            
            if (categoryEntity != null)
            {
                DeleteCategory(categoryEntity.CategoryID);
            }
        }

        public void UpdateCategoryByID(int id, string newName)
        {
            var categoryEntity = _categoriesTable.SingleOrDefault(c => c.CategoryID == id);
            if (categoryEntity != null)
            {
                categoryEntity.CategoryName = newName;
                context.SubmitChanges();
            }
        }

        public int AddCategory(CategoryEntity entity)
        {
            if (entity != null)
            {
                _categoriesTable.InsertOnSubmit(entity);
                context.SubmitChanges();
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

            _postCategoryMapping.InsertAllOnSubmit(postCategoryMappings);
            context.SubmitChanges();
        }

        public void UpdatePostCategoryMapping(List<CategoryEntity> categoryEntity, int postID)
        {
            var postCategoryMappings = new List<CategoryMapping>();
            var postMappings = _postCategoryMapping.Where(p => p.PostID == postID).ToList();
            categoryEntity.ForEach(c => postCategoryMappings.Add(new CategoryMapping { CategoryID = c.CategoryID, PostID = postID }));
            _postCategoryMapping.DeleteAllOnSubmit(postMappings);
            _postCategoryMapping.InsertAllOnSubmit(postCategoryMappings);
            context.SubmitChanges();
        }

        public void DeletePostCategoryMapping(int postID)
        {
            var mappings = _postCategoryMapping.Where(m => m.PostID == postID).ToList();
            if (mappings.Count > 0)
            {
                _postCategoryMapping.DeleteAllOnSubmit(mappings);
                context.SubmitChanges();
            }
        }

        public void DeletePostCategoryMapping(IEnumerable<int> postList)
        {
            var mappings = _postCategoryMapping.Where(c => postList.Contains(c.PostID));
            if (mappings.Any())
            {
                _postCategoryMapping.DeleteAllOnSubmit(mappings);
                context.SubmitChanges();
            }
        }

        ~Category()
        {
            Dispose(false);
        }
    }
}
