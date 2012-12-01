using System;
using System.Collections.Generic;
using System.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockPost : IPost
    {
        private readonly List<PostEntity> _postsTable;
        private readonly IComment _commentRepository;
        private readonly ITag _tagRepository;
        private readonly ICategory _categoryRepository;
        private readonly IUser _userRepository;

        public MockPost()
        {
            _postsTable = GetMockPosts();
            _commentRepository = new MockComment();
            _tagRepository = new MockTag();
            _categoryRepository = new MockCategory();
            _userRepository = new MockUser();
        }

        public List<PostEntity> GetPostsByUserID(int userID)
        {
            var postEntities = _postsTable.Where(p => p.OwnerUserID == userID)
                                                      .OrderByDescending(p => p.PostEditedDate)
                                                      .ToList();
            return PostProcessEntities(postEntities);
        }

        public List<PostEntity> GetPostsByUserID(int userID, byte entryType)
        {
            var postEntities = _postsTable.Where(p => p.OwnerUserID == userID && p.EntryType == entryType)
                                                      .OrderByDescending(p => p.PostEditedDate)
                                                      .ToList();
            return PostProcessEntities(postEntities);
        }

        public List<PostEntity> GetPages()
        {
            var postEntities = _postsTable.Where(p => !p.IsPrivate && p.EntryType == 2)
                             .OrderByDescending(p => p.PostEditedDate)
                             .ToList();
            return PostProcessEntities(postEntities);
        }

        public List<PostEntity> GetPages(int userID)
        {
            var postEntities = _postsTable.Where(p => (!p.IsPrivate && p.EntryType == 2) || (p.IsPrivate && p.EntryType == 2 && p.OwnerUserID == userID))
                             .OrderByDescending(p => p.PostEditedDate)
                             .ToList();
            return PostProcessEntities(postEntities);
        }

        public List<PostEntity> GetAllPostsOrPages(bool includeAll)
        {
            var postEntities = _postsTable.ToList();
            return !includeAll ? postEntities : PostProcessEntities(postEntities);
        }

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
            }
            return post;
        }

        public PostEntity GetPostByUrl(string url, byte entryType)
        {
            var post = _postsTable.SingleOrDefault(p => p.PostUrl == url && p.EntryType == entryType);
            return post != null ? GetPostByID(post.PostID) : null;
        }

        public List<PostEntity> GetPosts()
        {
            var postEntities = _postsTable.Where(p => !p.IsPrivate && p.EntryType == 1)
                             .OrderByDescending(p => p.PostEditedDate)
                             .ToList();
            return PostProcessEntities(postEntities);
        }

        public List<PostEntity> GetPosts(int userID)
        {
            var postEntities = _postsTable.Where(p => (!p.IsPrivate && p.EntryType == 1) || (p.IsPrivate && p.EntryType == 1 && p.OwnerUserID == userID))
                             .OrderByDescending(p => p.PostEditedDate)
                             .ToList();
            return PostProcessEntities(postEntities);
        }

        public List<PostEntity> GetPosts(int excludeUserID, byte entryType)
        {
            var postEntities = _postsTable.Where(p => p.EntryType == entryType && p.OwnerUserID != excludeUserID && !p.IsPrivate)
                                                      .ToList();
            return PostProcessEntities(postEntities);
        }


        public void DeletePost(int postID)
        {
            throw new NotImplementedException();
        }

        public int AddPost(PostEntity postEntity)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(PostEntity postEntity)
        {
            throw new NotImplementedException();
        }

        public void DeletePostsByUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public List<PostEntity> GetMockPosts()
        {
            var fakePostRepository = new List<PostEntity>();
            var i = 0;

            // Order

            /*
             *  Post Title 14 (p) [4/2012] [ruby]
             *  Post Title 13 (p)
             *  Post Title 12 (p)
             *  Post Title 11 (p)
             *  Post Title 10 (p)
             *  Post Title 9 (p)
             *  Post Title 8 (p)
             *  Post Title 7               [csharp,mvc 3] 
             *  Post Title 6                               [CSharp]
             *  Post Title 5
             *  Post Title 4
             *  Post Title 3
             *  Post Title 2
             *  Post Title 1
             *  Post Title 28 (p) [1/2012] [ruby]
             *  Post Title 27 (p)
             *  Post Title 26 (p)
             *  Post Title 25 (p)
             *  Post Title 24 (p)
             *  Post Title 23 (p)
             *  Post Title 22 (p)
             *  Post Title 21              [csharp,mvc 3]
             *  Post Title 20
             *  Post Title 19
             *  Post Title 18
             *  Post Title 17
             *  Post Title 16
             *  Post Title 15
             * 
             * */

            // Posts
            for (i = 1; i < 8; i++)
            {
                fakePostRepository.Add(new PostEntity { PostID = i, PostTitle = "Post Title " + i, PostContent = "Post Content  " + i, PostAddedDate = DateTime.Parse("04/21/2012").AddMinutes(i), PostEditedDate = DateTime.Parse("04/21/2012").AddMinutes(i), PostUrl = "a-test-url-" + i, EntryType = 1, OwnerUserID = 1 });
            }
            for (i = 8; i < 15; i++)
            {
                fakePostRepository.Add(new PostEntity { PostID = i, PostTitle = "Post Title " + i, PostContent = "Post Content  " + i, PostAddedDate = DateTime.Parse("04/21/2012").AddMinutes(i), PostEditedDate = DateTime.Parse("04/21/2012").AddMinutes(i), PostUrl = "a-test-url-" + i, EntryType = 1, OwnerUserID = 1, IsPrivate = true });
            }
            for (i = 15; i < 22; i++)
            {
                fakePostRepository.Add(new PostEntity { PostID = i, PostTitle = "Post Title " + i, PostContent = "Post Content  " + i, PostAddedDate = DateTime.Parse("01/21/2012").AddMinutes(i), PostEditedDate = DateTime.Parse("01/21/2012").AddMinutes(i), PostUrl = "a-test-url-" + i, EntryType = 1, OwnerUserID = 2 });
            }
            for (i = 22; i < 29; i++)
            {
                fakePostRepository.Add(new PostEntity { PostID = i, PostTitle = "Post Title " + i, PostContent = "Post Content  " + i, PostAddedDate = DateTime.Parse("01/21/2012").AddMinutes(i), PostEditedDate = DateTime.Parse("01/21/2012").AddMinutes(i), PostUrl = "a-test-url-" + i, EntryType = 1, OwnerUserID = 2, IsPrivate = true });
            }

            // Pages
            for (i = 29; i < 35; i++)
            {
                fakePostRepository.Add(new PostEntity { PostID = i, PostTitle = "Page Title " + i, PostContent = "Page Content  " + i, PostAddedDate = DateTime.Parse("01/11/2011"), PostEditedDate = DateTime.Parse("01/11/2011"), PostUrl = "a-test-url-" + i, EntryType = 2, Order = i, OwnerUserID = 1 });
            }
            for (i = 35; i < 41; i++)
            {
                fakePostRepository.Add(new PostEntity { PostID = i, PostTitle = "Page Title " + i, PostContent = "Page Content  " + i, PostAddedDate = DateTime.Parse("01/11/2011"), PostEditedDate = DateTime.Parse("01/11/2011"), PostUrl = "a-test-url-" + i, EntryType = 2, Order = i, OwnerUserID = 1, IsPrivate = true });
            }
            return fakePostRepository;
        }

        private List<PostEntity> PostProcessEntities(List<PostEntity> postEntities)
        {
            var users = _userRepository.GetAllUsers();
            postEntities.ForEach(p =>
            {
                p.Comments = _commentRepository.GetCommentsByPostID(p.PostID);
                p.Categories = _categoryRepository.GetCategoriesByPostID(p.PostID);
                p.Tags = _tagRepository.GetTagsByPostID(p.PostID);
                p.OwnerUserName = users.Single(u => u.UserID == p.OwnerUserID).UserDisplayName;
            });
            return postEntities;
        }

        public void Dispose()
        {
            
        }
    }
}
