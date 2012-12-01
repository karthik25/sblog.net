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
using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using sBlog.Net.Models;

namespace sBlog.Net.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInformation pagingInfo,
                                              Func<int, string> pageUrl)
        {
            var sb = new StringBuilder();
            for (var i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                sb.AppendLine(tag.ToString());
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
