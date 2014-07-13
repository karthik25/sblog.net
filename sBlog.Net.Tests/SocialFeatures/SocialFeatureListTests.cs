using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Infrastructure;

namespace sBlog.Net.Tests.SocialFeatures
{
    [TestClass]
    public class SocialFeatureListTests
    {
        [TestMethod]
        public void CanIdentifyEnabledSocialFeatures()
        {
            var socialConfig = new SocialFeaturesElement
            {
                Enabled = true,
                TwitterId = "mytwitterid",
                GithubId = "mygithubid",
                FacebookId = "my.facebook",
                PinterestId = ""
            };

            var enabledFeatures = SocialFeatureList.CreateFrom(socialConfig);
            Assert.IsNotNull(enabledFeatures);
            Assert.AreEqual(3, enabledFeatures.Count);

            var twitter = enabledFeatures.SingleOrDefault(e => e.FeatureName == "TwitterId");
            Assert.IsNotNull(twitter);
            Assert.AreEqual("mytwitterid", twitter.FeatureValue);
            Assert.AreEqual("twitter", twitter.FeatureImagePart);

            var github = enabledFeatures.SingleOrDefault(e => e.FeatureName == "GithubId");
            Assert.IsNotNull(github);
            Assert.AreEqual("mygithubid", github.FeatureValue);
            Assert.AreEqual("github", github.FeatureImagePart);

            var facebook = enabledFeatures.SingleOrDefault(e => e.FeatureName == "FacebookId");
            Assert.IsNotNull(facebook);
            Assert.AreEqual("my.facebook", facebook.FeatureValue);
            Assert.AreEqual("facebook", facebook.FeatureImagePart);
        }
    }
}