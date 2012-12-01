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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace sBlog.Net.Areas.Setup.Models
{
    public class SetupStatusViewModel
    {
        public bool IsConnectionStringValid { get; set; }
        public bool IsUploadsFolderValid { get; set; }
        public string ConnectionStatusClass { get; set; }
        public string Message { get; set; }
        public string UploadsFolderStatusClass { get; set; }
        public string UploadsMessage { get; set; }
        public bool InstallationComplete { get; set; }

        [DisplayName("Enter the connection string")]
        [Required(ErrorMessage = "Connction string is required")]
        public string ConnectionString { get; set; }
    }
}
