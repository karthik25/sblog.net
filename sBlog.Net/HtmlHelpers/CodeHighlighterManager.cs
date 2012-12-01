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
using System.Text;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.HtmlHelpers
{
    public static class CodeHighlighterManager
    {
        public static string CodeHighlighterBasePath = "~/Content/codeHighlighter";

        private const string ThemeFormat = "shTheme{0}.css";

        private const string ScriptFormat = "shBrush{0}.js";

        public static string GenerateStyles(this HtmlHelper htmlHelper, string syntaxTheme)
        {
            var urlHelper = htmlHelper.GetUrlHelper();
            var builder = new StringBuilder();

            // generate core style
            var coreStyle = GetStyleTag(urlHelper.Content(string.Format("{0}/styles/shCore.css", CodeHighlighterBasePath)));
            builder.AppendLine(coreStyle.ToString());

            // generate the style tag reqired by the theme
            var themeStyle = GetStyleTag(urlHelper.Content(string.Format("{0}/styles/{1}", CodeHighlighterBasePath, string.Format(ThemeFormat, syntaxTheme))));
            builder.AppendLine(themeStyle.ToString());

            return builder.ToString();
        }

        public static string GenerateScripts(this HtmlHelper htmlHelper, string syntaxScripts)
        {
            var urlHelper = htmlHelper.GetUrlHelper();
            var builder = new StringBuilder();
            var brushes = syntaxScripts != null ? syntaxScripts.Split('~') : new string[] { };

            // generate required scripts
            var coreScript = GetScriptTag(urlHelper.Content(string.Format("{0}/scripts/shCore.js", CodeHighlighterBasePath)));
            builder.AppendLine(coreScript.ToString());

            // genereate optional scripts by choice
            foreach (var brush in brushes)
            {
                var optionalScript = GetScriptTag(urlHelper.Content(string.Format("{0}/scripts/{1}", CodeHighlighterBasePath, string.Format(ScriptFormat, brush))));
                builder.AppendLine(optionalScript.ToString());
            }

            return builder.ToString();
        }

        private static TagBuilder GetStyleTag(string path)
        {
            var styleTag = new TagBuilder("link");
            styleTag.MergeAttribute("href", path);
            styleTag.MergeAttribute("rel", "stylesheet");
            styleTag.MergeAttribute("type", "text/css");

            return styleTag;
        }

        private static TagBuilder GetScriptTag(string url)
        {
            var scriptTag = new TagBuilder("script");
            scriptTag.MergeAttribute("src", url);
            scriptTag.MergeAttribute("type", "text/javascript");

            return scriptTag;
        }
    }
}