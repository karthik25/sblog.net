using System;
using System.Data.Entity;

namespace sBlog.Net.Plugins.PostViewCount
{
    public class PostViewCountDataContext : DbContext
    {
        public IDbSet<View> Views { get; set; }

        public PostViewCountDataContext()
            : base("Server=localhost;Database=sblog;user id=msuser1;password=msuser1;")
        {
            
        }

        public void InsertPostView(int postId, string postUrl)
        {
            var view = new View { PostId = postId, PostUrl = postUrl, ViewDate = DateTime.Now };
            Views.Add(view);
            SaveChanges();
        }
    }
}