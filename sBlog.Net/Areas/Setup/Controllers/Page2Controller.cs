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
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Utilities;
using sBlog.Net.Infrastructure;
using sBlog.Net.Domain.Generators;

namespace sBlog.Net.Areas.Setup.Controllers
{
    public class Page2Controller : Controller
    {
        private readonly ISettings _settingsRepository;
        private readonly IUser _userRepository;

        public Page2Controller(ISettings settingsRepository, IUser userRepository)
        {
            _settingsRepository = settingsRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            var installStatus = GetInstallStatus();

            if (!installStatus)
                return RedirectToRoute("SetupIndex");

            var setupDefaultParametersViewModel = new SetupDefaultParametersViewModel
                                                  {
                                                      AkismetUrl = GetRootUrl().TrimEnd('/')
                                                  };

            return View(setupDefaultParametersViewModel);
        }

        [HttpPost]
        public ActionResult Index(SetupDefaultParametersViewModel setupDefaultParametersViewModel)
        {
            if (ModelState.IsValid)
            {
                if (VerifyFields(setupDefaultParametersViewModel))
                {
                    UpdatePassword(setupDefaultParametersViewModel);
                    UpdateSettings(setupDefaultParametersViewModel);

                    // Installation is complete. Update the application variable
                    HttpContext.Application["Installation_Status"] = null;

                    return RedirectToAction("Index", "Home", new { Area = "" });
                }
            }

            return View(setupDefaultParametersViewModel);
        }

        private void UpdateSettings(SetupDefaultParametersViewModel setupDefaultParametersViewModel)
        {
            _settingsRepository.BlogName = setupDefaultParametersViewModel.BlogName;
            _settingsRepository.BlogAkismetUrl = setupDefaultParametersViewModel.AkismetUrl;
            _settingsRepository.InstallationComplete = true;
        }

        private void UpdatePassword(SetupDefaultParametersViewModel setupDefaultParametersViewModel)
        {
            var randomCode = RandomStringGenerator.RandomString();
            var userEntity = new UserEntity
                                 {
                                     UserID = 1,
                                     Password = PasswordHelper.GenerateHashedPassword(setupDefaultParametersViewModel.Password, randomCode),
                                     UserCode = TripleDES.EncryptString(randomCode)
                                 };
            _userRepository.UpdateUser(userEntity);
        }

        private bool VerifyFields(SetupDefaultParametersViewModel setupDefaultParametersViewModel)
        {
            if (setupDefaultParametersViewModel.Password != setupDefaultParametersViewModel.ConfirmPassword)
            {
                ModelState.AddModelError("Password", "Passwords do not match");
                return false;
            }
            return true;
        }

        private bool GetInstallStatus()
        {
            var context = new DbContext();
            return context.IsConnectionStringPopulated() && context.IsConnectionStringValid().SetupValid;
        }

        private string GetRootUrl()
        {
            return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        }
    }
}
