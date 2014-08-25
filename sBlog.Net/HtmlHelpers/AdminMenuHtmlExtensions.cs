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
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.HtmlHelpers
{
    public static class AdminMenuHtmlExtensions
    {
        public static MvcHtmlString AdminLink(this HtmlHelper htmlHelper, string displayText, string iconName, string routeName)
        {
            var urlHelper = htmlHelper.GetUrlHelper();

            var img = new TagBuilder("img");
            img.MergeAttribute("alt", displayText);
            img.MergeAttribute("src", urlHelper.Content(string.Format("~/Content/Images/{0}.png", iconName)));
            img.MergeAttribute("class", "adminImage");

            var aTag = GetAnchorTag(urlHelper.RouteUrl(routeName));
            aTag.InnerHtml = string.Format("{0} {1}", img, displayText);

            return MvcHtmlString.Create(aTag.ToString());
        }

        public static MvcHtmlString AdminLink(this HtmlHelper htmlHelper, string displayText, string iconName, string actionName, string controllerName)
        {
            var urlHelper = htmlHelper.GetUrlHelper();

            var img = new TagBuilder("img");
            img.MergeAttribute("alt", displayText);
            img.MergeAttribute("src", urlHelper.Content(string.Format("~/Content/Images/{0}.png", iconName)));
            img.MergeAttribute("class", "adminImage");

            var aTag = GetAnchorTag(urlHelper.Action(actionName, controllerName, new { Area = "" }));
            aTag.InnerHtml = string.Format("{0} {1}", img, displayText);

            return MvcHtmlString.Create(aTag.ToString());
        }

        private static TagBuilder GetAnchorTag(string url)
        {
            var aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", url);
            return aTag;
        }
    }
}
