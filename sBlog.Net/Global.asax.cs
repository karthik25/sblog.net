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
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using sBlog.Net.Areas.Admin.Models;
using sBlog.Net.CustomExceptions;
using sBlog.Net.DependencyManagement;
using sBlog.Net.Infrastructure;
using sBlog.Net.Models;
using sBlog.Net.Binders;
using System.Web.Security;
using System.Security.Principal;
using sBlog.Net.Filters;

namespace sBlog.Net
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new BlogErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute("Logon", "logon",
                            new { controller = "Account", action = "LogOn" });

            routes.MapRoute("Logoff", "logoff",
                            new { controller = "Account", action = "LogOff" });

            routes.MapRoute("Credits", "credits",
                            new { controller = "ViewPage", action = "Credits" });

            routes.MapRoute("Pages", "pages/{pageUrl}/{status}",
                            new { controller = "ViewPage", action = "Index", status = UrlParameter.Optional },
                            new { pageUrl = @"\S+", status = @"[a-z\-]*" });

            routes.MapRoute("Page", "page/{pageNumber}",
                            new { controller = "Home", action = "Index", pageNumber = UrlParameter.Optional },
                            new { pageNumber = @"\d+" });

            routes.MapRoute("CategoryPaged", "category/{categoryName}/page/{pageNumber}",
                            new { controller = "Home", action = "PostsByCategory" },
                            new { categoryName = @"\S+", pageNumber = @"\d+" });

            routes.MapRoute("Category", "category/{categoryName}",
                            new { controller = "Home", action = "PostsByCategory" },
                            new { categoryName = @"\S+" });

            routes.MapRoute("TagPaged", "tags/{tagName}/page/{pageNumber}",
                            new { controller = "Home", action = "PostsByTag" },
                            new { tagName = @"\S+", pageNumber = @"\d+" });

            routes.MapRoute("Tag", "tags/{tagName}",
                            new { controller = "Home", action = "PostsByTag" },
                            new { tagName = @"\S+" });

            routes.MapRoute("IndividualPost", "{year}/{month}/{url}/{status}",
                            new { controller = "Home", action = "View", status = UrlParameter.Optional },
                            new { year = @"\d{4}", month = @"[0-9]{1,2}", url = @"\S+", status = @"[a-z\-]*" });

            routes.MapRoute("PostByYearMonthPaged", "{year}/{month}/page/{pageNumber}",
                            new { controller = "Home", action = "PostsByYearAndMonth" },
                            new { year = @"\d{4}", month = @"[0-9]{1,2}", pageNumber = @"\d+" });

            routes.MapRoute("PostByYearMonth", "{year}/{month}",
                            new { controller = "Home", action = "PostsByYearAndMonth" },
                            new { year = @"\d{4}", month = @"[0-9]{1,2}" });

            routes.MapRoute("Error404", "404",
                            new { controller = "Errors", action = "Index" });

            routes.MapRoute("SetupError", "under-construction",
                            new {controller = "Maintenance", action = "Index"});

            routes.MapRoute("InvalidTheme", "invalid-theme",
                            new { controller = "Maintenance", action ="InvalidTheme" });

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("NotFound", "{*catchall}",
                            new { controller = "Errors", action = "Index" });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            RegisterRoutes(RouteTable.Routes);
            RegisterGlobalFilters(GlobalFilters.Filters);

            ModelBinders.Binders.Add(typeof(CheckBoxListViewModel), new CheckBoxListViewModelBinder());
            ModelBinders.Binders.Add(typeof(PostViewModel), new PostViewModelBinder());

            VerifyInstallation();
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            if (exception is UrlNotFoundException)
            {
                Log(exception);
                Response.Redirect(urlHelper.RouteUrl("Error404"), true);
            }
            else if (exception is SqlException)
            {
                Response.Redirect(urlHelper.RouteUrl("SetupError"), true);
            }
        }

        protected void Session_Start()
        {
            var installationStatus = Application["Installation_Status"];
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            if (installationStatus != null && installationStatus.ToString() == "false")
            {
                Response.Redirect(urlHelper.RouteUrl("SetupIndex"), true);
            }

            if (installationStatus != null && installationStatus.ToString() == "ERROR")
            {
                Response.Redirect(urlHelper.RouteUrl("SetupError"), true);
            }
        }

        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void VerifyInstallation()
        {
            try
            {
                var settingsRepository = InstanceFactory.CreateSettingsInstance();
                var installationStatus = settingsRepository.InstallationComplete;
                Application["Installation_Status"] = installationStatus.ToString().ToLower();
            }
            catch
            {
                Application["Installation_Status"] = "ERROR";
            }
        }

        private void Log(Exception exception)
        {
            var errorLogger = new ErrorLogger(exception);
            errorLogger.Log();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    var ticket = FormsAuthentication.Decrypt(encTicket);
                    var id = new UserIdentity(ticket);
                    var prin = new GenericPrincipal(id, null);
                    HttpContext.Current.User = prin;
                }
            }
        }
    }
}
