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
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Utilities;

namespace sBlog.Net.Domain.Hashers
{
    public class Md5Hasher : IHasher
    {
        public string HashString(string srcString)
        {
            return HashExtensions.GetMD5Hash(srcString);
        }
    }
}
