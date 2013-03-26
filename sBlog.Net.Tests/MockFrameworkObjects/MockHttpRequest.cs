using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockHttpRequest : HttpRequestBase
    {
        private readonly HttpContextBase _httpContextBase;
        private readonly bool _isAuthenticated;
        private readonly string _requestUrl;

        public MockHttpRequest(HttpContextBase httpContextBase, bool isAuthenticated, string requestUrl = null)
        {
            _httpContextBase = httpContextBase;
            _isAuthenticated = isAuthenticated;
            _requestUrl = requestUrl ?? "http://localhost/pages/a-test-url-36";
        }

        public override Uri Url
        {
            get
            {
                return new Uri(_requestUrl);
            }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get
            {
                return _requestUrl;
            }
        }

        public override bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
        }

        public override RequestContext RequestContext
        {
            get
            {
                return new MockRequestContext(_httpContextBase, new RouteData());
            }
        }

        public override string ApplicationPath
        {
            get { return @"/"; }
        }

        public override string PathInfo
        {
            get
            {
                return string.Empty;
            }
        }

        public override NameValueCollection ServerVariables
        {
            get
            {
                var nameValueCollection = new NameValueCollection();
                return nameValueCollection;
            }
        }
    }
}
