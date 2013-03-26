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
/* Post.cs 
 * 
 * This class extends the DefaultDisposable class,
 * Which implements the IDisposable interface for this class.
 * 
 * If you modify the class to add more disposable managed
 * resources, you can remove DefaultDisposable and implement
 * the Dispose() method yourself
 * 
 * */

using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Concrete
{
    public class Post : DefaultDisposable, IPost
    {
        private readonly Table<PostEntity> _postsTable;
        private readonly IComment _commentRepository;
        private readonly ITag _tagRepository;
        private readonly ICategory _categoryRepository;
        private readonly IUser _userRepository;

        public Post(IUser userRepository, ICategory categoryRepository, ITag tagRepository, IComment commentRepository)
        {
            _postsTable = context.GetTable<PostEntity>();
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets all the posts & pages by user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>a list of PostEntity objects</returns>
        public List<PostEntity> GetPostsByUserID(int userID)
        {
            var postEntities = _postsTable.Where(p => p.OwnerUserID == userID)
                                                      .OrderByDescending(p => p.PostEditedDate)
                                                      .ToList();
            return PostProcessEntities(postEntities);
        }

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
        public List<PostEntity> GetPostsByUserID(int userID, byte entryType)
        {
            var postEntities = _postsTable.Where(p => p.OwnerUserID == userID && p.EntryType == entryType)
                                                      .OrderByDescending(p => p.PostEditedDate)
                                                      .ToList();
            return PostProcessEntities(postEntities);
        }

        /// <summary>
        /// Get a post by ID.
        /// </summary>
        /// <param name="postID">The post ID.</param>
        /// <returns>A single PostEntity</returns>
        public PostEntity GetPostByID(int postID)
        {
            var post = _postsTable.SingleOrDefault(p => p.PostID == postID);
            if (post != null)
            {
                var user = _userRepository.GetAllUsers().Single(u => u.UserID == post.OwnerUserID);
                post.Comments = _commentRepository.GetCommentsByPostID(post.PostID);
                post.Categories = _categoryRepository.GetCategoriesByPostID(post.PostID);
                post.Tags = _tagRepository.GetTagsByPostID(post.PostID);
                post.OwnerUserName = user.UserDisplayName;
                post.UserName = user.UserName;
            }
            return post;
        }

        /// <summary>
        /// Get a post by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="entryType">Type of the entry.</param>
        /// <returns>A single PostEntity</returns>
        public PostEntity GetPostByUrl(string url, byte entryType)
        {
            var post = _postsTable.SingleOrDefault(p => p.PostUrl == url && p.EntryType == entryType);
            return post != null ? GetPostByID(post.PostID) : null;
        }

        /// <summary>
        /// Get all the public "posts" for an un-authenticated user
        /// 
        /// Used in the "blog" section
        /// </summary>
        /// <returns>a list of PostEntity objects</returns>
        public List<PostEntity> GetPosts()
        {
            var postEntities = _postsTable.Where(p => !p.IsPrivate && p.EntryType == 1)
                             .OrderByDescending(p => p.PostEditedDate)
                             .ToList();
            return PostProcessEntities(postEntities);
        }

        /// <summary>
        /// Get all public "posts" + private "posts" for the authenticated user
        /// 
        /// Used in the "blog" section
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>a list of PostEntity objects</returns>
        public List<PostEntity> GetPosts(int userID)
        {
            var postEntities = _postsTable.Where(p => (!p.IsPrivate && p.EntryType == 1) || (p.IsPrivate && p.EntryType == 1 && p.OwnerUserID == userID))
                             .OrderByDescending(p => p.PostEditedDate)
                             .ToList();
            return PostProcessEntities(postEntities);
        }

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
        public List<PostEntity> GetPosts(int excludeUserID, byte entryType)
        {
            var postEntities = _postsTable.Where(p => p.EntryType == entryType && p.OwnerUserID != excludeUserID && !p.IsPrivate)
                                                      .ToList();
            return PostProcessEntities(postEntities);
        }

        /// <summary>
        /// Get all the public "pages" for an un-authenticated user
        /// 
        /// Used in the "blog" section
        /// </summary>
        /// <returns>a list of PostEntity objects</returns>
        public List<PostEntity> GetPages()
        {
            var postEntities = _postsTable.Where(p => !p.IsPrivate && p.EntryType == 2)
                               .ToList();
            return PostProcessEntities(postEntities);
        }

        /// <summary>
        /// Get all public "pages" + private "pages" for the authenticated user
        /// 
        /// Used in the "blog" section
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>a list of PostEntity objects</returns>
        public List<PostEntity> GetPages(int userID)
        {
            var postEntities = _postsTable.Where(p => (!p.IsPrivate && p.EntryType == 2) || (p.IsPrivate && p.EntryType == 2 && p.OwnerUserID == userID))
                               .ToList();
            return PostProcessEntities(postEntities);
        }

        /// <summary>
        /// Get all posts or pages.
        /// 
        /// At this point used for the helper that helps users find a unique url
        /// </summary>
        /// <returns>a list of PostEntity objects</returns>
        public List<PostEntity> GetAllPostsOrPages(bool includeAll)
        {
            var postEntities = _postsTable.ToList();
            return !includeAll ? postEntities : PostProcessEntities(postEntities);
        }

        /// <summary>
        /// Delete a post identified by the id.
        /// </summary>
        /// <param name="postID">The post ID.</param>
        public void DeletePost(int postID)
        {
            var post = _postsTable.SingleOrDefault(p => p.PostID == postID);

            if (post != null)
            {
                _commentRepository.DeleteCommentsByPostID(postID);
                _tagRepository.DeleteTagsForPost(postID);
                _categoryRepository.DeletePostCategoryMapping(postID);
                _postsTable.DeleteOnSubmit(post);
                context.SubmitChanges();
            }
        }

        /// <summary>
        /// Update a post. It also should take care of cateogries/tags.
        /// </summary>
        /// <param name="postEntity">The post entity.</param>
        public void UpdatePost(PostEntity postEntity)
        {
            try
            {
                UpdatePostInternal(postEntity);

                _categoryRepository.UpdatePostCategoryMapping(postEntity.Categories, postEntity.PostID);

                if (postEntity.Tags != null)
                    _tagRepository.UpdateTagsForPost(postEntity.Tags, postEntity.PostID);
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// Delete all posts identified by the user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void DeletePostsByUserID(int userID)
        {
            var posts = _postsTable.Where(p => p.OwnerUserID == userID);
            if (posts.Any())
            {
                _postsTable.DeleteAllOnSubmit(posts);
                context.SubmitChanges();
            }
        }

        /// <summary>
        /// Add a post. It also should take care of cateogries/tags.
        /// </summary>
        /// <param name="postEntity">The post entity.</param>
        /// <returns></returns>
        public int AddPost(PostEntity postEntity)
        {
            var postID = -1;
            try
            {
                // add the post
                postID = AddPostInternal(postEntity);

                // add the categories
                _categoryRepository.AddPostCategoryMapping(postEntity.Categories, postID);

                // add the tags
                if (postEntity.Tags != null)
                    _tagRepository.AddTagsForPost(postEntity.Tags, postID);

                // return the id
                return postID;
            }
            catch
            {
                // delete categories/tags if any                
                if (postID > 0)
                {
                    _categoryRepository.DeletePostCategoryMapping(postID);
                    _tagRepository.DeleteTagsForPost(postID);
                }
                
                // delete the post
                DeletePost(postID);

                return -1;
            }
        }

        private int AddPostInternal(PostEntity postEntity)
        {
            _postsTable.InsertOnSubmit(postEntity);
            context.SubmitChanges();
            return postEntity.PostID;
        }

        private void UpdatePostInternal(PostEntity postEntity)
        {
            var post = _postsTable.SingleOrDefault(p => p.PostID == postEntity.PostID);
            if (post != null)
            {
                post.PostTitle = postEntity.PostTitle;
                post.PostContent = postEntity.PostContent;
                post.PostUrl = postEntity.PostUrl;
                post.PostEditedDate = postEntity.PostEditedDate;
                post.UserCanAddComments = postEntity.UserCanAddComments;
                post.CanBeShared = postEntity.CanBeShared;
                post.IsPrivate = postEntity.IsPrivate;
                post.EntryType = postEntity.EntryType;
                post.Order = postEntity.Order.HasValue ? postEntity.Order.Value : (int?)null;

                context.SubmitChanges();
            }
        }

        private List<PostEntity> PostProcessEntities(List<PostEntity> postEntities)
        {
            var users = _userRepository.GetAllUsers().ToList();
            postEntities.ForEach(p =>
            {
                p.Comments = _commentRepository.GetCommentsByPostID(p.PostID);
                p.Categories = _categoryRepository.GetCategoriesByPostID(p.PostID);
                p.Tags = _tagRepository.GetTagsByPostID(p.PostID);

                var user = users.Single(u => u.UserID == p.OwnerUserID);
                p.OwnerUserName = user.UserDisplayName;
                p.UserName = user.UserName;
            });
            return postEntities;
        }

        ~Post()
        {
            Dispose(false);
        }
    }
}
