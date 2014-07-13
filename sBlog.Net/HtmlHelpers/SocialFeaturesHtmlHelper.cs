using System.Configuration;
using System.Text;
using System.Web.Mvc;
using sBlog.Net.Enumerations;
using sBlog.Net.Infrastructure;

namespace sBlog.Net.HtmlHelpers
{
    public static class SocialFeaturesHtmlHelper
    {
        public static bool IsSocialFeaturesEnabled(this HtmlHelper htmlHelper)
        {
            return BlogStaticConfig.SocialFeatures != null && BlogStaticConfig.SocialFeatures.Enabled;
        }

        public static MvcHtmlString RenderSocialImages(this HtmlHelper htmlHelper)
        {
            var features = SocialFeatureList.CreateFrom(BlogStaticConfig.SocialFeatures ?? new SocialFeaturesElement());
            var builder = new StringBuilder();

            features.ForEach(feature =>
                {
                    var link = GetSocialLink(htmlHelper, feature);
                    builder.AppendLine(link.ToHtmlString());
                });

            return MvcHtmlString.Create(builder.ToString());
        }

        private static MvcHtmlString GetSocialLink(this HtmlHelper htmlHelper, SocialFeature feature)
        {
            var anchor = new TagBuilder("a");

            anchor.MergeAttribute("class", "social-link");
            anchor.MergeAttribute("href", BuildUrl(feature));
            anchor.InnerHtml = GetSocialImage(htmlHelper, feature);

            return MvcHtmlString.Create(anchor.ToString());
        }

        private static string BuildUrl(SocialFeature feature)
        {
            const string format = "http://www.{0}.com/{1}";
            return string.Format(format, feature.FeatureImagePart, feature.FeatureValue);
        }

        private static string GetSocialImage(this HtmlHelper htmlHelper, SocialFeature feature)
        {
            var img = new TagBuilder("img");

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            img.MergeAttribute("class", "pop rollover");
            img.MergeAttribute("src", urlHelper.Content(string.Format("~/Content/Images/social_icons/{0}.png", feature.FeatureImagePart)));
            img.MergeAttribute("data-rollover", urlHelper.Content(string.Format("~/Content/Images/social_icons/{0}-hover.png", feature.FeatureImagePart)));
            img.MergeAttribute("alt", feature.FeatureImagePart);

            return MvcHtmlString.Create(img.ToString()).ToHtmlString();
        }

        private static readonly SblogNetSettingsConfiguration BlogStaticConfig = ConfigurationManager.GetSection("sblognetSettings") 
                                                                                as SblogNetSettingsConfiguration;
    }
}