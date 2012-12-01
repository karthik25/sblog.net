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
namespace sBlog.Net.Akismet.Entities
{
    public class AkismetStatus
    {
        public AkismetStatus()
        {

        }

        public AkismetStatus(AkismetComment comment)
        {
            Comment = comment;
        }

        public AkismetComment Comment { get; set; }
        public bool IsSpam { get; set; }
        public bool IsHam { get; set; }
    }
}
