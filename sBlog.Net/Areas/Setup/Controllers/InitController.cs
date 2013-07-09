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
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Concrete;
using sBlog.Net.Areas.Setup.Models;
using sBlog.Net.Areas.Setup.Services;

namespace sBlog.Net.Areas.Setup.Controllers
{
    public class InitController : Controller
    {
        private readonly IPathMapper _pathMapper;
        private readonly DbContext _dbContext;

        public InitController(IPathMapper pathMapper)
        {
            _pathMapper = pathMapper;
            _dbContext = new DbContext();
        }

        public ActionResult Index()
        {
            var setupStatusViewModel = GetSetupStatusViewModel();

            if (setupStatusViewModel == null)
            {
                return RedirectToRoute("InitializeDatabase");
            }

            if (setupStatusViewModel.InstallationComplete)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(setupStatusViewModel);
        }

        [HttpPost]
        public ActionResult Index(SetupStatusViewModel setupStatusViewModel)
        {
            if (ModelState.IsValid && _dbContext.IsCredentialsValid(setupStatusViewModel.ConnectionString))
            {
                return RedirectToRoute("SetupPage2");
            }

            var setupModel = GetSetupStatusViewModel();
            ModelState.AddModelError("ConnectionString", "Connection string entered does not match");

            return View(setupModel);
        }

        private SetupStatusViewModel GetSetupStatusViewModel()
        {
            SetupStatusViewModel model = null;
            try
            {
                var status = _dbContext.IsConnectionStringValid();
                var uploadStatus = UploadFolderVerifier.CanSaveOrDeleteFiles(_pathMapper);
                model = new SetupStatusViewModel
                    {
                        IsConnectionStringValid = status.SetupValid,
                        ConnectionStatusClass = status.CssClass,
                        Message = status.Message,
                        IsUploadsFolderValid = uploadStatus,
                        UploadsFolderStatusClass = uploadStatus ? "confirm" : "error",
                        InstallationComplete = _dbContext.IsInstallationComplete(),
                        UploadsMessage = uploadStatus
                                             ? "The uploads directory is writeable."
                                             : "The uploads directory is not writeable."
                    };
            }
            catch
            {
                
            }
            return model;
        }
    }
}
