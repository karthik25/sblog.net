using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using sBlog.Net.Configuration;
using sBlog.Net.Enumerations;

namespace sBlog.Net.Infrastructure
{
    public static class SocialFeatureList
    {
        public static List<SocialFeature> CreateFrom(SocialFeaturesElement socialFeatures)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            var properties = typeof(SocialFeaturesElement).GetProperties(bindingFlags).Where(p => p.Name != "Enabled").ToList();
            var featureList = properties.Select(p => new SocialFeature
                {
                    FeatureName = p.Name,
                    FeatureValue = (string)p.GetValue(socialFeatures, null),
                    FeatureImagePart = GetImageValue(p.Name)
                });
            return featureList.Where(p => !string.IsNullOrEmpty(p.FeatureValue)).ToList();
        }

        private static string GetImageValue(string fullName)
        {
            var regex = new Regex(@"(\S+)(Id)");
            var matches = regex.Match(fullName);
            return matches.Groups[1].Value.ToLower();
        }
    }
}
