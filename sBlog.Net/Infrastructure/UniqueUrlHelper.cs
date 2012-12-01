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
using sBlog.Net.Domain.Generators;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Infrastructure
{
    public static class UniqueUrlHelper
    {
        public static string FindUniqueUrl(IPost postRepository, string currentUrl, byte entryType, int? excludeId = null)
        {
            var allPosts = postRepository.GetAllPostsOrPages(false);
            var postEntities = entryType == 1 ? allPosts.Where(p => p.EntryType == 1) : allPosts.Where(p => p.EntryType == 2);
            if (excludeId.HasValue)
            {
                postEntities = postEntities.Where(p => p.PostID != excludeId);
            }
            var currentUrls = postEntities.Select(p => p.PostUrl).ToList();
            return currentUrl.GetUniqueSlug(currentUrls);
        }
    }
}
