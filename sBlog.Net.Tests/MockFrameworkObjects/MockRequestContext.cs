using System.Web;
using System.Web.Routing;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    public class MockRequestContext : RequestContext
    {
        public MockRequestContext(HttpContextBase httpContextBase, RouteData routeData)
            : base(httpContextBase, routeData)
        {

        }
    }
}
