using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.CustomViewEngines
{
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine(string blogTheme)
        {
            base.AreaViewLocationFormats = new[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml", "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml" };
            base.AreaMasterLocationFormats = new[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml", "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml" };
            base.AreaPartialViewLocationFormats = new[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml", "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml" };
            base.ViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml", "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml" };
            base.MasterLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml", "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml" };
            base.PartialViewLocationFormats = GetPartialViewLocations(blogTheme);
            base.FileExtensions = new[] { "cshtml", "vbhtml" };
        }


        /// <summary>
        /// Gets the partial view locations. This is added as the first "looked up" location,
        /// since, in the case of sBlog.Net, partial views in the "Themes" folder takes presedence
        /// over the ones in the respective controller's respective folder or in the shared 
        /// views folder
        /// </summary>
        /// <param name="blogTheme">The blog theme.</param>
        /// <returns></returns>
        private static string[] GetPartialViewLocations(string blogTheme)
        {
            var locations = new List<string>();

            if (HasSharedPartialViews(blogTheme))
            {
                var csPath = "~/Themes/" + blogTheme + "/Shared/{0}.cshtml";
                var vbPath = "~/Themes/" + blogTheme + "/Shared/{0}.vbhtml";
                locations.Add(csPath);
                locations.Add(vbPath);
            }

            locations.Add("~/Views/{1}/{0}.cshtml");
            locations.Add("~/Views/{1}/{0}.vbhtml");
            locations.Add("~/Views/Shared/{0}.cshtml");
            locations.Add("~/Views/Shared/{0}.vbhtml");

            return locations.ToArray();
        }

        private static bool HasSharedPartialViews(string blogTheme)
        {
            if (string.IsNullOrEmpty(blogTheme))
                return false;

            var pathMapper = DependencyResolver.Current.GetService<IPathMapper>();
            var directoryRelativePath = string.Format("~/Themes/{0}/Shared", blogTheme);
            var directoryPath = pathMapper.MapPath(directoryRelativePath);
            return Directory.Exists(directoryPath);
        }
    }
}