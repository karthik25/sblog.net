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

namespace sBlog.Net.Controllers
{
    public class MaintenanceController : Controller
    {
        //
        // GET: /Maintenance/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InvalidTheme()
        {
            return View();
        }
    }
}
