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

using sBlog.Net.Akismet.Entities;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Akismet
{
    public class AkismetComment
    {
        public string Blog = null;
        public string UserIp = null;
        public string UserAgent = null;
        public string Referrer = null;
        public string Permalink = null;
        public string CommentType = null;
        public string CommentAuthor = null;
        public string CommentAuthorEmail = null;
        public string CommentAuthorUrl = null;
        public string CommentContent = null;

        public static AkismetComment Create(CommentEntity commentEntity, RequestData requestData)
        {
            return new AkismetComment
            {
                Blog = requestData.Blog,
                UserIp = requestData.UserIp,
                UserAgent = requestData.UserAgent,
                Referrer = requestData.Referrer,
                Permalink = null,
                CommentType = "blog",
                CommentAuthor = commentEntity.CommentUserFullName,
                CommentAuthorEmail = commentEntity.CommenterEmail,
                CommentAuthorUrl = commentEntity.CommenterSite,
                CommentContent = commentEntity.CommentContent
            };
        }
    }
}
