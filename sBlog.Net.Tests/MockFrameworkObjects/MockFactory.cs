using System;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using Moq;
using sBlog.Net.Tests.MockObjects;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public static class MockFactory
    {
        public static HttpContextBase GetMockContext(int userId, bool isAuthenticated, string url = null)
        {
            var context = new Mock<HttpContextBase>();

            context.SetupGet(c => c.Request).Returns(GetMockRequest(context.Object, isAuthenticated, url));
            context.SetupGet(c => c.Response).Returns(GetMockResponse(null));
            context.SetupGet(c => c.IsDebuggingEnabled).Returns(true);

            var identity = new MockUserIdentity(null, userId);
            IPrincipal principal = new GenericPrincipal(identity, null);

            context.SetupGet(c => c.User).Returns(principal);

            return context.Object;
        }

        public static HttpRequestBase GetMockRequest(HttpContextBase httpContextBase, bool isAuthenticated,
                                                       string requestUrl = null)
        {
            var reqUrl = requestUrl != null ? requestUrl.Replace("~", "http://localhost") : "http://localhost/pages/a-test-url-36";
            var uri = new Uri(reqUrl);
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(p => p.Url).Returns(uri);
            request.SetupGet(p => p.AppRelativeCurrentExecutionFilePath).Returns(requestUrl);
            request.SetupGet(p => p.IsAuthenticated).Returns(isAuthenticated);
            request.SetupGet(p => p.RequestContext).Returns(httpContextBase.GetMockRequestContext());
            request.SetupGet(p => p.ApplicationPath).Returns(@"/");
            request.SetupGet(p => p.PathInfo).Returns(string.Empty);
            request.SetupGet(p => p.ServerVariables).Returns(new NameValueCollection());
            return request.Object;
        }

        public static RequestContext GetMockRequestContext(this HttpContextBase httpContextBase)
        {
            var requestContext = new Mock<RequestContext>();
            requestContext.SetupProperty(r => r.HttpContext, httpContextBase);
            requestContext.SetupProperty(r => r.RouteData, new RouteData());
            return requestContext.Object;
        }

        public static HttpResponseBase GetMockResponse(string virtualPath)
        {
            var response = new Mock<HttpResponseBase>();
            response.Setup(x => x.ApplyAppPathModifier(virtualPath)).Returns(virtualPath);
            return response.Object;
        }
    }
}