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
using sBlog.Net.Mappers;
using System.IO;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.HtmlHelpers
{
    public static class ThemeRoller
    {
        private const string ThemeCssBasePath = "~/Themes/{0}/css";
        private const string ThemeScriptBasePath = "~/Themes/{0}/js";

        public static MvcHtmlString LoadStylesAndScripts(this HtmlHelper helper, string themeName)
        {
            var urlHelper = helper.GetUrlHelper();
            var builder = new StringBuilder();

            var pathMapper = new PathMapper();
            var basePath = pathMapper.MapPath(string.Format(ThemeCssBasePath, themeName));

            var cssFiles = Directory.GetFiles(basePath, "*.css").ToList();

            cssFiles.ForEach(file =>
            {
                var themeStyle = new TagBuilder("link");
                themeStyle.MergeAttribute("href", urlHelper.Content(string.Format("{0}/{1}", string.Format(ThemeCssBasePath, themeName), Path.GetFileName(file))));
                themeStyle.MergeAttribute("rel", "stylesheet");
                themeStyle.MergeAttribute("type", "text/css");

                builder.AppendLine(themeStyle.ToString());
            });

            basePath = pathMapper.MapPath(string.Format(ThemeScriptBasePath, themeName));

            if (Directory.Exists(basePath))
            {
                var scriptFiles = Directory.GetFiles(basePath, "*.js").ToList();

                scriptFiles.ForEach(file =>
                {
                    var themeScript = new TagBuilder("script");
                    themeScript.MergeAttribute("src", urlHelper.Content(string.Format("{0}/{1}", string.Format(ThemeScriptBasePath, themeName), Path.GetFileName(file))));
                    themeScript.MergeAttribute("type", "text/javascript");

                    builder.AppendLine(themeScript.ToString());
                });
            }

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
