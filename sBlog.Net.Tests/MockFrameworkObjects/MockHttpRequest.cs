using System;
using System.Web;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockHttpRequest : HttpRequestBase
    {
        private bool _isAuthenticated = false;

        public override Uri Url
        {
            get
            {
                return new Uri("http://localhost/pages/a-test-url-36");
            }
        }

        public override bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
        }

        public void SetAuth(bool isAuthenticated)
        {
            _isAuthenticated = isAuthenticated;
        }
    }
}
