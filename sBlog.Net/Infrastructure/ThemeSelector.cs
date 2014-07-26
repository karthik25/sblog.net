using sBlog.Net.Configuration;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Infrastructure
{
    public static class ThemeSelector
    {
        public static string FindTheme(this ThemeElement themeElement, ISettings settings, IPathMapper mapper)
        {
            var possibleTheme = themeElement.SelectedTheme;

            if (!string.IsNullOrEmpty(possibleTheme) && ThemeExists(possibleTheme, mapper))
                return possibleTheme;

            possibleTheme = settings.BlogTheme;

            if (!string.IsNullOrEmpty(possibleTheme) && ThemeExists(possibleTheme, mapper))
                return possibleTheme;

            return null;
        }

        public static bool MasterExists(this string themeName, IPathMapper pathMapper, string expectedMasterName)
        {
            var requiredFile = string.Format("{0}\\{1}.cshtml", pathMapper.MapPath(string.Format("~/Themes/{0}", themeName)), expectedMasterName);
            return System.IO.File.Exists(requiredFile);
        }

        private static bool ThemeExists(string themeName, IPathMapper pathMapper)
        {
            var requiredFolder = pathMapper.MapPath(string.Format("~/Themes/{0}", themeName));
            return System.IO.Directory.Exists(requiredFolder);
        }
    }
}