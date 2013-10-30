using System.Web;

namespace sBlog.Net.Extensibility.Concrete
{
    public class PluginContext
    {
        public HttpRequest Request { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string UserToken { get; set; }
    }
}
