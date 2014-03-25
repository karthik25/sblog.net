using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.CustomExceptions;
using sBlog.Net.Tests.MockObjects;
using sBlog.Net.Controllers;
using System.Web.Mvc;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Models;
using sBlog.Net.Tests.MockFrameworkObjects;
using sBlog.Net.Tests.Helpers;

namespace sBlog.Net.Tests.Controllers
{
    [TestClass]
    public class ViewPageControllerTests
    {
        [TestInitialize]
        public void Setup()
        {
            HttpContext.Current = MockHelperFactory.FakeHttpContext();

            var item = RouteTable.Routes.Cast<Route>().SingleOrDefault(r => r.Url == "pages/{pageUrl}/{status}");

            if (item == null)
            {
                var routeValueDictionary = new RouteValueDictionary
                    {
                        {"controller", "ViewPage"},
                        {"action", "Index"},
                        {"status", UrlParameter.Optional}
                    };

                var constraintDictionary = new RouteValueDictionary
                    {
                        {"pageUrl", @"\S+"},
                        {"status", @"[a-z\-]*"}
                    };

                RouteTable.Routes.Add("Pages", new Route("pages/{pageUrl}/{status}", routeValueDictionary,
                                                         constraintDictionary, null));
            }
        }

        [TestMethod]
        public void Can_Get_A_Single_Page()
        {
            var httpContext = GetHttpContext(false, 0);
            var pageController = GetViewPageController(httpContext);
            pageController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)pageController.Index("a-test-url-29", "");
            var model = result.ViewData.Model as ViewPostOrPageModel;
            Assert.IsNotNull(model);
            var page = model.Post;
            Assert.IsNotNull(page);
            Assert.AreEqual("Page Title 29", page.PostTitle);
            Assert.AreEqual("Page Content  29", page.PostContent);
        }

        [TestMethod]
        public void Can_Get_A_Single_Page_Private()
        {
            var httpContext = GetHttpContext(true, 1);
            var pageController = GetViewPageController(httpContext);
            pageController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)pageController.Index("a-test-url-36", "");
            var model = result.ViewData.Model as ViewPostOrPageModel;
            Assert.IsNotNull(model);
            var page = model.Post;
            Assert.IsNotNull(page);
            Assert.AreEqual("[Private] Page Title 36", page.PostTitle);
            Assert.AreEqual("Page Content  36", page.PostContent);
        }

        [TestMethod]
        [ExpectedException(typeof(UrlNotFoundException), "Unable to find a page w/ the url a-test-url-36")]
        public void Can_Throw_Exception_For_Illegal_Access()
        {
            var httpContext = GetHttpContext(true, 2);
            var pageController = GetViewPageController(httpContext);
            pageController.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = (ViewResult)pageController.Index("a-test-url-36", "");
            var model = result.ViewData.Model as ViewPostOrPageModel;
            Assert.IsNotNull(model);
            var page = model.Post;
        }

        [TestMethod]
        public void Can_Generate_All_Pages()
        {
            var httpContext = GetHttpContext(false, 0);
            var pageController = GetViewPageController(httpContext);
            pageController.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var result = (PartialViewResult)pageController.Pages();
            var blogMenuModel = result.ViewData.Model as BlogMenuViewModel;
            Assert.IsNotNull(blogMenuModel);
            Assert.AreEqual(5, blogMenuModel.Pages.Count);

            var first = blogMenuModel.Pages.Single(b => b.Title == "Page Title 29");
            Assert.AreEqual("a-test-url-29", first.Url);
            Assert.AreEqual(false, first.Selected);

            var second = blogMenuModel.Pages.Single(b => b.Title == "Page Title 30");
            Assert.AreEqual("a-test-url-30", second.Url);
            Assert.AreEqual(false, second.Selected);
        }

        [TestMethod]
        public void Can_Generate_Other_Pages()
        {
            var httpContext = GetHttpContext(false, 0);
            var pageController = GetViewPageController(httpContext);
            pageController.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var result = (PartialViewResult)pageController.PagesList();
            var posts = result.ViewData.Model as List<PostEntity>;

            Assert.IsNotNull(posts);
            Assert.AreEqual(2, posts.Count);

            var first = posts.Single(p => p.PostTitle == "Page Title 33");
            Assert.AreEqual("a-test-url-33", first.PostUrl);

            var second = posts.Single(p => p.PostTitle == "Page Title 34");
            Assert.AreEqual("a-test-url-34", second.PostUrl);
        }

        [TestMethod]
        public void Can_Generate_Other_Pages_Authenticated()
        {
            var httpContext = GetHttpContext(true, 1);
            var pageController = GetViewPageController(httpContext);
            pageController.ControllerContext = new ControllerContext { HttpContext = httpContext };

            var result = (PartialViewResult)pageController.PagesList();
            var posts = result.ViewData.Model as List<PostEntity>;

            Assert.IsNotNull(posts);
            Assert.AreEqual(8, posts.Count);

            var first = posts.Single(p => p.PostTitle == "Page Title 33");
            Assert.AreEqual("a-test-url-33", first.PostUrl);

            var second = posts.Single(p => p.PostTitle == "Page Title 40");
            Assert.AreEqual("a-test-url-40", second.PostUrl);
        }

        private static HttpContextBase GetHttpContext(bool isAuthenticated, int userId)
        {
            var mockContext = MockFactory.GetMockContext(userId, isAuthenticated);
            return mockContext;
        }

        private static ViewPageController GetViewPageController(HttpContextBase httpContext)
        {
            var post = MockObjectFactory.CreatePostRepository();
            var cacheService = MockObjectFactory.CreateCacheService();
            var settings = MockObjectFactory.CreateSettingsRepository();
            var user = MockObjectFactory.CreateUserRepository();
            var pageController = new ViewPageController(post, user, settings, cacheService)
                                 {
                                     Url = new UrlHelper(httpContext.Request.RequestContext, RouteTable.Routes)
                                 };

            return pageController;
        }
    }
}
