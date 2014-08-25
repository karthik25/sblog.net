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
                var anchor = new TagBuilder("a");
                anchor.MergeAttribute("href", pageUrl(i));
                anchor.InnerHtml = i.ToString(CultureInfo.InvariantCulture);

                var item = new TagBuilder("li") { InnerHtml = anchor.ToString() };
                if (i == pagingInfo.CurrentPage)
                    item.MergeAttribute("class", "active");

                sb.AppendLine(item.ToString());
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
