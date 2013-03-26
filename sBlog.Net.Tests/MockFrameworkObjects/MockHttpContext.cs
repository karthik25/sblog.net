using System.Web;
using sBlog.Net.Tests.MockObjects;
using System.Security.Principal;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockHttpContext : HttpContextBase
    {
        public MockHttpRequest HttpRequest;
        public MockHttpResponse HttpResponse;

        private readonly int _userId = 1;
        private readonly bool _isAuthenticated;
        private readonly string _url;

        public MockHttpContext(int userId, bool isAuthenticated, string url = null)
        {
            _userId = userId;
            _isAuthenticated = isAuthenticated;
            _url = url;
        }

        public override HttpRequestBase Request
        {
            get
            {
                HttpRequest = new MockHttpRequest(this, _isAuthenticated, _url);
                return HttpRequest;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                HttpResponse = new MockHttpResponse();
                return HttpResponse;
            }
        }

        public override IPrincipal User
        {
            get
            {
                var identity = new MockUserIdentity(null, _userId);
                IPrincipal principal = new GenericPrincipal(identity, null);
                return principal;
            }
            set
            {
                
            }
        }

        public override bool IsDebuggingEnabled
        {
            get
            {
                return true;
            }
        }
    }    
}
