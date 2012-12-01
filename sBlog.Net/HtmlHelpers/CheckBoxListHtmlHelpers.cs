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
using System.Text;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using sBlog.Net.Models;

namespace sBlog.Net.HtmlHelpers
{
    public static class CheckBoxListHtmlHelpers
    {
        private const string ChkId = "chkBox_{0}_{1}";
        private const string LblId = "lblLabel_{0}_{1}";
        private const string ValID = "valValue_{0}_{1}";

        public static MvcHtmlString CheckBoxInput(this HtmlHelper html, string headerText, CheckBoxListItem item, int index)
        {
            var chkBoxID = string.Format(ChkId, Regex.Replace(headerText, @"\s+", ""), index);
            var hiddenFieldID = string.Format("hdnChk_{0}_{1}", Regex.Replace(headerText, @"\s+", ""), index);

            var builder = new StringBuilder();
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "checkbox");
            tagBuilder.MergeAttribute("id", chkBoxID);
            tagBuilder.MergeAttribute("name", chkBoxID);
            tagBuilder.MergeAttribute("class", "chkClickable");
            tagBuilder.MergeAttribute("value", item.IsChecked.ToString().ToLower());
            if (item.IsChecked)
                tagBuilder.MergeAttribute("checked", "checked");
            builder.AppendLine(tagBuilder.ToString());

            var hdnTagBuilder1 = CreateHiddenTag(hiddenFieldID, item.IsChecked.ToString().ToLower(), "hdnStatus");
            builder.AppendLine(hdnTagBuilder1.ToString());

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString CheckBoxInputLabel(this HtmlHelper html, string headerText, CheckBoxListItem item, int index)
        {
            var lblFor = string.Format(ChkId, Regex.Replace(headerText, @"\s+", ""), index);
            var lblForHdn = string.Format(LblId, Regex.Replace(headerText, @"\s+", ""), index);

            var builder = new StringBuilder();

            var tagBuilder = new TagBuilder("label");
            tagBuilder.MergeAttribute("for", lblFor);
            tagBuilder.SetInnerText(item.Text);
            builder.AppendLine(tagBuilder.ToString());

            var hdnTagBuilder = CreateHiddenTag(lblForHdn, item.Text);
            builder.AppendLine(hdnTagBuilder.ToString());

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString CheckBoxValue(this HtmlHelper html, string headerText, CheckBoxListItem item, int index)
        {
            var valueID = string.Format(ValID, Regex.Replace(headerText, @"\s+", ""), index);

            var builder = new StringBuilder();
            var tagBuilder = CreateHiddenTag(valueID, item.Value);
            builder.AppendLine(tagBuilder.ToString());

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString CheckBoxListHeader(this HtmlHelper html, string headerText)
        {
            var hdrId = string.Format("hdrTitle_{0}", Regex.Replace(headerText, @"\s+", ""));

            var builder = new StringBuilder();

            var hdnHdr = CreateHiddenTag(hdrId, headerText);
            builder.AppendLine(hdnHdr.ToString());

            return MvcHtmlString.Create(builder.ToString());
        }

        private static TagBuilder CreateHiddenTag(string chkBoxID, string chkBoxValue, string cssClass = null)
        {
            var hiddenTag = new TagBuilder("input");
            hiddenTag.MergeAttribute("type", "hidden");
            hiddenTag.MergeAttribute("id", chkBoxID);
            hiddenTag.MergeAttribute("name", chkBoxID);
            
            if (!string.IsNullOrEmpty(cssClass))
                hiddenTag.MergeAttribute("class", cssClass);

            hiddenTag.MergeAttribute("value", chkBoxValue);

            return hiddenTag;
        }
    }
}
