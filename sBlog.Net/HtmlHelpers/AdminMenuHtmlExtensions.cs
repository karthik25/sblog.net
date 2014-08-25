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

using System.Linq;
using System.Web.Mvc;
using System.Text;
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

        public static MvcHtmlString AdminLink(this HtmlHelper htmlHelper, string linkText, string routeName, string actionName, string controllerName, string imageUrl, string toolTip = "")
        {
            var builder = new StringBuilder();
            var urlHelper = htmlHelper.GetUrlHelper();

            var liTag = new TagBuilder("li");
            
            var div = new TagBuilder("div");

            var innerBuilder = GetInnerBuilder(urlHelper.Content(imageUrl), linkText);
            div.InnerHtml = innerBuilder.ToString();

            var aTag = GetAnchorTag(urlHelper.RouteUrl(routeName));
            aTag.InnerHtml = div.ToString();

            if (IsSelected(htmlHelper, controllerName, actionName))
                liTag.AddCssClass("current");

            liTag.InnerHtml = aTag.ToString();
            if (!string.IsNullOrEmpty(toolTip))
                liTag.Attributes["title"] = toolTip;

            builder.AppendLine(liTag.ToString());

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString AdminLinkWithSubmenu(this HtmlHelper htmlHelper, string linkText, string routeName, string actionName, string controllerName, params string[] imageUrls)
        {
            var builder = new StringBuilder();

            var drillDownBuilder = new StringBuilder();

            var liTag = new TagBuilder("li");
            if (IsSelected(htmlHelper, controllerName, actionName))
                liTag.AddCssClass("current");

            var ulTag = GetUlTag();
            var urlHelper = htmlHelper.GetUrlHelper();

            var div = new TagBuilder("div");

            var innerBuilder = GetInnerBuilder(urlHelper.Content(imageUrls.First()), linkText);
            div.InnerHtml = innerBuilder.ToString();

            var aTag = GetAnchorTag(urlHelper.RouteUrl(routeName));
            aTag.InnerHtml = div.ToString();

            builder.AppendLine(aTag.ToString());

            var manageString = AdminLink(htmlHelper, string.Format("Manage {0}", linkText), routeName, string.Format("Manage{0}", linkText), controllerName, imageUrls.Skip(1).First());
            var newString = AdminLink(htmlHelper, string.Format("Add a {0}", controllerName), string.Format("Admin{0}Add", linkText), "Add", controllerName, imageUrls.Last());

            drillDownBuilder.AppendLine(manageString.ToHtmlString());
            drillDownBuilder.AppendLine(newString.ToHtmlString());

            ulTag.InnerHtml = drillDownBuilder.ToString();

            builder.AppendLine(ulTag.ToString());

            liTag.InnerHtml = builder.ToString();

            return MvcHtmlString.Create(liTag.ToString());
        }

        private static StringBuilder GetInnerBuilder(string url, string linkText)
        {
            var innerBuilder = new StringBuilder();

            var img = GetImageLink(url);
            innerBuilder.AppendLine(img.ToString());

            var span = new TagBuilder("span") { InnerHtml = linkText };
            innerBuilder.AppendLine(span.ToString());

            return innerBuilder;
        }

        private static TagBuilder GetUlTag()
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("sub-menu");
            ul.AddCssClass("no-display");

            return ul;
        }

        private  static TagBuilder GetImageLink(string url)
        {
            var img = new TagBuilder("img");
            img.MergeAttribute("src", url);
            img.MergeAttribute("class", "adminImage");
            img.MergeAttribute("alt", "");

            return img;
        }

        private static TagBuilder GetAnchorTag(string url)
        {
            var aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", url);
            return aTag;
        }

        private static bool IsSelected(HtmlHelper htmlHelper, string controller, string action)
        {
            var currentAction = htmlHelper.GetRequiredString("action");
            var currentController = htmlHelper.GetRequiredString("controller");

            return currentAction.ToLower() == action.ToLower() && currentController.ToLower() == controller.ToLower();
        }
    }
}
