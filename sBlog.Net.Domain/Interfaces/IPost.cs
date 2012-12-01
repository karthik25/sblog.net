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
    public interface IPost : IDisposable
    {
        /// <summary>
        /// Gets all the posts & pages by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>a list of PostEntity objects</returns>
        List<PostEntity> GetPostsByUserID(int userID);

        /// <summary>
        /// Get the posts identified by the user ID and entry type
        /// 
        /// entryType could be 1 for posts and 2 for pages
        /// 
        /// Used in the "admin" section.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="entryType">Type of the entry.</param>
        /// <returns>a list of PostEntity objects</returns>
        List<PostEntity> GetPostsByUserID(int userID, byte entryType);

        /// <summary>
        /// Gets the posts identified by entry type for all users except the user id passed
        /// 
        /// entryType could be 1 for posts and 2 for pages
        /// 
        /// Used in the "admin" section (only for the "super" admin"
        /// </summary>
        /// <param name="excludeUserID">The exclude user ID.</param>
        /// <param name="entryType">Type of the entry.</param>
        /// <returns>a list of PostEntity objects</returns>
        List<PostEntity> GetPosts(int excludeUserID, byte entryType);

        /// <summary>
        /// Get all the public "posts" for an un-authenticated user
        /// 
        /// Used in the "blog" section
        /// </summary>
        /// <returns>a list of PostEntity objects</returns>
        List<PostEntity> GetPosts();

        /// <summary>
        /// Get all public "posts" + private "posts" for the authenticated user
        /// 
        /// Used in the "blog" section
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>a list of PostEntity objects</returns>
        List<PostEntity> GetPosts(int userID);

        /// <summary>
        /// Get all the public "pages" for an un-authenticated user
        /// 
        /// Used in the "blog" section
        /// </summary>
        /// <returns>a list of PostEntity objects</returns>
        List<PostEntity> GetPages();
        
        /// <summary>
        /// Get all public "pages" + private "pages" for the authenticated user
        /// 
        /// Used in the "blog" section
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>a list of PostEntity objects</returns>
        List<PostEntity> GetPages(int userID);

        /// <summary>
        /// Get all posts or pages.
        /// 
        /// At this point used for the helper that helps users find a unique url
        /// </summary>
        /// <returns>a list of PostEntity objects</returns>
        List<PostEntity> GetAllPostsOrPages(bool includeAll);

        /// <summary>
        /// Get a post by ID.
        /// </summary>
        /// <param name="postID">The post ID.</param>
        /// <returns>A single PostEntity</returns>
        PostEntity GetPostByID(int postID);

        /// <summary>
        /// Get a post by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="entryType">Type of the entry.</param>
        /// <returns>A single PostEntity</returns>
        PostEntity GetPostByUrl(string url, byte entryType);

        /// <summary>
        /// Delete a post identified by the id.
        /// </summary>
        /// <param name="postID">The post ID.</param>
        void DeletePost(int postID);

        /// <summary>
        /// Add a post. It also should take care of cateogries/tags.
        /// </summary>
        /// <param name="postEntity">The post entity.</param>
        /// <returns></returns>
        int AddPost(PostEntity postEntity);

        /// <summary>
        /// Update a post. It also should take care of cateogries/tags.
        /// </summary>
        /// <param name="postEntity">The post entity.</param>
        void UpdatePost(PostEntity postEntity);

        /// <summary>
        /// Delete all posts identified by the user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        void DeletePostsByUserID(int userID);
    }
}
