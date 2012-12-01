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
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Interfaces
{
    public interface ICategory : IDisposable
    {
        List<CategoryEntity> GetCategories();
        List<CategoryEntity> GetCategoriesByPostID(int postID);
        void DeleteCategory(int categoryID);
        void DeleteCategory(string category);
        void UpdateCategoryByID(int id, string newName);
        int AddCategory(CategoryEntity entity);
        void AddPostCategoryMapping(List<CategoryEntity> categoryEntity, int postID);
        void UpdatePostCategoryMapping(List<CategoryEntity> categoryEntity, int postID);
        void DeletePostCategoryMapping(int postID);
        void DeletePostCategoryMapping(IEnumerable<int> postList);
    }
}
