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
namespace sBlog.Net.Models.Account
{
    public class AccountBaseViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
    }
}