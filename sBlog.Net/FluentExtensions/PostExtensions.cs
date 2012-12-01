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
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.FluentExtensions
{
    public static class PostExtensions
    {
        public static IList<PostEntity> GetPostsByStatus(this List<PostEntity> allPosts, int status)
        {
            var postEntities = allPosts;

            switch (status)
            {
                case 1:
                    postEntities = postEntities.Where(p => p.IsPrivate).ToList();
                    break;
                case 2:
                    postEntities = postEntities.Where(p => !p.IsPrivate).ToList();
                    break;
            }

            return postEntities;
        }
    }
}