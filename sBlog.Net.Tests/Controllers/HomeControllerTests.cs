using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
        [TestInitialize]
        public void Setup()
        {
            HttpContext.Current = MockHelperFactory.FakeHttpContext();

            var item = RouteTable.Routes.Cast<Route>().SingleOrDefault(r => r.Url == "{year}/{month}/{url}/{status}");

            if (item == null)
            {
                var routeValueDictionary = new RouteValueDictionary
                    {
                        {"controller", "Home"},
                        {"action", "View"},
                        {"status", UrlParameter.Optional}
                    };

                var constraintDictionary = new RouteValueDictionary
                    {
                        {"year", @"\d{4}"},
                        {"month", @"[0-9]{1,2}"},
                        {"url", @"\S+"},
                        {"status", @"[a-z\-]*"}
                    };

                RouteTable.Routes.Add("IndividualPost", new Route("{year}/{month}/{url}/{status}", routeValueDictionary,
                                                                  constraintDictionary, null));
            }
        }

        [TestMethod]
        public void Can_Return_First_Page_With_5_Items()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.Index(null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 7", posts.Single(p => p.Post.PostID == 7).Post.PostTitle);
        }

        [TestMethod]
        public void Can_Return_Second_Page_With_5_Items()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.Index(2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 20", posts.Single(p => p.Post.PostID == 20).Post.PostTitle);
        }

        [TestMethod]
        public void Can_Return_First_Page_With_5_Items_Authenticated()
        {
            var httpContext = GetHttpContext(true, 1);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.Index(null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 14", posts.Single(p => p.Post.PostID == 14).Post.PostTitle);
        }

        [TestMethod]
        public void Can_Return_Second_Page_With_5_Items_Authenticated()
        {
            var httpContext = GetHttpContext(true, 1);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.Index(2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 7", posts.Single(p => p.Post.PostID == 7).Post.PostTitle);
        }

        [TestMethod]
        public void Can_Return_Third_Page_With_5_Items_Authenticated()
        {
            var httpContext = GetHttpContext(true, 1);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.Index(3);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 21", posts.Single(p => p.Post.PostID == 21).Post.PostTitle);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Year_Month()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByYearAndMonth("2012", "04", null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 7", posts.Single(p => p.Post.PostID == 7).Post.PostTitle);
            Assert.AreEqual(4, posts.Single(p => p.Post.PostID == 7).Post.PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Year_Month_Page_Two()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByYearAndMonth("2012", "04", 2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 2);
            Assert.AreEqual("Post Title 1", posts.Single(p => p.Post.PostID == 1).Post.PostTitle);
            Assert.AreEqual(4, posts.Single(p => p.Post.PostID == 1).Post.PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Year_Month_Authenticated()
        {
            var httpContext = GetHttpContext(true, 2);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByYearAndMonth("2012", "01", null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 28", posts.Single(p => p.Post.PostID == 28).Post.PostTitle);
            Assert.AreEqual(1, posts.Single(p => p.Post.PostID == 28).Post.PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Year_Month_Page_Two_Authenticated()
        {
            var httpContext = GetHttpContext(true, 2);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByYearAndMonth("2012", "01", 2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 21", posts.Single(p => p.Post.PostID == 21).Post.PostTitle);
            Assert.AreEqual(1, posts.Single(p => p.Post.PostID == 21).Post.PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_Tag_Name_Page_1()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByTag("csharp", null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 7", posts.First().Post.PostTitle);
            Assert.IsTrue(posts.Single(p => p.Post.PostID == 7).Post.Tags.Any(t => t.TagName == "CSharp"));
            Assert.IsTrue(posts.Single(p => p.Post.PostID == 7).Post.Tags.Any(t => t.TagSlug == "csharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Tag_Name_Page_2()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByTag("csharp", 2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 2", posts.First().Post.PostTitle);
            Assert.IsTrue(posts.Single(p => p.Post.PostID == 19).Post.Tags.Any(t => t.TagName == "CSharp"));
            Assert.IsTrue(posts.Single(p => p.Post.PostID == 19).Post.Tags.Any(t => t.TagSlug == "csharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Tag_Name_Page_1_Authenticated()
        {
            var httpContext = GetHttpContext(true, 2);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByTag("ruby", null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 28", posts.First().Post.PostTitle);
            Assert.IsTrue(posts.Single(p => p.Post.PostID == 28).Post.Tags.Any(t => t.TagName == "Ruby"));
            Assert.IsTrue(posts.Single(p => p.Post.PostID == 28).Post.Tags.Any(t => t.TagSlug == "ruby"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Tag_Name_Page_2_Authenticated()
        {
            var httpContext = GetHttpContext(true, 2);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByTag("ruby", 2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 2);
            Assert.AreEqual("[Private] Post Title 23", posts.First().Post.PostTitle);
            Assert.IsTrue(posts.Single(p => p.Post.PostID == 23).Post.Tags.Any(t => t.TagName == "Ruby"));
            Assert.IsTrue(posts.Single(p => p.Post.PostID == 23).Post.Tags.Any(t => t.TagSlug == "ruby"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_1()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByCategory("CSharp", null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 6", posts.Single(p => p.Post.PostID == 6).Post.PostTitle);
            Assert.IsTrue(posts.First().Post.Categories.Any(c => c.CategoryName == "CSharp"));
            Assert.IsTrue(posts.First().Post.Categories.Any(c => c.CategorySlug == "csharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_2()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByCategory("CSharp", 2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 1);
            Assert.AreEqual("Post Title 16", posts.Single(p => p.Post.PostID == 16).Post.PostTitle);
            Assert.IsTrue(posts.First().Post.Categories.Any(c => c.CategoryName == "CSharp"));
            Assert.IsTrue(posts.First().Post.Categories.Any(c => c.CategorySlug == "csharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_1_Authenticated()
        {
            var httpContext = GetHttpContext(true, 2);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByCategory("CSharp", null);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("Post Title 6", posts.Single(p => p.Post.PostID == 6).Post.PostTitle);
            Assert.IsTrue(posts.First().Post.Categories.Any(c => c.CategoryName == "CSharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_2_Authenticated()
        {
            var httpContext = GetHttpContext(true, 2);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByCategory("CSharp", 2);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 5);
            Assert.AreEqual("[Private] Post Title 24", posts.Single(p => p.Post.PostID == 24).Post.PostTitle);
            Assert.IsTrue(posts.First().Post.Categories.Any(c => c.CategoryName == "CSharp"));
        }

        [TestMethod]
        public void Can_Return_Posts_By_Category_Name_Page_3_Authenticated()
        {
            var httpContext = GetHttpContext(true, 2);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.PostsByCategory("CSharp", 3);
            var model = result.ViewData.Model as BlogPostPageViewModel;
            Assert.IsNotNull(model);
            var posts = model.Posts;
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count == 0);
        }

        [TestMethod]
        public void Can_Return_Posts_By_URL()
        {
            var httpContext = GetHttpContext(false, 0);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.View("2012", "04", "a-test-url-1", "");
            var model = result.ViewData.Model as ViewPostOrPageModel;
            Assert.IsNotNull(model);
            var post = model.Post;
            Assert.IsNotNull(post);
            Assert.AreEqual("Post Title 1", post.PostTitle);
            Assert.AreEqual(2012, post.PostAddedDate.Year);
            Assert.AreEqual(4, post.PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_URL_Private_Owner()
        {
            var httpContext = GetHttpContext(true, 1);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.View("2012", "04", "a-test-url-14", "");
            var model = result.ViewData.Model as ViewPostOrPageModel;
            Assert.IsNotNull(model);
            var post = model.Post;
            Assert.IsNotNull(post);
            Assert.AreEqual("[Private] Post Title 14", post.PostTitle);
            Assert.AreEqual(2012, post.PostAddedDate.Year);
            Assert.AreEqual(4, post.PostAddedDate.Month);
        }

        [TestMethod]
        public void Can_Return_Posts_By_URL_Private_Admin()
        {
            var httpContext = GetHttpContext(true, 1);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.View("2012", "04", "a-test-url-11", "");
            var model = result.ViewData.Model as ViewPostOrPageModel;
            Assert.IsNotNull(model);
            var post = model.Post;
            Assert.IsNotNull(post);
            Assert.AreEqual("[Private] Post Title 11", post.PostTitle);
            Assert.AreEqual(2012, post.PostAddedDate.Year);
            Assert.AreEqual(4, post.PostAddedDate.Month);
        }

        [TestMethod]
        [ExpectedException(typeof(UrlNotFoundException), "Unable to find a post w/ the url a-test-url-25 for the month 01 and year 2012")]
        public void Can_Throw_Appropriate_Exception_When_Accessing_Private_Post()
        {
            var httpContext = GetHttpContext(true, 1);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.View("2012", "01", "a-test-url-25", "");
            var model = result.ViewData.Model as ViewPostOrPageModel;
            Assert.IsNotNull(model);
            var post = model.Post;
        }

        [TestMethod]
        [ExpectedException(typeof(UrlNotFoundException), "Unable to find a post w/ the url a-test-url-10 for the month 04 and year 2012")]
        public void Can_Return_Posts_By_URL_Private_Non_Admin()
        {
            var httpContext = GetHttpContext(true, 2);
            var postController = GetHomeControllerInstance(httpContext);
            postController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)postController.View("2012", "04", "a-test-url-10", "");
            var model = result.ViewData.Model as ViewPostOrPageModel;
            Assert.IsNotNull(model);
            var post = model.Post;
        }

        private static HomeController GetHomeControllerInstance(HttpContextBase httpContext)
        {            
            var postRepository = MockObjectFactory.CreatePostRepository();
            var userRepository = MockObjectFactory.CreateUserRepository();
            var categoryRepository = MockObjectFactory.CreateCategoryRepository();
            var tagRepository = MockObjectFactory.CreateTagRepository();
            var settingsRepository = MockObjectFactory.CreateSettingsRepository();
            var cacheService = MockObjectFactory.CreateCacheService();
            var postController = new HomeController(postRepository, userRepository, categoryRepository, tagRepository, settingsRepository, cacheService)
                                 {
                                    Url = new UrlHelper(httpContext.Request.RequestContext, RouteTable.Routes)
                                 };

            return postController;
        }

        private static MockHttpContext GetHttpContext(bool isAuthenticated, int userId)
        {
            var mockContext = new MockHttpContext(userId, isAuthenticated);
            return mockContext;
        }
    }
}
