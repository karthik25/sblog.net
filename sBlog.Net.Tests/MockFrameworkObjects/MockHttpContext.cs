using System.Web;
using sBlog.Net.Tests.MockObjects;
using System.Security.Principal;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockHttpContext : HttpContextBase
    {
        public MockHttpRequest HttpRequest;

        private readonly int _userId = 1;
        private readonly bool _isAuthenticated;

        public MockHttpContext(int userId, bool isAuthenticated)
        {
            _userId = userId;
            _isAuthenticated = isAuthenticated;
        }

        public override HttpRequestBase Request
        {
            get
            {
                HttpRequest = new MockHttpRequest(_isAuthenticated);
                return HttpRequest;
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
    }
}
