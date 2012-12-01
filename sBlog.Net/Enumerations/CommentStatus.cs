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
namespace sBlog.Net.Enumerations
{
    public enum CommentStatus
    {
        all = int.MaxValue,
        approved = 0,
        pending = 1,
        spam = 2,
        trash = -1
    }
}
