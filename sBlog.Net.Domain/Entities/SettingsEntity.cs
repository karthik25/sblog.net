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

using System.ComponentModel.DataAnnotations;

namespace sBlog.Net.Domain.Entities
{
    [Table("sblog_settings")]
    public class SettingsEntity
    {
        [Key]
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
    }
}
