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
using System.Web.Mvc;

namespace sBlog.Net.Areas.Setup
{
    public class SetupAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Setup";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("SetupPage2", "setup/page2",
                 new { controller = "Page2", action = "Index" });

            context.MapRoute("SetupIndex", "setup",
                 new { controller = "Init", action = "Index" });

            context.MapRoute(
                "Setup_default",
                "Setup/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
