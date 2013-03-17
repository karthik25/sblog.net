using sBlog.Net.Domain.Entities;
using sBlog.Net.Models;

namespace sBlog.Net.FluentExtensions
{
    public static class PostModelExtensions
    {
        public static PostModel ToPostModel(this BlogPostPageViewModel blogPostPageViewModel, PostEntity postEntity)
        {
            var postModel = new PostModel
                {
                    Post = postEntity,
                    DisqusEnabled = blogPostPageViewModel.DisqusEnabled
                };
            return postModel;
        }
    }
}