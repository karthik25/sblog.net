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
    public class CategoryController : BlogController
    {
        private readonly ICategory _categoryRepository;

        public CategoryController(ICategory categoryRepository, ISettings settingsRepository)
            : base (settingsRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [ChildActionOnly]
        public ActionResult BlogCategories()
        {
            var model = _categoryRepository.GetCategories();
            return PartialView("BlogCategories", model);
        }
    }
}
