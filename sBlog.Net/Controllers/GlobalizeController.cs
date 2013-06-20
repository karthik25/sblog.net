using System.Globalization;
using System.Web.Mvc;

namespace sBlog.Net.Controllers
{
    public class GlobalizeController : Controller
    {
        public ActionResult ChangeCulture(string lang)
        {
            Session["Culture"] = new CultureInfo(lang);
            return RedirectToAction("Index", "Home");
        }
    }
}
