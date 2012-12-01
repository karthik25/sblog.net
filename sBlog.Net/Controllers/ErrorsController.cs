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

namespace sBlog.Net.Controllers
{
    public class ErrorsController : BlogController
    {
        public ErrorsController(ISettings settingsRepository)
            : base (settingsRepository)
        {
            ExpectedMasterName = "_LayoutPage";
        }

        public ActionResult Index()
        {
            Server.ClearError();
            return View();
        }
    }
}
