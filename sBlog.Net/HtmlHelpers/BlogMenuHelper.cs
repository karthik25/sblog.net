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
using sBlog.Net.Models;
using System.Text;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.HtmlHelpers
{
    public static class BlogMenuHelper
    {
        /// <summary>
        /// Creates a page link that will be displayed on the blog
        /// </summary>
        /// <param name="helper">The Html helper.</param>
        /// <param name="menu">This holds link's text, url and whether it has to be selected</param>
        /// <returns></returns>
        public static MvcHtmlString CreatePageLink(this HtmlHelper helper, BlogMenuOption menu)
        {
            var builder = new StringBuilder();
            var urlHelper = helper.GetUrlHelper();

            var li = new TagBuilder("li");
            if (menu.Selected)
                li.AddCssClass("current_page_item");

            var tag = new TagBuilder("a");
            tag.MergeAttribute("href",
                               menu.Url != "/" ? urlHelper.RouteUrl("Pages", new { pageUrl = menu.Url, status = string.Empty }) : urlHelper.RouteUrl("Page", new { pageNumber = 1 }));
            tag.InnerHtml = menu.Title;
            li.InnerHtml = tag.ToString();
            builder.AppendLine(li.ToString());

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
