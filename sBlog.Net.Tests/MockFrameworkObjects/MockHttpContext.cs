using System.Web;
using sBlog.Net.Tests.MockObjects;
using System.Security.Principal;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockHttpContext : HttpContextBase
    {
        public MockHttpRequest httpRequest;

        private int _userID = 1;
        private bool _isAuthenticated;

        public override HttpRequestBase Request
        {
            get
            {
                httpRequest = new MockHttpRequest();
                httpRequest.SetAuth(_isAuthenticated);
                return httpRequest;
            }
        }

        public override IPrincipal User
        {
            get
            {
                var identity = new MockUserIdentity(null);
                identity.SetUserID(_userID);
                IPrincipal principal = new GenericPrincipal(identity, null);
                return principal;
            }
            set
            {
                
            }
        }

        public void SetUserID(int userID)
        {
            _userID = userID;
        }

        public void SetAuth(bool auth)
        {
            _isAuthenticated = auth;
        }
    }
}
