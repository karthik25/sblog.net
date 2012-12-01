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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sBlog.Net.Collections;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.Controllers
{
    public class RecentsController : BlogController
    {
        private readonly IPost _postRepository;
        private readonly ICacheService _cacheService;

        public RecentsController(IPost postRepository, ISettings settingsRepository, ICacheService cacheService)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
            _cacheService = cacheService;
            ExpectedMasterName = string.Empty;
        }

        [ChildActionOnly]
        public ActionResult RecentPosts()
        {
            var posts = GetPostsInternal();
            var model = posts.OrderByDescending(p => p.PostEditedDate)
                             .Take(5);
            return PartialView("RecentPosts", model);
        }

        [ChildActionOnly]
        public ActionResult BlogArchives()
        {
            var model = new ArchiveCollection(GetPostsInternal());
            return PartialView(model);
        }

        private List<PostEntity> GetPostsInternal()
        {
            var posts = Request.IsAuthenticated
                            ? _postRepository.GetPosts(GetUserId())
                            : _cacheService.GetPostsFromCache(_postRepository, CachePostsUnauthKey);
            return posts;
        }
    }
}
