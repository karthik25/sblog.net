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
using System.Collections.Generic;
using sBlog.Net.DependencyManagement;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.HtmlHelpers
{
    public static class BlogScriptStyleProvider
    {
        /// <summary>
        /// This html extension injects the following sets of files sequentially
        /// 
        ///     * Common css & js files like SiteCommons, jQuery
        ///     * Code highlighter styles
        ///     * Code highlighter scripts
        ///     * Injected script content
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns>a mvc html string w/ various scripts & styles</returns>
        public static MvcHtmlString GetCommonScriptsAndStyles(this HtmlHelper htmlHelper)
        {
            var urlHelper = htmlHelper.GetUrlHelper();
            var settingsRepository = InstanceFactory.CreateSettingsInstance();
            var sBuilder = new StringBuilder();

            var customFiles = GetCustomFiles(settingsRepository.BlogSyntaxHighlighting);

            GetFiles().ForEach(file =>
            {
                var tag = file.IsScript ? BuildScript(urlHelper, file.FileRelativePath) 
                                               : BuildCss(urlHelper, file.FileRelativePath);
                sBuilder.AppendLine(tag.ToString());
            });

            if (customFiles.Any())
            {
                var cssData = htmlHelper.GenerateStyles(settingsRepository.BlogSyntaxTheme);
                sBuilder.AppendLine(cssData);

                var scriptData = htmlHelper.GenerateScripts(settingsRepository.BlogSyntaxScripts);
                sBuilder.AppendLine(scriptData);

                customFiles.ForEach(file =>
                {
                    var tag = file.IsScript ? BuildScript(urlHelper, file.FileRelativePath)
                                                   : BuildCss(urlHelper, file.FileRelativePath);
                    sBuilder.AppendLine(tag.ToString());
                });
            }

            sBuilder.AppendLine(InjectScript(urlHelper));

            return MvcHtmlString.Create(sBuilder.ToString());
        }

        /// <summary>
        /// The layout pages might require custom script content that might not be part
        /// of the .js file's or properties needed by the layout pages (like the site's root url).
        /// </summary>
        /// <param name="urlHelper">The Url helper.</param>
        /// <returns>The dynamic javascript content</returns>
        private static string InjectScript(UrlHelper urlHelper)
        {
            var baseUrl = urlHelper.Content("~/");
            const string injectScriptFormat = @"<script type=""text/javascript"" language=""javascript"">{0}</script>";
            return string.Format(injectScriptFormat, "var siteRoot='" + baseUrl + "';");
        }

        private static List<GenericFile> GetCustomFiles(bool syntaxHighlighterStatus)
        {
            var customFiles = new List<GenericFile>();

            if (syntaxHighlighterStatus)
                customFiles.Add(new GenericFile { FileRelativePath = "~/Scripts/SyntaxHighlighter.js", IsScript = true });

            return customFiles;
        }

        private static TagBuilder BuildCss(UrlHelper urlHelper, string relativePath)
        {
            var tag = new TagBuilder("link");
            tag.MergeAttribute("rel", "stylesheet");
            tag.MergeAttribute("type", "text/css");
            tag.MergeAttribute("href", urlHelper.Content(relativePath));

            return tag;
        }

        private static TagBuilder BuildScript(UrlHelper urlHelper, string relativePath)
        {
            var tag = new TagBuilder("script");
            tag.MergeAttribute("type", "text/javascript");
            tag.MergeAttribute("src", urlHelper.Content(relativePath));

            return tag;
        }

        private static List<GenericFile> GetFiles()
        {
            var genericFiles = new List<GenericFile>
            {
                new GenericFile { FileRelativePath = "~/Content/SiteCommons.css", IsScript = false },
                new GenericFile { FileRelativePath = "~/Scripts/jquery-1.7.2.min.js", IsScript = true },
                new GenericFile { FileRelativePath = "~/Content/sBlog/jquery.qtip-1.0.0-rc3.min.js", IsScript = true },
                new GenericFile { FileRelativePath = "~/Content/sBlog/Site.js", IsScript = true }
            };

            return genericFiles;
        }

        private class GenericFile
        {
            public string FileRelativePath { get; set; }
            public bool IsScript { get; set; }
        }
    }
}
