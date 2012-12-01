#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion
using System.Web.Mvc;

namespace sBlog.Net.FluentExtensions
{
    public static class HtmlHelperExtensions
    {
        public static string GetRequiredString(this HtmlHelper htmlHelper, string requiredString)
        {
            return htmlHelper.ViewContext
                             .RouteData
                             .GetRequiredString(requiredString);
        }

        public static bool IsAuthenticated(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext
                             .RequestContext
                             .HttpContext
                             .Request
                             .IsAuthenticated;
        }

        public static UrlHelper GetUrlHelper(this HtmlHelper htmlHelper)
        {
            return new UrlHelper(htmlHelper.ViewContext
                                           .RequestContext);
        }
    }
}
