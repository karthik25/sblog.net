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
using System.Web;
using System.Web.Mvc;
using System.Text;
using sBlog.Net.Mappers;
using sBlog.Net.Domain.Interfaces;
using System.IO;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.HtmlHelpers
{
    public static class AdminScriptProviderHelpers
    {
        public static MvcHtmlString GenerateAdminScripts(this HtmlHelper htmlHelper)
        {
            var urlHelper = htmlHelper.GetUrlHelper();
            var builder = IsDebug() ? BuildDebugScripts(urlHelper) : BuildReleaseScripts(urlHelper);
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString GenerateAdminStyles(this HtmlHelper htmlHelper)
        {
            var builder = new StringBuilder();
            var urlHelper = htmlHelper.GetUrlHelper();
            if (IsDebug())
            {
                builder.AppendLine(GetStyleElement(urlHelper, "~/Content/Site.css"));
                builder.AppendLine(GetStyleElement(urlHelper, "~/Content/themes/base/jquery-ui.css"));
                builder.AppendLine(GetStyleElement(urlHelper, "~/Content/themes/base/jquery-ui.custom.css"));
            }
            else
            {
                builder.AppendLine(GetStyleElement(urlHelper, "~/Content/Site.min.css"));
                builder.AppendLine(GetStyleElement(urlHelper, "~/Content/themes/base/jquery-ui.css"));
                builder.AppendLine(GetStyleElement(urlHelper, "~/Content/themes/base/jquery-ui.custom.min.css"));                
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        private static StringBuilder BuildDebugScripts(UrlHelper urlHelper)
        {
            var builder = new StringBuilder();
            IPathMapper mapper = new PathMapper();
            var basePath = mapper.MapPath("~/Scripts/Required/debug");

            var files = Directory.GetFiles(basePath).ToList();

            files.ForEach(file =>
            {
                var scriptTag = new TagBuilder("script");
                scriptTag.MergeAttribute("type", "text/javascript");
                var scriptUrl = urlHelper.Content("~/Scripts/Required/debug/" + Path.GetFileName(file));
                scriptTag.MergeAttribute("src", scriptUrl);

                builder.AppendLine(scriptTag.ToString());
            });
            return builder;
        }

        private static StringBuilder BuildReleaseScripts(UrlHelper urlHelper)
        {
            var builder = new StringBuilder();

            var scriptTag = new TagBuilder("script");
            scriptTag.MergeAttribute("type", "text/javascript");
            var scriptUrl = urlHelper.Content("~/Scripts/Required/minified/script-bundle.min.js");
            scriptTag.MergeAttribute("src", scriptUrl);

            builder.AppendLine(scriptTag.ToString());

            return builder;
        }

        private static string GetStyleElement(UrlHelper urlHelper, string stylePath)
        {
            var styleElement = new TagBuilder("link");
            styleElement.MergeAttribute("type", "text/css");
            styleElement.MergeAttribute("rel", "stylesheet");
            var styleUrl = urlHelper.Content(stylePath);
            styleElement.MergeAttribute("href", styleUrl);
            return styleElement.ToString();
        }

        private static bool IsDebug()
        {
            return HttpContext.Current.IsDebuggingEnabled;
        }
    }
}
