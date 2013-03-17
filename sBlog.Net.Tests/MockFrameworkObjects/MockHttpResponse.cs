using System.Web;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockHttpResponse : HttpResponseBase
    {
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return "/";
        }
    }
}
