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
using sBlog.Net.Akismet.Interfaces;

namespace sBlog.Net.Akismet.Extensions
{
    public static class AksimetExtensions
    {
        public static AkismetStatus CheckIfSpamOrHam(this AkismetStatus commentStatus, IAkismetService akismet)
        {
            commentStatus.IsSpam = akismet.CommentCheck(commentStatus.Comment);
            commentStatus.IsHam = !commentStatus.IsSpam;
            return commentStatus;
        }

        public static AkismetStatus SubmitSpam(this AkismetStatus commentStatus, IAkismetService akismet)
        {
            if (commentStatus.IsSpam)
            {
                akismet.SubmitSpam(commentStatus.Comment);
            }
            return commentStatus;
        }

        public static AkismetStatus SubmitHam(this AkismetStatus commentStatus, IAkismetService akismet)
        {
            if (commentStatus.IsHam)
            {
                akismet.SubmitHam(commentStatus.Comment);
            }
            return commentStatus;
        }
    }
}
