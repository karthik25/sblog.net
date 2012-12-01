using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Controllers;
using sBlog.Net.CustomExceptions;
using sBlog.Net.Tests.Helpers;
using sBlog.Net.Models;
using sBlog.Net.Tests.MockFrameworkObjects;

namespace sBlog.Net.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Can_Return_First_Page_With_5_Items()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(false, 1) };
            var result = (ViewResult)postController.Index(null);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 7", posts.Single(p => p.PostID == 7).PostTitle);
        }

        [TestMethod]
        public void Can_Return_Second_Page_With_5_Items()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = new MockHttpContext() };
            var result = (ViewResult)postController.Index(2);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 20", posts.Single(p => p.PostID == 20).PostTitle);
        }

        [TestMethod]
        public void Can_Return_First_Page_With_5_Items_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 1) };
            var result = (ViewResult)postController.Index(null);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 14", posts.Single(p => p.PostID == 14).PostTitle);
        }

        [TestMethod]
        public void Can_Return_Second_Page_With_5_Items_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 1) };
            var result = (ViewResult)postController.Index(2);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 7", posts.Single(p => p.PostID == 7).PostTitle);
        }

        [TestMethod]
        public void Can_Return_Third_Page_With_5_Items_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 1) };
            var result = (ViewResult)postController.Index(3);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 21", posts.Single(p => p.PostID == 21).PostTitle);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Year_Month()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(false, 1) };
            var result = (ViewResult)postController.PostsByYearAndMonth("2012", "04", null);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 7", posts.Single(p => p.PostID == 7).PostTitle);
            Assert.AreEqual(4, posts.Single(p => p.PostID == 7).PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Year_Month_Page_Two()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(false, 1) };
            var result = (ViewResult)postController.PostsByYearAndMonth("2012", "04", 2);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 2);
            Assert.AreEqual("Post Title 1", posts.Single(p => p.PostID == 1).PostTitle);
            Assert.AreEqual(4, posts.Single(p => p.PostID == 1).PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Year_Month_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 2) };
            var result = (ViewResult)postController.PostsByYearAndMonth("2012", "01", null);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 28", posts.Single(p => p.PostID == 28).PostTitle);
            Assert.AreEqual(1, posts.Single(p => p.PostID == 28).PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Year_Month_Page_Two_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 2) };
            var result = (ViewResult)postController.PostsByYearAndMonth("2012", "01", 2);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 21", posts.Single(p => p.PostID == 21).PostTitle);
            Assert.AreEqual(1, posts.Single(p => p.PostID == 21).PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Tag_Name_Page_1()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(false, 1) };
            var result = (ViewResult)postController.PostsByTag("csharp", null);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 7", posts.First().PostTitle);
            Assert.IsTrue(posts.Single(p => p.PostID == 7).Tags.Any(t => t.TagName == "CSharp"));
            Assert.IsTrue(posts.Single(p => p.PostID == 7).Tags.Any(t => t.TagSlug == "csharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Tag_Name_Page_2()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(false, 1) };
            var result = (ViewResult)postController.PostsByTag("csharp", 2);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 2", posts.First().PostTitle);
            Assert.IsTrue(posts.Single(p => p.PostID == 19).Tags.Any(t => t.TagName == "CSharp"));
            Assert.IsTrue(posts.Single(p => p.PostID == 19).Tags.Any(t => t.TagSlug == "csharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Tag_Name_Page_1_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 2) };
            var result = (ViewResult)postController.PostsByTag("ruby", null);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 28", posts.First().PostTitle);
            Assert.IsTrue(posts.Single(p => p.PostID == 28).Tags.Any(t => t.TagName == "Ruby"));
            Assert.IsTrue(posts.Single(p => p.PostID == 28).Tags.Any(t => t.TagSlug == "ruby"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Tag_Name_Page_2_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 2) };
            var result = (ViewResult)postController.PostsByTag("ruby", 2);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 2);
            Assert.AreEqual("[Private] Post Title 23", posts.First().PostTitle);
            Assert.IsTrue(posts.Single(p => p.PostID == 23).Tags.Any(t => t.TagName == "Ruby"));
            Assert.IsTrue(posts.Single(p => p.PostID == 23).Tags.Any(t => t.TagSlug == "ruby"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_1()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(false, 1) };
            var result = (ViewResult)postController.PostsByCategory("CSharp", null);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 6", posts.Single(p => p.PostID == 6).PostTitle);
            Assert.IsTrue(posts.First().Categories.Any(c => c.CategoryName == "CSharp"));
            Assert.IsTrue(posts.First().Categories.Any(c => c.CategorySlug == "csharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_2()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(false, 1) };
            var result = (ViewResult)postController.PostsByCategory("CSharp", 2);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 1);
            Assert.AreEqual("Post Title 16", posts.Single(p => p.PostID == 16).PostTitle);
            Assert.IsTrue(posts.First().Categories.Any(c => c.CategoryName == "CSharp"));
            Assert.IsTrue(posts.First().Categories.Any(c => c.CategorySlug == "csharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_1_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 2) };
            var result = (ViewResult)postController.PostsByCategory("CSharp", null);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 6", posts.Single(p => p.PostID == 6).PostTitle);
            Assert.IsTrue(posts.First().Categories.Any(c => c.CategoryName == "CSharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_2_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 2) };
            var result = (ViewResult)postController.PostsByCategory("CSharp", 2);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 24", posts.Single(p => p.PostID == 24).PostTitle);
            Assert.IsTrue(posts.First().Categories.Any(c => c.CategoryName == "CSharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_3_Authenticated()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 2) };
            var result = (ViewResult)postController.PostsByCategory("CSharp", 3);
            var posts = (result.ViewData.Model as BlogPostPageViewModel).Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 0);
        }

        [TestMethod]
        public void Can_Return_Posts_By_URL()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(false, 1) };
            var result = (ViewResult)postController.View("2012", "04", "a-test-url-1", "");
            var post = (result.ViewData.Model as ViewPostOrPageModel).Post;
            Assert.IsNotNull(post);
            Assert.AreEqual("Post Title 1", post.PostTitle);
            Assert.AreEqual(2012, post.PostAddedDate.Year);
            Assert.AreEqual(4, post.PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_URL_Private_Owner()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 1) };
            var result = (ViewResult)postController.View("2012", "04", "a-test-url-14", "");
            var post = (result.ViewData.Model as ViewPostOrPageModel).Post;
            Assert.IsNotNull(post);
            Assert.AreEqual("[Private] Post Title 14", post.PostTitle);
            Assert.AreEqual(2012, post.PostAddedDate.Year);
            Assert.AreEqual(4, post.PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_URL_Private_Admin()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 1) };
            var result = (ViewResult)postController.View("2012", "04", "a-test-url-11", "");
            var post = (result.ViewData.Model as ViewPostOrPageModel).Post;
            Assert.IsNotNull(post);
            Assert.AreEqual("[Private] Post Title 11", post.PostTitle);
            Assert.AreEqual(2012, post.PostAddedDate.Year);
            Assert.AreEqual(4, post.PostAddedDate.Month);
        }

        [TestMethod]
        [ExpectedException(typeof(UrlNotFoundException), "Unable to find a post w/ the url a-test-url-25 for the month 01 and year 2012")]
        public void Can_Throw_Appropriate_Exception_When_Accessing_Private_Post()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 1) };
            var result = (ViewResult)postController.View("2012", "01", "a-test-url-25", "");
            var post = (result.ViewData.Model as ViewPostOrPageModel).Post;            
        }

        [TestMethod]
        [ExpectedException(typeof(UrlNotFoundException), "Unable to find a post w/ the url a-test-url-10 for the month 04 and year 2012")]
        public void Can_Return_Posts_By_URL_Private_Non_Admin()
        {
            var postController = GetHomeControllerInstance();
            postController.ControllerContext = new ControllerContext { HttpContext = GetHttpContext(true, 2) };
            var result = (ViewResult)postController.View("2012", "04", "a-test-url-10", "");
            var post = (result.ViewData.Model as ViewPostOrPageModel).Post;
        }

        private static HomeController GetHomeControllerInstance()
        {
            var postRepository = MockObjectFactory.CreatePostRepository();
            var userRepository = MockObjectFactory.CreateUserRepository();
            var categoryRepository = MockObjectFactory.CreateCategoryRepository();
            var tagRepository = MockObjectFactory.CreateTagRepository();
            var settingsRepository = MockObjectFactory.CreateSettingsRepository();
            var cacheService = MockObjectFactory.CreateCacheService();
            var postController = new HomeController(postRepository, userRepository, categoryRepository, tagRepository, settingsRepository, cacheService);

            return postController;
        }

        private MockHttpContext GetHttpContext(bool isAuthenticated, int userID)
        {
            var mockContext = new MockHttpContext();
            mockContext.SetAuth(isAuthenticated);
            mockContext.SetUserID(userID);
            return mockContext;
        }
    }
}
