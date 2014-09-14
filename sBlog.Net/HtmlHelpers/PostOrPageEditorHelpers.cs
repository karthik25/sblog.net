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
using System.Text;
using System.Web.Mvc;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.HtmlHelpers
{
    public static class PostOrPageEditorHelpers
    {
        public static MvcHtmlString GenerateCkEditorScripts(this HtmlHelper htmlHelper)
        {
            const string prefix = "~/Content/CKEditor/";
            var scripts = new List<string> { "ckeditor.js", "adapters/jquery.js", "ckeditor_init.js" };
            var builder = new StringBuilder();
            var urlHelper = htmlHelper.GetUrlHelper();

            scripts.ForEach(script =>
            {
                var scriptTag = new TagBuilder("script");
                scriptTag.MergeAttribute("type", "text/javascript");
                var scriptUrl = urlHelper.Content(string.Format("{0}{1}", prefix, script));
                scriptTag.MergeAttribute("src", scriptUrl);

                builder.AppendLine(scriptTag.ToString());
            });

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
