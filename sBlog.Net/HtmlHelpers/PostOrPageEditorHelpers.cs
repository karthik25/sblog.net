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
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Mvc;
using sBlog.Net.Configuration;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.HtmlHelpers
{
    public static class PostOrPageEditorHelpers
    {
        public static MvcHtmlString GenerateEditorScriptBySelection(this HtmlHelper htmlHelper)
        {
            return BlogStaticConfig.EditorType == "html"
                       ? htmlHelper.GenerateCkEditorScripts()
                       : htmlHelper.GenerateMarkdownStypesAndScripts();
        }

        public static MvcHtmlString GenerateCkEditorScripts(this HtmlHelper htmlHelper)
        {
            const string prefix = "~/Content/CKEditor/";
            var scripts = new List<string> { "ckeditor.js", "adapters/jquery.js", "ckeditor_init.js" };
            var builder = new StringBuilder();
            var urlHelper = htmlHelper.GetUrlHelper();

            scripts.ForEach(script =>
            {
                var scriptTag = urlHelper.GetScript(prefix, script);
                builder.AppendLine(scriptTag.ToString());
            });

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString GenerateMarkdownStypesAndScripts(this HtmlHelper htmlHelper)
        {
            const string prefix = "~/Scripts/";
            var scripts = new List<string> { "MarkdownDeepLib.min.js", "markdown_init.js" };
            var builder = new StringBuilder();
            var urlHelper = htmlHelper.GetUrlHelper();

            scripts.ForEach(script =>
                {
                    var scriptTag = urlHelper.GetScript(prefix, script);
                    builder.AppendLine(scriptTag.ToString());
                });

            var markdownStyle = new TagBuilder("link");
            markdownStyle.MergeAttribute("rel", "stylesheet");
            markdownStyle.MergeAttribute("type", "text/css");
            var styleUrl = urlHelper.Content("~/Scripts/mdd_styles.css");
            markdownStyle.MergeAttribute("href", styleUrl);

            builder.AppendLine(markdownStyle.ToString());

            return MvcHtmlString.Create(builder.ToString());
        }

        private static string GetScript(this UrlHelper urlHelper, string prefix, string script)
        {
            var scriptTag = new TagBuilder("script");
            scriptTag.MergeAttribute("type", "text/javascript");
            var scriptUrl = urlHelper.Content(string.Format("{0}{1}", prefix, script));
            scriptTag.MergeAttribute("src", scriptUrl);
            return scriptTag.ToString();
        }

        private static readonly SblogNetSettingsConfiguration BlogStaticConfig = ConfigurationManager.GetSection("sblognetSettings")
                                                             as SblogNetSettingsConfiguration;
    }
}
