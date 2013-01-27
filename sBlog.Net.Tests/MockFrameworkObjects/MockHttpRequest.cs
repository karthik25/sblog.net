using System;
using System.Web;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockHttpRequest : HttpRequestBase
    {
        private readonly bool _isAuthenticated;

        public MockHttpRequest(bool isAuthenticated)
        {
            _isAuthenticated = isAuthenticated;
        }

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
    }
}
