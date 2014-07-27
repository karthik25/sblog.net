using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Configuration;

namespace sBlog.Net.Tests.Infrastructure
{
    [TestClass]
    public class ApplicationConfigurationTests
    {
        [TestMethod]
        public void CanReadAThemeFromWebConfig()
        {
            var themeSettings = BlogStaticConfig.Theme;
            Assert.IsNotNull(themeSettings);
            Assert.AreEqual("PerfectBlemish", themeSettings.SelectedTheme);
        }

        [TestMethod]
        public void CanReadHasherTyeFromWebConfig()
        {
            var hasherType = BlogStaticConfig.HasherFullyQualifiedTypeName;
            Assert.IsNotNull(hasherType);
            Assert.AreEqual("sBlog.Net.Domain.Hashers.Md5Hasher", hasherType);
        }

        private static readonly SblogNetSettingsConfiguration BlogStaticConfig = ConfigurationManager.GetSection("sblognetSettings")
                                                                     as SblogNetSettingsConfiguration;
    }
}
