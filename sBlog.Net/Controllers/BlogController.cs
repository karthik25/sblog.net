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
using System.Configuration;
using System.Web.Mvc;
using sBlog.Net.Configuration;
using sBlog.Net.CustomExceptions;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Infrastructure;
using sBlog.Net.Mappers;
using sBlog.Net.Models;

namespace sBlog.Net.Controllers
{
    public class BlogController : Controller, IControllerProperties
    {
        protected string ExpectedMasterName = "_Layout";
        protected ISettings SettingsRepository;
        private readonly IPathMapper _pathMapper;

        public bool IsAdminController { get; set; }

        public BlogController(ISettings blogSettings)
        {
            SettingsRepository = blogSettings;
            _pathMapper = new PathMapper();
        }

        protected int GetUserId()
        {
            var userId = -1;
            if (Request.IsAuthenticated)
            {
                var userIdentity = (IUserInfo)User.Identity;
                userId = Int32.Parse(userIdentity.UserId);
            }
            return userId;
        }

        protected string GetToken()
        {
            var userIdentity = (UserIdentity)User.Identity;
            return userIdentity.UserToken;
        }

        protected bool CheckToken(string receivedToken)
        {
            var validationStatus = GetToken() == receivedToken;
            return validationStatus;
        }

        public string GetBlogTheme()
        {
            var themeName = BlogStaticConfig.Theme.FindTheme(SettingsRepository, _pathMapper);
            return themeName;
        }

        public ActionResult Logo()
        {
            var model = new LogoViewModel
                            {
                                BlogName = SettingsRepository.BlogName,
                                RootUrl = GetRootUrl()
                            };
            return PartialView(model);
        }

        public ActionResult IncompleteSetup()
        {
            var installationStatus = SettingsRepository.InstallationComplete;
            return PartialView("SetupStatus", installationStatus);
        }

        public ActionResult BlogFooter()
        {
            return PartialView("Footer", SettingsRepository.BlogName);
        }

        public string BlogCaption()
        {
            return SettingsRepository.BlogCaption;
        }

        protected bool IsMarkDown()
        {
            return SettingsRepository.EditorType == "markdown";
        }

        protected string GetRootUrl()
        {
            return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));            
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var action = filterContext.Result as ViewResult;

            if (action != null && !string.IsNullOrEmpty(ExpectedMasterName))
            {
                var themeName = BlogStaticConfig.Theme.FindTheme(SettingsRepository, _pathMapper);

                if (!string.IsNullOrEmpty(themeName))
                {
                    action.MasterName = themeName.MasterExists(_pathMapper, ExpectedMasterName) ? 
                                                    string.Format(LayoutFormat, themeName, ExpectedMasterName)
                                                    : string.Format(DefaultLayoutFormat, ExpectedMasterName);
                }
                else
                {
                    throw new InvalidThemeException("Invalid theme {0}", themeName);
                }
            }
            
            base.OnActionExecuted(filterContext);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            throw new UrlNotFoundException("Unable to find an action with the name specified: [{0}]", actionName);
        }

        private const string LayoutFormat = "~/Themes/{0}/{1}.cshtml";
        private const string DefaultLayoutFormat = "~/Views/Shared/{0}.cshtml";

        protected const string CachePostsUnauthKey = "GetAllPosts";
        protected const string CachePagesUnauthKey = "GetAllPages";

        private static readonly SblogNetSettingsConfiguration BlogStaticConfig = ConfigurationManager.GetSection("sblognetSettings")
                                                                             as SblogNetSettingsConfiguration;
    }

    public interface IControllerProperties
    {
        bool IsAdminController { get; set; }
    }
}
