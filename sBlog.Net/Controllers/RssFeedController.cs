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
using System.Linq;
using System.Web.Mvc;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Models;
using sBlog.Net.Rss;
using sBlog.Net.CustomResults;

namespace sBlog.Net.Controllers
{
    public class RssFeedController : BlogController
    {
        private readonly IPost _postRepository;

        public RssFeedController(IPost postRepository, ISettings settingsRepository)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
        }

        public ActionResult Index()
        {
            var rssFeedViewModel = GetRssFeedViewModel();
            var feed = RssFeedGenerator.GetRssFeedData(rssFeedViewModel, Url);
            return new RssActionResult { Feed = feed };
        }

        private RssFeedViewModel GetRssFeedViewModel()
        {
            if (Request.Url != null)
                return new RssFeedViewModel
                           {
                               BlogTitle = SettingsRepository.BlogName,
                               BlogCaption = SettingsRepository.BlogCaption,
                               BlogLanguage = "en-us",
                               BlogUrl = GetRootUrl(),
                               Posts = _postRepository.GetPosts().Take(10).ToList()
                           };
            return null;
        }
    }
}
