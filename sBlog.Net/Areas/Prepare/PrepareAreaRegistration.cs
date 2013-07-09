using System.Web.Mvc;

namespace sBlog.Net.Areas.Prepare
{
    public class PrepareAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Prepare";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("UpdateDatabase", "prepare/update",
                             new { controller = "Database", action = "Update" });

            context.MapRoute("InitializeDatabase", "prepare",
                             new { controller = "Database", action = "Index" });

            context.MapRoute(
                "Prepare_default",
                "Prepare/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
