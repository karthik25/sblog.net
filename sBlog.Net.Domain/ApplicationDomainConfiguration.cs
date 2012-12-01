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
using System.Configuration;

namespace sBlog.Net.Domain
{
    public static class ApplicationDomainConfiguration
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["AppDb"] != null ? ConfigurationManager.ConnectionStrings["AppDb"].ToString() : string.Empty;
            }
        }
    }
}
