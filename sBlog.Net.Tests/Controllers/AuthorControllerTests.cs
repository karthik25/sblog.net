using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Controllers;
using sBlog.Net.Models;
using sBlog.Net.Tests.Helpers;
using sBlog.Net.Tests.MockFrameworkObjects;

namespace sBlog.Net.Tests.Controllers
{
    [TestClass]
    public class AuthorControllerTests
    {
        [TestInitialize]
        public void Setup()
        {
            HttpContext.Current = MockHelperFactory.FakeHttpContext();
        }

        [TestMethod]
        public void Can_Return_Posts_By_Author()
        {
            var httpContext = GetHttpContext(true, 1);
            var postController = GetAuthorControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.AuthorListing(null);
            var model = result.ViewData.Model as AuthorListingViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(5, model.Authors.Count);
            var author = model.Authors.SingleOrDefault(a => a.UserID == 1);
            Assert.IsNotNull(author);
            Assert.AreEqual(7, author.Posts.Count);
            Assert.AreEqual("admin",author.UserName);
            Assert.AreEqual("Admin", author.UserDisplayName);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Author_Page_2()
        {
            var httpContext = GetHttpContext(true, 1);
            var authorController = GetAuthorControllerInstance(httpContext);
            authorController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)authorController.AuthorListing(2);
            var model = result.ViewData.Model as AuthorListingViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Authors.Count);
            var author = model.Authors.SingleOrDefault(a => a.UserID == 6);
            Assert.IsNotNull(author);
            Assert.AreEqual(0, author.Posts.Count);
        }

        [TestMethod]
        public void Can_Return_Posts_For_Author()
        {
            var httpContext = GetHttpContext(true, 1);
            var authorController = GetAuthorControllerInstance(httpContext);
            authorController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)authorController.PostsByAuthor("admin", null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 14", posts.Single(p => p.Post.PostID == 14).Post.PostTitle);
            Assert.AreEqual(1, posts.First().Post.OwnerUserID);
        }

        [TestMethod]
        public void Can_Return_Posts_For_Author_Page_2()
        {
            var httpContext = GetHttpContext(true, 1);
            var authorController = GetAuthorControllerInstance(httpContext);
            authorController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)authorController.PostsByAuthor("admin", 2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual(1, posts.First().Post.OwnerUserID);
        }

        [TestMethod]
        public void Can_Return_Posts_For_Another_Author()
        {
            var httpContext = GetHttpContext(true, 1);
            var authorController = GetAuthorControllerInstance(httpContext);
            authorController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)authorController.PostsByAuthor("karthik", null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 20", posts.Single(p => p.Post.PostID == 20).Post.PostTitle);
            Assert.AreEqual(2, posts.First().Post.OwnerUserID);
        }

        [TestMethod]
        public void Can_Return_Posts_For_Another_Author_Page_2()
        {
            var httpContext = GetHttpContext(true, 1);
            var authorController = GetAuthorControllerInstance(httpContext);
            authorController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)authorController.PostsByAuthor("karthik", 2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 2);
            Assert.AreEqual(2, posts.First().Post.OwnerUserID);
        }


        private static AuthorController GetAuthorControllerInstance(HttpContextBase httpContext)
        {
            var postRepository = MockObjectFactory.CreatePostRepository();
            var settingsRepository = MockObjectFactory.CreateSettingsRepository();
            var cacheService = MockObjectFactory.CreateCacheService();
            var userRepository = MockObjectFactory.CreateUserRepository();

            var authorController = new AuthorController(postRepository, userRepository, cacheService, settingsRepository)
                {
                    Url = new UrlHelper(httpContext.Request.RequestContext, RouteTable.Routes)
                };
            return authorController;
        }

        private static HttpContextBase GetHttpContext(bool isAuthenticated, int userId)
        {
            var mockContext = MockFactory.GetMockContext(userId, isAuthenticated);
            return mockContext;
        }
    }
}
