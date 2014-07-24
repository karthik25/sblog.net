using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Infrastructure;
using sBlog.Net.Tests.MockObjects;

namespace sBlog.Net.Tests.Infrastructure
{
    [TestClass]
    public class ThemeSelectorTests
    {
        [TestMethod]
        public void CanReadAThemeFromWebConfig()
        {
            var themeSettings = BlogStaticConfig.Theme;
            Assert.IsNotNull(themeSettings);
            Assert.AreEqual("PerfectBlemish", themeSettings.SelectedTheme);
        }

        [TestMethod]
        public void CanSelectAWebConfigTheme()
        {
            var settings = MockAppFactory.GetMockSettings();
            var mapper = new MockPathMapper();
            var themeElement = new ThemeElement{ SelectedTheme = "PrePaid" };
            var selectedTheme = themeElement.FindTheme(settings, mapper);
            Assert.AreEqual("PrePaid", selectedTheme);
        }

        [TestMethod]
        public void CanSelectASettingsTheme()
        {
            var settings = MockAppFactory.GetMockSettings();
            var mapper = new MockPathMapper();
            var themeElement = new ThemeElement();
            var selectedTheme = themeElement.FindTheme(settings, mapper);
            Assert.AreEqual("PerfectBlemish", selectedTheme);
        }

        [TestMethod]
        public void CanSelectASettingsThemeWithInvalidWebConfigTheme()
        {
            var settings = MockAppFactory.GetMockSettings();
            var mapper = new MockPathMapper();
            var themeElement = new ThemeElement{ SelectedTheme = "InvalidTheme" };
            var selectedTheme = themeElement.FindTheme(settings, mapper);
            Assert.AreEqual("PerfectBlemish", selectedTheme);
        } 

        [TestMethod]
        public void ReturnsNullIfNeitherExists()
        {
            var settings = MockAppFactory.GetMockSettings();
            settings.BlogTheme = "";
            var mapper = new MockPathMapper();
            var themeElement = new ThemeElement();
            var selectedTheme = themeElement.FindTheme(settings, mapper);
            Assert.IsNull(selectedTheme);
        }

        private static readonly SblogNetSettingsConfiguration BlogStaticConfig = ConfigurationManager.GetSection("sblognetSettings")
                                                                     as SblogNetSettingsConfiguration;

    }
}
