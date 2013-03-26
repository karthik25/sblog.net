using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Tests.MockFrameworkObjects;

namespace sBlog.Net.Tests.Routes
{
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void Can_Identify_Logon_Url()
        {
            var context = new MockHttpContext(0, false, "~/logon");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Account", routeData.Values["controller"]);
            Assert.AreEqual("LogOn", routeData.Values["action"]);
        }

        [TestMethod]
        public void Can_Identify_Logoff_Url()
        {
            var context = new MockHttpContext(0, false, "~/logoff");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Account", routeData.Values["controller"]);
            Assert.AreEqual("LogOff", routeData.Values["action"]);
        }

        [TestMethod]
        public void Can_Identify_Credits_Url()
        {
            var context = new MockHttpContext(0, false, "~/credits");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("ViewPage", routeData.Values["controller"]);
            Assert.AreEqual("Credits", routeData.Values["action"]);
        }

        [TestMethod]
        public void Can_Identify_Authors_Url_Without_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/authors");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Author", routeData.Values["controller"]);
            Assert.AreEqual("AuthorListing", routeData.Values["action"]);
        }

        [TestMethod]
        public void Can_Identify_Authors_Url_With_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/authors/page/2");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Author", routeData.Values["controller"]);
            Assert.AreEqual("AuthorListing", routeData.Values["action"]);
            Assert.AreEqual("2", routeData.Values["pageNumber"]);
        }

        [TestMethod]
        public void Can_Identify_Author_Posts_Url_Without_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/authors/admin");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Author", routeData.Values["controller"]);
            Assert.AreEqual("PostsByAuthor", routeData.Values["action"]);
            Assert.AreEqual("admin", routeData.Values["authorName"]);
        }

        [TestMethod]
        public void Can_Identify_Author_Posts_Url_With_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/authors/admin/page/2");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Author", routeData.Values["controller"]);
            Assert.AreEqual("PostsByAuthor", routeData.Values["action"]);
            Assert.AreEqual("admin", routeData.Values["authorName"]);
            Assert.AreEqual("2", routeData.Values["pageNumber"]);
        }

        [TestMethod]
        public void Can_Identify_Pages_Url()
        {
            var context = new MockHttpContext(0, false, "~/pages/some-page");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("ViewPage", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual("some-page", routeData.Values["pageUrl"]);
        }

        [TestMethod]
        public void Can_Identify_Pages_Url_With_Status()
        {
            var context = new MockHttpContext(0, false, "~/pages/some-page/comment-posted");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("ViewPage", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual("some-page", routeData.Values["pageUrl"]);
            Assert.AreEqual("comment-posted", routeData.Values["status"]);
        }

        [TestMethod]
        public void Can_Identify_Page_Url()
        {
            var context = new MockHttpContext(0, false, "~/page/1");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["pageNumber"]);
        }

        [TestMethod]
        public void Can_Identify_Category_Url_Without_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/category/asp-net");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("PostsByCategory", routeData.Values["action"]);
            Assert.AreEqual("asp-net", routeData.Values["categoryName"]);
        }

        [TestMethod]
        public void Can_Identify_Category_Url_With_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/category/asp-net/page/2");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("PostsByCategory", routeData.Values["action"]);
            Assert.AreEqual("asp-net", routeData.Values["categoryName"]);
            Assert.AreEqual("2", routeData.Values["pageNumber"]);
        }

        [TestMethod]
        public void Can_Identify_Tag_Url_Without_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/tag/asp-net");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("PostsByTag", routeData.Values["action"]);
            Assert.AreEqual("asp-net", routeData.Values["tagName"]);
        }

        [TestMethod]
        public void Can_Identify_Tag_Url_With_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/tag/asp-net/page/2");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("PostsByTag", routeData.Values["action"]);
            Assert.AreEqual("asp-net", routeData.Values["tagName"]);
            Assert.AreEqual("2", routeData.Values["pageNumber"]);
        }

        [TestMethod]
        public void Can_Identify_Individual_Post()
        {
            var context = new MockHttpContext(0, false, "~/2013/03/some-post");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("View", routeData.Values["action"]);
            Assert.AreEqual("2013", routeData.Values["year"]);
            Assert.AreEqual("03", routeData.Values["month"]);
            Assert.AreEqual("some-post", routeData.Values["url"]);            
        }

        [TestMethod]
        public void Can_Identify_Individual_Post_With_Status()
        {
            var context = new MockHttpContext(0, false, "~/2013/03/some-post/comment-posted");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("View", routeData.Values["action"]);
            Assert.AreEqual("2013", routeData.Values["year"]);
            Assert.AreEqual("03", routeData.Values["month"]);
            Assert.AreEqual("some-post", routeData.Values["url"]);
            Assert.AreEqual("comment-posted", routeData.Values["status"]);
        }

        [TestMethod]
        public void Can_Identify_MonthYear_Url_Without_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/2013/02");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("PostsByYearAndMonth", routeData.Values["action"]);
            Assert.AreEqual("2013", routeData.Values["year"]);
            Assert.AreEqual("02", routeData.Values["month"]);
        }

        [TestMethod]
        public void Can_Identify_MonthYear_Url_With_Page_Number()
        {
            var context = new MockHttpContext(0, false, "~/2013/02/page/2");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("PostsByYearAndMonth", routeData.Values["action"]);
            Assert.AreEqual("2013", routeData.Values["year"]);
            Assert.AreEqual("02", routeData.Values["month"]);
            Assert.AreEqual("2", routeData.Values["pageNumber"]);
        }

        [TestMethod]
        public void Can_Identify_404_Url()
        {
            var context = new MockHttpContext(0, false, "~/404");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Errors", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestMethod]
        public void Can_Identify_Under_Construction_Url()
        {
            var context = new MockHttpContext(0, false, "~/under-construction");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Maintenance", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestMethod]
        public void Can_Identify_Invalid_Theme_Url()
        {
            var context = new MockHttpContext(0, false, "~/invalid-theme");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Maintenance", routeData.Values["controller"]);
            Assert.AreEqual("InvalidTheme", routeData.Values["action"]);
        }

        [TestMethod]
        public void Can_Identify_Default_Url()
        {
            var context = new MockHttpContext(0, false, "~/home/index");
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var routeData = routes.GetRouteData(context);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("home", routeData.Values["controller"]);
            Assert.AreEqual("index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }
    }
}
