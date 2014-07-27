using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Configuration;
using sBlog.Net.Infrastructure;

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
        public void CanReadConnectionString()
        {
            var connectionString = ApplicationConfiguration.ConnectionString;
            Assert.IsNotNull(connectionString);
            Assert.AreEqual("Server=localhost;Database=sblog;user id=msuser1;password=msuser1;", connectionString);
        }

        [TestMethod]
        public void CanReadHasherTyeFromWebConfig()
        {
            var hasherType = ApplicationConfiguration.HasherTypeName;
            Assert.IsNotNull(hasherType);
            Assert.AreEqual("sBlog.Net.Domain.Hashers.Sha5Hasher", hasherType);
        }

        [TestMethod]
        public void CanReadCacheDurationFromWebConfig()
        {
            var cacheDuration = ApplicationConfiguration.CacheDuration;
            Assert.IsNotNull(cacheDuration);
            Assert.AreEqual(15, cacheDuration);
        }

        [TestMethod]
        public void CanReadBitlyUserNameFromWebConfig()
        {
            var bitlyName = ApplicationConfiguration.BitlyUserName;
            Assert.IsNotNull(bitlyName);
            Assert.AreEqual("sampleaccount", bitlyName);
        }

        [TestMethod]
        public void CanReadBitlyApiKeyFromWebConfig()
        {
            var bitlyKey = ApplicationConfiguration.BitlyApiKey;
            Assert.IsNotNull(bitlyKey);
            Assert.AreEqual("samplekey", bitlyKey);
        }

        private static readonly SblogNetSettingsConfiguration BlogStaticConfig = ConfigurationManager.GetSection("sblognetSettings")
                                                                     as SblogNetSettingsConfiguration;
    }
}
